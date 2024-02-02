using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSystem
{
    class Loader3d
    {
        public static RawModel3d LoadCube(float tu = 1.0f, float tv = 1.0f, bool outer = true)
        {
            // vertices 8 points.
            Vertex3f[] Points = new Vertex3f[8]
            {
                new Vertex3f(-1, -1, -1),
                new Vertex3f(1, -1, -1),
                new Vertex3f(1, 1, -1),
                new Vertex3f(-1, 1, -1),
                new Vertex3f(-1, -1, 1),
                new Vertex3f(1, -1, 1),
                new Vertex3f(1, 1, 1),
                new Vertex3f(-1, 1, 1)
            };

            Vertex3f[] Normals = new Vertex3f[6]
            {
                Vertex3f.UnitX,
                -Vertex3f.UnitX,
                Vertex3f.UnitY,
                -Vertex3f.UnitY,
                Vertex3f.UnitZ,
                -Vertex3f.UnitZ
            };

            Vertex2f[] texCoords = new Vertex2f[4]
            {
                new Vertex2f(0, 0),
                new Vertex2f(tu, 0),
                new Vertex2f(tu, tv),
                new Vertex2f(0, tv)
            };

            //         7------6                        
            //         |      |                
            //         |  +z  |   counter-clockwise
            //         |      |               
            //  7------4------5------6------7          
            //  |      |      |      |      |  
            //  |  -x  |  -y  |  +x  |  +y  |
            //  |      |      |      |      | 
            //  3------0------1------2------3           
            //         |      |                 
            //         |  -z  |                
            //         |      |                
            //         3------2                         
            List<float> positionList = new List<float>();
            List<float> normalList = new List<float>();
            List<float> textureList = new List<float>();

            attachQuad3(positionList, Points, 1, 2, 6, 5, outer); // +x
            attachQuad3(positionList, Points, 3, 0, 4, 7, outer); // -x
            attachQuad3(positionList, Points, 2, 3, 7, 6, outer); // +y
            attachQuad3(positionList, Points, 0, 1, 5, 4, outer); // -y
            attachQuad3(positionList, Points, 4, 5, 6, 7, outer); // +z
            attachQuad3(positionList, Points, 3, 2, 1, 0, outer); // -z

            attachQuad3N(normalList, Normals, 0, 1, outer); // +x
            attachQuad3N(normalList, Normals, 1, 0, outer); // -x
            attachQuad3N(normalList, Normals, 2, 3, outer); // +y
            attachQuad3N(normalList, Normals, 3, 2, outer); // -y
            attachQuad3N(normalList, Normals, 4, 5, outer); // +z
            attachQuad3N(normalList, Normals, 5, 4, outer); // -z

            attachQuad2(textureList, texCoords, 0, 1, 2, 3, outer); // +x
            attachQuad2(textureList, texCoords, 0, 1, 2, 3, outer); // -x
            attachQuad2(textureList, texCoords, 0, 1, 2, 3, outer); // +y
            attachQuad2(textureList, texCoords, 0, 1, 2, 3, outer); // -y
            attachQuad2(textureList, texCoords, 0, 1, 2, 3, outer); // +z
            attachQuad2(textureList, texCoords, 0, 1, 2, 3, outer); // -z

            // gen vertext array.
            float[] positions = positionList.ToArray();
            float[] textures = textureList.ToArray();
            float[] normals = normalList.ToArray();

            TangentSpace.CalculateTangents(positions, textures, normals, out float[] tangents, out float[] bitangents);

            uint vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);
            uint vbo;
            vbo = StoreDataInAttributeList(0, 3, positions);
            vbo = StoreDataInAttributeList(1, 2, textures);
            vbo = StoreDataInAttributeList(2, 3, normals);
            vbo = StoreDataInAttributeList(3, 4, tangents);
            vbo = StoreDataInAttributeList(4, 4, bitangents);

            Gl.BindVertexArray(0);

            void attachQuad3(List<float> list, Vertex3f[] points, int a, int b, int c, int d, bool isOuter)
            {
                if (isOuter)
                {
                    list.AddRange(attachVertices3f(points, a, b, c));
                    list.AddRange(attachVertices3f(points, a, c, d));
                }
                else
                {
                    list.AddRange(attachVertices3f(points, a, c, b));
                    list.AddRange(attachVertices3f(points, a, d, c));
                }
            }

            void attachQuad3N(List<float> list, Vertex3f[] points, int a, int b, bool isOuter)
            {
                if (isOuter)
                {
                    list.AddRange(attachVertices3f(points, a, a, a));
                    list.AddRange(attachVertices3f(points, a, a, a));
                }
                else
                {
                    list.AddRange(attachVertices3f(points, b, b, b));
                    list.AddRange(attachVertices3f(points, b, b, b));
                }
            }

            void attachQuad2(List<float> list, Vertex2f[] points, int a, int b, int c, int d, bool isOuter)
            {
                if (isOuter)
                {
                    list.AddRange(attachVertices2f(points, a, b, c));
                    list.AddRange(attachVertices2f(points, a, c, d));
                }
                else
                {
                    list.AddRange(attachVertices2f(points, a, c, b));
                    list.AddRange(attachVertices2f(points, a, d, c));
                }
            }

            float[] attachVertices3f(Vertex3f[] points, int a, int b, int c)
            {
                float[] vertices = new float[9];
                vertices[0] = points[a].x; vertices[1] = points[a].y; vertices[2] = points[a].z;
                vertices[3] = points[b].x; vertices[4] = points[b].y; vertices[5] = points[b].z;
                vertices[6] = points[c].x; vertices[7] = points[c].y; vertices[8] = points[c].z;
                return vertices;
            }

            float[] attachVertices2f(Vertex2f[] points, int a, int b, int c)
            {
                float[] vertices = new float[6];
                vertices[0] = points[a].x; vertices[1] = points[a].y;
                vertices[2] = points[b].x; vertices[3] = points[b].y;
                vertices[4] = points[c].x; vertices[5] = points[c].y;
                return vertices;
            }

            return new RawModel3d(vao, positions);
        }

        /// <summary>
        /// * data를 gpu에 올리고 vbo를 반환한다.<br/>
        /// * vao는 함수 호출 전에 바인딩하여야 한다.<br/>
        /// </summary>
        /// <param name="attributeNumber">attributeNumber 슬롯 번호</param>
        /// <param name="coordinateSize">자료의 벡터 성분의 개수 (예) vertex3f는 3이다.</param>
        /// <param name="data"></param>
        /// <param name="usage"></param>
        /// <returns>vbo를 반환</returns>
        public static unsafe uint StoreDataInAttributeList(uint attributeNumber, int coordinateSize, float[] data, BufferUsage usage = BufferUsage.StaticDraw)
        {
            // VBO 생성
            uint vboID = Gl.GenBuffer();

            // VBO의 데이터를 CPU로부터 GPU에 복사할 때 사용하는 BindBuffer를 다음과 같이 사용
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(data.Length * sizeof(float)), data, usage);

            // 이전에 BindVertexArray한 VAO에 현재 Bind된 VBO를 attributeNumber 슬롯에 설정
            Gl.VertexAttribPointer(attributeNumber, coordinateSize, VertexAttribType.Float, false, 0, IntPtr.Zero);
            //Gl.VertexArrayVertexBuffer(glVertexArrayVertexBuffer, vboID, )

            // GPU 메모리 조작이 필요 없다면 다음과 같이 바인딩 해제
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);

            return vboID;
        }

    }
}

using OpenGL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace LSystem
{
    public partial class Form3D : Form
    {
        EngineLoop _gameLoop;
        List<Entity> entities;
        StaticShader _shader;

        public Form3D()
        {
            InitializeComponent();

            LSystem.Quaternion q1 = new Quaternion(1, 2, 3, 4);
            Quaternion q2 = new Quaternion(4, 3, 2, 1);
            Quaternion q12 = q1 * q2;
            Quaternion q21 = q2 * q1;
            Console.WriteLine($"{q12.X} {q12.Y} {q12.Z} {q12.W}");
            Console.WriteLine($"{q21.X} {q21.Y} {q21.Z} {q21.W}");
        }

        private void Form3D_Load(object sender, EventArgs e)
        {
            // ### 초기화 ###
            _gameLoop = new EngineLoop();
            _shader = new StaticShader();
            entities = new List<Entity>();

            Texture texture = new Texture(EngineLoop.PROJECT_PATH + @"\Res\bricks.jpg");
            TexturedModel texturedModel = new TexturedModel(Loader3d.LoadCube(), texture);
            for (int i = -2; i < 2; i++)
            {
                for (int j = -2; j < 2; j++)
                {
                    Entity ent = new Entity(texturedModel);
                    ent.Position = new Vertex3f(3 * i, 3 * j, 0);
                    ent.Material = Material.White;
                    ent.IsAxisVisible = true;
                    entities.Add(ent);
                }
            }

            // ### 주요로직 ###
            _gameLoop.UpdateFrame = (deltaTime) =>
            {
                int w = this.glControl1.Width;
                int h = this.glControl1.Height;
                if (_gameLoop.Width * _gameLoop.Height == 0)
                {
                    _gameLoop.Init(w, h);
                    _gameLoop.Camera.Init(w, h);
                }

                float milliSecond = deltaTime * 0.001f;

                Entity entity = entities[0];
                if (Keyboard.IsKeyDown(Key.D1)) entity.Roll(1);
                if (Keyboard.IsKeyDown(Key.D2)) entity.Roll(-1);
                if (Keyboard.IsKeyDown(Key.D3)) entity.Yaw(1);
                if (Keyboard.IsKeyDown(Key.D4)) entity.Yaw(-1);
                if (Keyboard.IsKeyDown(Key.D5)) entity.Pitch(1);
                if (Keyboard.IsKeyDown(Key.D6)) entity.Pitch(-1);

                Camera camera = _gameLoop.Camera;
                this.Text = $"{FramePerSecond.FPS}fps, t={FramePerSecond.GlobalTick} p={camera.Position}";
            };

            _gameLoop.RenderFrame = (deltaTime) =>
            {
                Camera camera = _gameLoop.Camera;

                Gl.Enable(EnableCap.CullFace);
                Gl.CullFace(CullFaceMode.Back);

                Gl.ClearColor(0.3f, 0.3f, 0.3f, 1.0f);
                Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Gl.Enable(EnableCap.DepthTest);

                Gl.Enable(EnableCap.Blend);
                Gl.BlendEquation(BlendEquationMode.FuncAdd);
                Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

                foreach (Entity entity in entities)
                {
                    Renderer.Render(_shader, entity, camera);
                }
            };
        }

        private void glControl1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Camera camera = _gameLoop.Camera;
            if (camera is FpsCamera) camera?.GoForward(0.02f * e.Delta);
            if (camera is OrbitCamera) (camera as OrbitCamera)?.FarAway(-0.005f * e.Delta);
        }

        private void glControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Mouse.CurrentPosition = new Vertex2i(e.X, e.Y);
            Camera camera = _gameLoop.Camera;
            Vertex2i delta = Mouse.DeltaPosition;
            camera?.Yaw(-delta.x);
            camera?.Pitch(-delta.y);
            Mouse.PrevPosition = new Vertex2i(e.X, e.Y);
        }

        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            int glLeft = this.Width - this.glControl1.Width;
            int glTop = this.Height - this.glControl1.Height;
            int glWidth = this.glControl1.Width;
            int glHeight = this.glControl1.Height;
            _gameLoop.DetectInput(this.Left + glLeft, this.Top + glTop, glWidth, glHeight);

            // 엔진 루프, 처음 로딩시 deltaTime이 커지는 것을 방지
            if (FramePerSecond.DeltaTime < 1000)
            {
                _gameLoop.Update(deltaTime: FramePerSecond.DeltaTime);
                _gameLoop.Render(deltaTime: FramePerSecond.DeltaTime);
            }
            FramePerSecond.Update();
        }

        private void glControl1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("정말로 끝내시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            else if (e.KeyCode == Keys.W)
            {
                
            }
        }
    }
}

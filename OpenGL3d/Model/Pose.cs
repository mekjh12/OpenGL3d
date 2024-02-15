using OpenGL;

namespace LSystem
{
    public struct Pose
    {
        Quaternion _q;
        Vertex3f _pos;

        public Quaternion Quaternion
        {
            get => _q;
            set => _q = value;
        }

        public Matrix4x4f Matrix4x4f
        {
            get
            {
                float angle = _q.RotationAngle;
                if (_q.RotationVector.Normalized == Vertex3f.UnitX)
                {
                    return Matrix4x4f.RotatedX(angle);
                }
                else if (_q.RotationVector.Normalized == Vertex3f.UnitY)
                {
                    return Matrix4x4f.RotatedY(angle);
                }
                else if (_q.RotationVector.Normalized == Vertex3f.UnitZ)
                {
                    return Matrix4x4f.RotatedZ(angle);
                }
                else
                {
                    return (Matrix4x4f)_q;
                }
            }
        }

        public Vertex3f Postiton
        {
            get => _pos;
            set => _pos = value;
        }

        public Pose(Quaternion q, Vertex3f pos)
        {
            _q = q;
            _pos = pos;
        }

    }
}

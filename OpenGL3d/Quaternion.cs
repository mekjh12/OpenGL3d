using System;
using OpenGL;

namespace LSystem
{
    public struct Quaternion
    {
        public static readonly Quaternion Identity = new Quaternion(0.0, 0.0, 0.0, 1.0);

        private Vertex3d _DefaultVector;
        private Vertex3d _Vector;
        private double _CosAngle;

        public Vertex3f RotationVector
        {
            get
            {
                if (_Vector.ModuleSquared() >= float.Epsilon)
                {
                    _DefaultVector = _Vector.Normalized;
                }

                return (Vertex3f)_DefaultVector;
            }
            set => SetEuler(value, RotationAngle);
        }

        public float RotationAngle
        {
            get => (float)Angle.ToDegrees(2.0 * Math.Acos(_CosAngle));
            set => SetEuler(RotationVector, value);
        }

        public float X
        {
            get => (float)_Vector.x;
            set => _Vector.x = value;
        }

        public float Y
        {
            get => (float)_Vector.y;
            set => _Vector.y = value;
        }

        public float Z
        {
            get => (float)_Vector.z;
            set => _Vector.z = value;
        }

        public float W
        {
            get => (float)_CosAngle;
            set => _CosAngle = value;
        }

        public double Magnitude
        {
            get
            {
                double num = _Vector.x * _Vector.x;
                double num2 = _Vector.y * _Vector.y;
                double num3 = _Vector.z * _Vector.z;
                double num4 = _CosAngle * _CosAngle;
                return Math.Sqrt(num + num2 + num3 + num4);
            }
        }

        public bool IsIdentity
        {
            get
            {
                if (Math.Abs(_Vector.Module()) >= float.Epsilon)
                {
                    return false;
                }

                if (Math.Abs(_CosAngle - 1.0) >= 1.4012984643248171E-45)
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsNormalized => Math.Abs(Magnitude - 1.0) < 1.4012984643248171E-45;

        public Quaternion Conjugated
        {
            get
            {
                Quaternion result = new Quaternion(this);
                result._Vector = -result._Vector;
                return result;
            }
        }

        public Quaternion(double q1, double q2, double q3, double q4)
        {
            _DefaultVector = Vertex3d.UnitY;
            _Vector.x = q1;
            _Vector.y = q2;
            _Vector.z = q3;
            _CosAngle = q4;
            _DefaultVector = RotationVector;
        }

        public Quaternion(Vertex3f rVector, float rAngle)
        {
            _DefaultVector = rVector;
            _Vector = default(Vertex3d);
            _CosAngle = 0.0;
            SetEuler(rVector, rAngle);
        }

        public Quaternion(Quaternion other)
        {
            _DefaultVector = other._DefaultVector;
            _Vector = other._Vector;
            _CosAngle = other._CosAngle;
        }

        public void SetEuler(Vertex3f rVector, float rAngle)
        {
            double num = Angle.ToRadians(rAngle / 2f);
            double num2 = Math.Sin(num);
            _Vector.x = num2 * (double)rVector.x;
            _Vector.y = num2 * (double)rVector.y;
            _Vector.z = num2 * (double)rVector.z;
            _CosAngle = Math.Cos(num);
            Normalize();
        }

        public void Normalize()
        {
            double magnitude = Magnitude;
            if (magnitude >= 1.4012984643248171E-45)
            {
                double num = 1.0 / magnitude;
                _Vector *= num;
                _CosAngle *= num;
                return;
            }

            throw new InvalidOperationException("zero magnitude quaternion");
        }

        public void Conjugate()
        {
            _Vector = -_Vector;
        }

        /// <summary>
        /// 쿼터니온의 곱을 반환한다. 
        /// OpenGL.Quaternion의 곱의 연산 오류로 인하여 새롭게 구현하였다.
        /// 순서는 q2.Concatenate(q1)의 의미는 q1을 적용한 후에 q2를 적용한다.
        /// </summary>
        /// <param name="quaternion"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            //순서는 q2.Concatenate(q1)의 의미는 q1을 적용한 후에 q2를 적용한다.
            float s1 = q1.W;
            float s2 = q2.W;
            Vertex3f v1 = new Vertex3f(q1.X, q1.Y, q1.Z);
            Vertex3f v2 = new Vertex3f(q2.X, q2.Y, q2.Z);
            float s = s1 * s2 - v1.Dot(v2);
            Vertex3f v = v1 * s2 + v2 * s1 + v1.Cross(v2);
            return new Quaternion(v.x, v.y, v.z, s);
        }

        public static Vertex3f operator *(Quaternion q, Vertex3f v)
        {
            return (Matrix3x3f)q * v;
        }

        public static Vertex3d operator *(Quaternion q, Vertex3d v)
        {
            return (Matrix3x3d)q * v;
        }

        public static Vertex4f operator *(Quaternion q, Vertex4f v)
        {
            return (Matrix4x4f)q * v;
        }

        public static Vertex4d operator *(Quaternion q, Vertex4d v)
        {
            return (Matrix4x4d)q * v;
        }

        public static explicit operator Matrix3x3f(Quaternion q)
        {
            Matrix3x3f result = default(Matrix3x3f);
            double x = q._Vector.x;
            double y = q._Vector.y;
            double z = q._Vector.z;
            double cosAngle = q._CosAngle;
            double num = x * x;
            double num2 = y * y;
            double num3 = z * z;
            result[0u, 0u] = (float)(1.0 - 2.0 * (num2 + num3));
            result[1u, 0u] = (float)(2.0 * (x * y - z * cosAngle));
            result[2u, 0u] = (float)(2.0 * (x * z + y * cosAngle));
            result[0u, 1u] = (float)(2.0 * (x * y + z * cosAngle));
            result[1u, 1u] = (float)(1.0 - 2.0 * (num + num3));
            result[2u, 1u] = (float)(2.0 * (y * z - x * cosAngle));
            result[0u, 2u] = (float)(2.0 * (x * z - y * cosAngle));
            result[1u, 2u] = (float)(2.0 * (x * cosAngle + y * z));
            result[2u, 2u] = (float)(1.0 - 2.0 * (num + num2));
            return result;
        }

        public static explicit operator Matrix3x3d(Quaternion q)
        {
            Matrix3x3d result = default(Matrix3x3d);
            double x = q._Vector.x;
            double y = q._Vector.y;
            double z = q._Vector.z;
            double cosAngle = q._CosAngle;
            double num = x * x;
            double num2 = y * y;
            double num3 = z * z;
            result[0u, 0u] = 1.0 - 2.0 * (num2 + num3);
            result[1u, 0u] = 2.0 * (x * y - z * cosAngle);
            result[2u, 0u] = 2.0 * (x * z + y * cosAngle);
            result[0u, 1u] = 2.0 * (x * y + z * cosAngle);
            result[1u, 1u] = 1.0 - 2.0 * (num + num3);
            result[2u, 1u] = 2.0 * (y * z - x * cosAngle);
            result[0u, 2u] = 2.0 * (x * z - y * cosAngle);
            result[1u, 2u] = 2.0 * (x * cosAngle + y * z);
            result[2u, 2u] = 1.0 - 2.0 * (num + num2);
            return result;
        }

        public static explicit operator Matrix4x4f(Quaternion q)
        {
            Matrix4x4f result = default(Matrix4x4f);
            double x = q._Vector.x;
            double y = q._Vector.y;
            double z = q._Vector.z;
            double cosAngle = q._CosAngle;
            double num = x * x;
            double num2 = y * y;
            double num3 = z * z;
            result[0u, 0u] = (float)(1.0 - 2.0 * (num2 + num3));
            result[1u, 0u] = (float)(2.0 * (x * y - z * cosAngle));
            result[2u, 0u] = (float)(2.0 * (x * z + y * cosAngle));
            result[3u, 0u] = 0f;
            result[0u, 1u] = (float)(2.0 * (x * y + z * cosAngle));
            result[1u, 1u] = (float)(1.0 - 2.0 * (num + num3));
            result[2u, 1u] = (float)(2.0 * (y * z - x * cosAngle));
            result[3u, 1u] = 0f;
            result[0u, 2u] = (float)(2.0 * (x * z - y * cosAngle));
            result[1u, 2u] = (float)(2.0 * (x * cosAngle + y * z));
            result[2u, 2u] = (float)(1.0 - 2.0 * (num + num2));
            result[3u, 2u] = 0f;
            result[0u, 3u] = 0f;
            result[1u, 3u] = 0f;
            result[2u, 3u] = 0f;
            result[3u, 3u] = 1f;
            return result;
        }

        public static explicit operator Matrix4x4d(Quaternion q)
        {
            Matrix4x4d result = default(Matrix4x4d);
            double x = q._Vector.x;
            double y = q._Vector.y;
            double z = q._Vector.z;
            double cosAngle = q._CosAngle;
            double num = x * x;
            double num2 = y * y;
            double num3 = z * z;
            result[0u, 0u] = 1.0 - 2.0 * (num2 + num3);
            result[1u, 0u] = 2.0 * (x * y - z * cosAngle);
            result[2u, 0u] = 2.0 * (x * z + y * cosAngle);
            result[3u, 0u] = 0.0;
            result[0u, 1u] = 2.0 * (x * y + z * cosAngle);
            result[1u, 1u] = 1.0 - 2.0 * (num + num3);
            result[2u, 1u] = 2.0 * (y * z - x * cosAngle);
            result[3u, 1u] = 0.0;
            result[0u, 2u] = 2.0 * (x * z - y * cosAngle);
            result[1u, 2u] = 2.0 * (x * cosAngle + y * z);
            result[2u, 2u] = 1.0 - 2.0 * (num + num2);
            result[3u, 2u] = 0.0;
            result[0u, 3u] = 0.0;
            result[1u, 3u] = 0.0;
            result[2u, 3u] = 0.0;
            result[3u, 3u] = 1.0;
            return result;
        }

        public override string ToString()
        {
            return $"Axis: {RotationVector} Angle: {RotationAngle} (X{_Vector.x},Y{_Vector.y},Z{_Vector.z},W{_CosAngle})";
        }
    }
}

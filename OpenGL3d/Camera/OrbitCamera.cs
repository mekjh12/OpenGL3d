using OpenGL;
using System;

namespace LSystem
{
    public class OrbitCamera : Camera
    {
        private float _distance = 1.0f;
        private const float MIN_FARAWAY_DISTANCE = 1.0f;

        public Vertex3f OrbitPositon => _position - _cameraForward * _distance;

        public OrbitCamera(string name, float x, float y, float z, float distance) : base(name, x, y, z)
        {
            _distance = distance;
        }

        public override Matrix4x4f ViewMatrix
        {
            get
            {
                Vertex3f pos = _position - _cameraForward * _distance;
                return Extension.CreateViewMatrix(pos, _cameraRight, _cameraUp, _cameraForward);
            }
        }

        public override void Update(int deltaTime)
        {
            base.Update(deltaTime);
        }

        protected override void UpdateCameraVectors()
        {
            Vertex3f direction = Vertex3f.Zero;
            float yawRad = _yaw.ToRadian();
            float pitchRad = _pitch.ToRadian();
            direction.x = Cos(yawRad) * Cos(pitchRad);
            direction.y = Sin(yawRad) * Cos(pitchRad);
            direction.z = Sin(pitchRad);

            _cameraForward = -direction.Normalized;
            _cameraRight = _cameraForward.Cross(Vertex3f.UnitZ).Normalized;
            _cameraUp = _cameraRight.Cross(_cameraForward).Normalized;

            float Cos(float radian) => (float)Math.Cos(radian);
            float Sin(float radian) => (float)Math.Sin(radian);
        }

        public void FarAway(float deltaDistance)
        {
            _distance += deltaDistance;
            if (_distance < MIN_FARAWAY_DISTANCE) { _distance = MIN_FARAWAY_DISTANCE; }
        }

        public override void GoForward(float deltaDistance)
        {
            Vertex3f forward = new Vertex3f(_cameraForward.x, _cameraForward.y, 0.0f);
            _position += forward * deltaDistance;
        }

        public override void GoRight(float deltaDistance)
        {
            Vertex3f right = new Vertex3f(_cameraRight.x, _cameraRight.y, 0.0f);
            _position += right * deltaDistance;
        }
    }
}

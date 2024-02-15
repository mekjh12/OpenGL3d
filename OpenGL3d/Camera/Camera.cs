using OpenGL;
using System;

namespace LSystem
{
    public abstract class Camera
    {
        private const float MAX_VARIANCE = 10; // 처음시작할 때 급격한 변동을 막기 위한 변수

        protected string _name = "";
        protected const float SENSITIVITY = 1.0f;

        protected float FOV_DEGREE = 90;
        protected float NEAR_PLANE = 0.1f;
        protected float FAR_PLANE = 500.0f;

        protected int _width;
        protected int _height;

        protected Vertex3f _cameraForward = Vertex3f.UnitZ;
        protected Vertex3f _cameraUp = Vertex3f.UnitY;
        protected Vertex3f _cameraRight = Vertex3f.UnitX;
        protected Vertex3f _position;

        protected const float MAX_PITCH = 89;
        protected float _pitch = 0.0f;
        protected float _yaw = 0.0f;


        public int Width => _width;

        public int Height => _height;

        public float AspectRatio => ((float)_width / (float)_height);

        public float CameraPitch
        {
            get => _pitch;
            set => _pitch = value;
        }

        public float CameraYaw
        {
            get => _yaw;
            set => _yaw = value;
        }

        public float FOV
        {
            get => FOV_DEGREE;
            set => FOV_DEGREE = value;
        }

        public string Name => _name;

        public float NEAR
        {
            get => NEAR_PLANE;
            set => NEAR_PLANE = value;
        }

        public float FAR
        {
            get => FAR_PLANE;
            set => FAR_PLANE = value;
        }

        public Vertex3f Position
        {
            get => _position;
            set => _position = value;
        }

        public Vertex3f Forward
        {
            get => _cameraForward;
            set => _cameraForward = value;
        }

        public Vertex3f Up
        {
            get => _cameraUp;
            set => _cameraUp = value;
        }

        public Vertex3f Right
        {
            get => _cameraRight;
            set => _cameraRight = value;
        }

        public virtual Matrix4x4f ModelMatrix => ViewMatrix.Inverse;

        public virtual Matrix4x4f ViewMatrix => Matrix4x4f.LookAtDirection(_position, _cameraForward, _cameraUp);

        public Matrix4x4f ProjectiveMatrix
        {
            get
            {
                float s = (float)_width / (float)_height;
                return Extension.CreateProjectionMatrix(FOV, s, NEAR, FAR);
            }
        }

        /// <summary>
        /// FocusDistance == g
        /// </summary>
        public float FocusDistance => 1.0f / (float)Math.Tan((FOV * 0.5f).ToRadian());

        public Camera(string name, float x, float y, float z)
        {
            _name = name;
            _position = new Vertex3f(x, y, z);
        }

        public virtual void Init(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual void Resume()
        {

        }

        public virtual void ShutDown()
        {
            //Debug.WriteLine("camera shutdown!");
        }

        public virtual void Update(int deltaTime)
        {
            UpdateCameraVectors();
        }

        protected abstract void UpdateCameraVectors();

        public virtual void Yaw(float deltaDegree)
        {
            if (Math.Abs(deltaDegree) > MAX_VARIANCE) return;
            _yaw += SENSITIVITY * deltaDegree;
            if (_yaw < -180) _yaw += 360;
            if (_yaw > 180) _yaw -= 360;
        }

        public virtual void Roll(float deltaDegree)
        {

        }

        public virtual void Pitch(float deltaDegree)
        {
            if (Math.Abs(deltaDegree) > MAX_VARIANCE) return;
            _pitch += SENSITIVITY * deltaDegree;
            _pitch = _pitch.Clamp(-MAX_PITCH, MAX_PITCH);
        }

        public virtual void GoForward(float deltaDistance)
        {
            _position += _cameraForward * deltaDistance;
        }

        public virtual void GoUp(float deltaDistance)
        {
            _position += _cameraUp * deltaDistance;
        }

        public virtual void GoRight(float deltaDistance)
        {
            _position += _cameraRight * deltaDistance;
        }

        public virtual void GoTo(Vertex3f position)
        {
            _position = position;
        }

    }
}

using OpenGL;
using System.Windows.Forms;
using System.Windows.Input;

namespace LSystem
{
    public partial class EngineLoop
    {
        public static string EXECUTE_PATH = Application.StartupPath;

        private FPSCamera _camera;
        private Form3D _form3d;

        private int _width;
        private int _height;

        public EngineLoop(Form3D form)
        {
            _form3d = form;
        }

        public void Init(int width, int height)
        {
            _width =  width;
            _height = height;

            ShowCursor(false);
            Gl.Viewport(0, 0, _width, _height);
        }

        public void Update(int deltaTime)
        {            
            if (_camera == null)
            {
                _camera = new FPSCamera("fpsCam", 0, 0, 0, 0, 0);
                _camera.Init(_width, _height);
            }
            
            KeyCheck(deltaTime);
            _camera.Update(deltaTime);

            _form3d.Text = $"{FramePerSecond.FPS}fps, t={FramePerSecond.GlobalTick}";
        }

        public void Render(int deltaTime)
        {
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            Gl.ClearColor(0.1f, 0.1f, 0.3f, 1.0f);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Gl.Viewport(0, 0, (int)_width, (int)_height);
            Gl.Enable(EnableCap.DepthTest);



        }

        public void KeyCheck(int deltaTime)
        {
            float milliSecond = deltaTime * 0.001f;
            float cameraSpeed = 1.0f;

            if (Keyboard.IsKeyDown(Key.W)) _camera.GoForward(milliSecond * cameraSpeed);
            if (Keyboard.IsKeyDown(Key.S)) _camera.GoForward(-milliSecond * cameraSpeed);
            if (Keyboard.IsKeyDown(Key.D)) _camera.GoRight(milliSecond * cameraSpeed);
            if (Keyboard.IsKeyDown(Key.A)) _camera.GoRight(-milliSecond * cameraSpeed);
            if (Keyboard.IsKeyDown(Key.E)) _camera.GoUp(milliSecond * cameraSpeed);
            if (Keyboard.IsKeyDown(Key.Q)) _camera.GoUp(-milliSecond * cameraSpeed);
        }

    }
}

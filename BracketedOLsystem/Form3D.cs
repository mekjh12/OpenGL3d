using LSystem;
using System;
using System.Windows.Forms;

namespace LSystem
{
    public partial class Form3D : Form
    {
        EngineLoop gameLoop;

        public Form3D()
        {
            InitializeComponent();
        }

        private void Form3D_Load(object sender, EventArgs e)
        {
            gameLoop = new EngineLoop(this);

            // 시작시 초기화
            this.glControl1.ContextCreated += (s, ee) =>
            {
                int w = this.glControl1.Width;
                int h = this.glControl1.Height;
                gameLoop.Init(w, h);
            };

            // 매 프레임마다 렌더링
            this.glControl1.Render += (s, ee) =>
            {
                int glLeft = this.Width - this.glControl1.Width;
                int glTop = this.Height - this.glControl1.Height;
                int glWidth = this.glControl1.Width;
                int glHeight = this.glControl1.Height;
                gameLoop.DetectInput(this.Left + glLeft, this.Top + glTop, glWidth, glHeight);

                // 엔진 루프, 처음 로딩시 deltaTime이 커지는 것을 방지
                if (FramePerSecond.DeltaTime < 1000)
                {
                    gameLoop.Update(deltaTime: FramePerSecond.DeltaTime);
                    gameLoop.Render(deltaTime: FramePerSecond.DeltaTime);
                }
                FramePerSecond.Update();
            };

        }
    }
}

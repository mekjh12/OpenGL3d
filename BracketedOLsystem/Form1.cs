using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LindenmayerSystem
{
    public partial class Form1 : Form
    {
        LSystem olSystem;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbGrammer.Text = "X,F[+X]F[-X]+X\r\nF,FF";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            olSystem = new LSystem();
            olSystem.Init(n: (int)nbrNum.Value, delta: (float)nbrDelta.Value);

            string[] lines = tbGrammer.Text.Split(new char[] { '\n' });
            for (int i = 0; i < lines.Length; i++)
            {
                string[] cols = lines[i].Split(new char[] {','});
                if (cols.Length != 2) continue;
                olSystem.Registry(cols[0].Trim(), cols[1].Trim());
            }

            Vertex3f pose = new Vertex3f(300, 20, 90);
            string word = olSystem.Generate(tbAxiom.Text);
            float r = (float)nbrLength.Value;

            // draw mode

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.Gray);
            Stack<Vertex3f> stack = new Stack<Vertex3f>();
            Stack<Pen> color = new Stack<Pen>();

            Random rnd = new Random();

            Vertex2f end = new Vertex2f();
            Vertex2f start = new Vertex2f();
            int height = this.pictureBox1.Height;

            Pen pen = new Pen(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), 10);
            color.Push(pen);

            while (true)
            {
                if (word.Length == 0) break;
                char c = word[0];
                word = word.Substring(1);

                if (c == 'F')
                {
                    start.x = pose.x;
                    start.y = pose.y;
                    float deg = pose.z;
                    float rad = deg * 3.141502f / 180.0f;
                    end.x = (float)(r *  Math.Cos(rad)) + start.x;
                    end.y = (float)(r * Math.Sin(rad)) + start.y;
                    g.DrawLine(pen, start.x, height - start.y, end.x, height - end.y);
                    pose.x = end.x;
                    pose.y = end.y;
                }
                else if (c == '+')
                {
                    pose.z += olSystem.Delta;
                }
                else if (c == '-')
                {
                    pose.z -= olSystem.Delta;
                }
                else if (c == '[')
                {
                    color.Push(pen);
                    pen = new Pen(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), 0.5f * pen.Width);
                    stack.Push(new Vertex3f(pose.x, pose.y, pose.z));
                }
                else if (c == ']')
                {
                    pose = stack.Pop();
                    pen = color.Pop();
                }
            }
        }
    }
}

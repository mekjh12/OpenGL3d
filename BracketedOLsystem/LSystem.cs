using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LSystem
{
    class LSystem
    {
        int _n;
        float _delta;
        Dictionary<string, string> _productions;
        Color _branchColor;
        Color _leafColor;

        public Color BranchColor
        {
            get => _branchColor; 
            set => _branchColor = value;
        }

        public Color LeafColor
        {
            get => _leafColor; 
            set => _leafColor = value;
        }

        public float Delta => _delta;

        public LSystem()
        {
            _branchColor = Color.Brown;
            _leafColor = Color.Green;
        }

        public void Init(int n, float delta)
        {
            _delta = delta;
            _n = n;
        }

        public void Registry(string predecessor,string successor)
        {
            if (_productions == null) _productions = new Dictionary<string, string>();

            if (_productions.ContainsKey(predecessor))
            {
                _productions[predecessor] = successor;
            }
            else
            {
                _productions.Add(predecessor, successor);
            }
        }

        private string Generate(string axiom)
        {
            string word = axiom;

            for (int i = 0; i < _n; i++)
            {
                foreach (KeyValuePair<string, string> item in _productions)
                {
                    string predecessor = item.Key;
                    word = ReplaceWord(word, predecessor);
                }
                Console.WriteLine(i + "=" + word);
            }

            return word;

            string ReplaceWord(string originalWord, string predecessor)
            {
                string newWord = "";
                int index = originalWord.IndexOf(predecessor);

                while (index >= 0)
                {
                    string successor = _productions.ContainsKey(predecessor) ? 
                        _productions[predecessor] : predecessor;

                    newWord += originalWord.Substring(0, index) + successor;
                    originalWord = originalWord.Substring(index + predecessor.Length);
                    index = originalWord.IndexOf(predecessor);
                }
                return newWord + originalWord;
            }
        }
        
        public void LoadProductions(string grammer)
        {
            string[] lines = grammer.Split(new char[] { '\n' });
            for (int i = 0; i < lines.Length; i++)
            {
                string[] cols = lines[i].Split(new char[] { ',' });
                if (cols.Length != 2) continue;
                Registry(cols[0].Trim(), cols[1].Trim());
            }
        }

        public void Render(Graphics g, string axiom,
            int px, int py, float prad, float height,
            float branchLength, float drawWidth)
        {
            Vertex3f pose = new Vertex3f(px, py, prad);
            string word = Generate(axiom);
            float r = (float)branchLength;

            // draw mode
            g.Clear(Color.Gray);
            Stack<Vertex3f> stack = new Stack<Vertex3f>();

            Vertex2f end = new Vertex2f();
            Vertex2f start = new Vertex2f();

            Pen pen = new Pen(_branchColor, drawWidth);

            while (true)
            {
                if (word.Length == 0) break;
                char c = word[0];
                word = word.Substring(1);

                if (c == 'F' || c == 'X')
                {
                    start.x = pose.x;
                    start.y = pose.y;
                    float deg = pose.z;
                    float rad = deg * 3.141502f / 180.0f;
                    end.x = (float)(r * Math.Cos(rad)) + start.x;
                    end.y = (float)(r * Math.Sin(rad)) + start.y;
                    g.DrawLine(pen, start.x, height - start.y, end.x, height - end.y);
                    pose.x = end.x;
                    pose.y = end.y;
                }
                else if (c == '+')
                {
                    pose.z += _delta;
                }
                else if (c == '-')
                {
                    pose.z -= _delta;
                }
                else if (c == '[')
                {
                    stack.Push(new Vertex3f(pose.x, pose.y, pose.z));
                }
                else if (c == ']')
                {
                    pose = stack.Pop();
                }
            }
        }

        public void RenderRndColorRewriting(Graphics g, string axiom, int px, int py, float prad, float height, float BranchLength)
        {
            Vertex3f pose = new Vertex3f(px, py, prad);
            string word = Generate(axiom);
            float r = (float)BranchLength;

            // draw mode
            g.Clear(Color.Gray);
            Stack<Vertex3f> stack = new Stack<Vertex3f>();
            Stack<Pen> color = new Stack<Pen>();

            Random rnd = new Random();

            Vertex2f end = new Vertex2f();
            Vertex2f start = new Vertex2f();

            Pen pen = new Pen(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), 10);
            color.Push(pen);

            while (true)
            {
                if (word.Length == 0) break;
                char c = word[0];
                word = word.Substring(1);

                if (c == 'F' || c == 'X')
                {
                    start.x = pose.x;
                    start.y = pose.y;
                    float deg = pose.z;
                    float rad = deg * 3.141502f / 180.0f;
                    end.x = (float)(r * Math.Cos(rad)) + start.x;
                    end.y = (float)(r * Math.Sin(rad)) + start.y;
                    g.DrawLine(pen, start.x, height - start.y, end.x, height - end.y);
                    pose.x = end.x;
                    pose.y = end.y;
                }
                else if (c == '+')
                {
                    pose.z += _delta;
                }
                else if (c == '-')
                {
                    pose.z -= _delta;
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

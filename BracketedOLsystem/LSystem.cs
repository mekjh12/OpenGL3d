using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LindenmayerSystem
{
    class LSystem
    {
        int _n;
        float _delta;
        Dictionary<string, string> _productions;

        public float Delta => _delta;

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

        public string Generate(string axiom)
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

    }
}

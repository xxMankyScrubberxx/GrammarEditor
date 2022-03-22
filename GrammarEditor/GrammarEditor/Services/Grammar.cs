using System;
using System.Collections.Generic;
using System.Text;

namespace GrammarEditor.Services
{
    public class Grammar
    {
        private int randString(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        public string Generate(string grammar)
        {
            while (grammar.IndexOf("[") >= 0)
            {
                int start = grammar.IndexOf("[", 0);
                int stop = grammar.IndexOf("]", 0);
                string options = grammar.Substring(start + 1, stop - start - 1);

                string[] opts = options.Split(',');
                int cnt = opts.Length;
                int rnd = randString(0, cnt - 1);
                string selection = opts[rnd];
                int sLen = selection.Length;

                if (selection[0].ToString() == " ")
                {
                    selection = selection.Substring(1, sLen);
                }

                string newgrammar = grammar.Substring(0, start) + selection + grammar.Substring(stop + 1, grammar.Length);

                grammar = newgrammar;
            }
            return grammar;
        }
    }
}

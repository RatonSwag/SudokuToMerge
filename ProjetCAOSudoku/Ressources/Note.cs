using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCAOSudoku.Ressources
{
    public abstract class Note
    {
        public int position { get; }

        public Note(int position)
        {
            this.position = position;
        }

        public Note(string strToNote)
        {
            position = int.Parse(strToNote.Split("$$")[0]);
        }

        public abstract override string ToString();

        public abstract string ToDisplay();
    }
}

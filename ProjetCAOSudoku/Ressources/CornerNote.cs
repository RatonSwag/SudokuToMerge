using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCAOSudoku.Ressources
{
    public class CornerNote : Note
    {
        public int note { get; set; }

        public CornerNote(int position, int note) : base(position)
        {
            this.note = note;
        }

        public CornerNote(string strToNote) : base(strToNote)
        {
            this.note = int.Parse(strToNote.Split("$$")[1]);
        }

        public override string ToString()
        {
            return $"{position}$${note}";
        }

        public override string ToDisplay()
        {
            return note.ToString();
        }
    }
}

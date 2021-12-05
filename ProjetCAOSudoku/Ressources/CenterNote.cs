using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCAOSudoku.Ressources
{
    public class CenterNote : Note
    {
        public List<int> notes { get; set; }

        public CenterNote(int position, int[] notes) : base(position)
        {
            this.notes = notes.ToList();
        }

        public CenterNote(string strToNote) : base(strToNote)
        {
            string[] notesArray = strToNote.Split("$$")[1].Split("**");
            notes = new();
            
            foreach(string str in notesArray)
            {
                this.notes.Add(int.Parse(str));
            }
            
        }


        public override string ToString()
        {
            string notesToString = $"{position}$$";

            //Formatte chaque chiffre de la note pour la sauvegarde/le chargement
            for(int ctr = 0; ctr < notes.Count; ctr++)
            {
                notesToString += notes[ctr];
                if(ctr != notes.Count - 1)
                {
                    notesToString += "**";
                }
            }
            return notesToString;
        }


        public override string ToDisplay()
        {
            string toDisplay = "";
            foreach(int n in notes)
            {
                toDisplay += n;
            }

            return toDisplay;
        }
    }
}

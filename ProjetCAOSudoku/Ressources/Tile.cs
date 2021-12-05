using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCAOSudoku.Ressources
{
    public class Tile
    {
        private const int nbrOfSide = 4;
        public int? answer { get;  set; }

        public bool isValid { get; set; }
        public List<Note> notes { get; private set; }

        public Tile() 
        {
            this.answer = null;
            this.isValid = true;
            this.notes = new List<Note>();
        }

        public Tile(int? answer, bool isValid)
        {
            this.answer = answer;
            this.isValid = isValid;
            this.notes = new List<Note>();
        }

        public Tile(int? answer, bool isValid, string notes)
        {
            this.answer = answer;
            this.isValid = isValid;
            this.notes = new List<Note>();

            foreach (string note in notes.Split("??"))
            {
                if (int.Parse(note.Split("$$")[0]) <= nbrOfSide)
                    this.notes.Add(new CornerNote(note));
                else
                    this.notes.Add(new CenterNote(note));
            }
            OrderNotes();
        }

        public void SetAnswer(int? value)
        {
            answer = value;
            isValid = true;
            notes.Clear();
        }

        public void SetIsValid(bool isValid)
        {
            this.isValid = isValid;
        }

        //Ajoute une note à la case et, si la case contient déja une note à la position, la remplace
        public void AddNote(int note, int position)
        {
            //Si la note est entre 1 et 9
            if(note >= 1 && note <= 9 && !Contains(note))
            {
                CornerNote newNote = new(position, note);
                //Si note à position donné existe, la remplace
                if (notes.Any(x => x.position == newNote.position))
                {
                    notes[notes.FindIndex(x => x.position == newNote.position)] = newNote;
                }
                //Sinon, l'ajoute à la liste
                else
                {
                    notes.Add(newNote);
                }
                OrderNotes();
            }
        }

        public void AddNote(int[] note, int position)
        {
            if(position > nbrOfSide)
            {
                CenterNote newNote = new(position, note);
                //Si note à position donné existe, la remplace
                if (notes.Any(x => x.position == newNote.position))
                {
                    notes[notes.FindIndex(x => x.position == newNote.position)] = newNote;
                }
                //Sinon, l'ajoute à la liste
                else
                {
                    notes.Add(newNote);
                }
                OrderNotes();
            }
        }

        public override string ToString()
        {

            if (answer != null)
            {
                //Si pas de notes dans la case mais réponse
                if (notes.Count == 0)
                    return $"{answer}--{isValid}";
                //Si notes et réponse
                else
                {
                    string notesToString = "";
                    for (int ctr = 0; ctr < notes.Count; ctr++)
                    {
                        notesToString += notes.ElementAt(ctr).ToString();
                        if (ctr != notes.Count - 1)
                        {
                            notesToString += "??";
                        }
                    }
                    return $"{answer}--{isValid}--{notesToString}".ToLower();
                }
            }
            else
            {
                //Si pas de notes dans la case ni réponse
                if (notes.Count == 0)
                    return $"null--{isValid}";
                //Si notes mais pas de réponse
                else
                {
                    string notesToString = "";
                    for (int ctr = 0; ctr < notes.Count; ctr++)
                    {
                        notesToString += notes.ElementAt(ctr).ToString();
                        if (ctr != notes.Count - 1)
                        {
                            notesToString += "??";
                        }
                    }

                    return $"null--{isValid}--{notesToString}";
                }
            }
        }

        //Vérifie si les notes dans la case contiennent le chiffre suivant
        private bool Contains(int note)
        {
            foreach (CornerNote n in notes)
            {
                if(n.note == note)
                    return true;
            }

            return false;
        }

        private void OrderNotes()
        {
            notes = notes.OrderBy(x => x.position).ToList();
        }

        public override bool Equals(object obj)
        {
            return obj is Tile tile && answer == tile.answer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCAOSudoku.Ressources
{
    public class BoardController
    {
        public MainWindow view { get; }
        public Board board { get; private set; }
        private GridStateChecker stateChecker;
        public BoardController(MainWindow view)
        {
            this.view = view;
            this.board = new Board();
            this.stateChecker = new GridStateChecker(this.board.grid, this.board.GetGridSize(), this.board.GetBoxSize());
            view.DrawGrid(board.grid);
        }

        //Met à jour la réponse dans la case sélectionnée
        public void SetAnswerAt(int row, int column, int answer)
        {
            board.SetAnswerAt(row, column, answer);
            var validite = stateChecker.CheckLine(row) && stateChecker.CheckColumn(column) && stateChecker.CheckBox(row, column);
            board.SetIsValid(validite, row, column);
            view.DrawGrid(board.grid);
        }

        //Ajoute une note à la case sélectionnée
        public void AddNoteAt(int row, int column, string note, int position)
        {
            if(note.Length == 1)
            {
                board.AddNoteAt(row, column, int.Parse(note), position);
            }
            else
            {
                List<int> listNote = new();

                foreach(Char c in note)
                {
                    listNote.Add(c - '0');
                }
                board.AddNoteAt(row, column, listNote.ToArray(), position);
            }

            view.DrawGrid(board.grid);
        }

        public void SaveBoard()
        {
            GridSaver saver = new();
            saver.SaveGrid(this.board.grid, this.board.GetGridSize());
        }

        public void LoadBoard()
        {
            GridSaver saver = new();
            this.board.grid = saver.LoadGrid(this.board.GetGridSize());
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjetCAOSudoku.Ressources
{
    public class Board
    {
        protected const int GridSize = 9;
        protected const int BoxSize = 3;
        public Tile[][] grid { get; set; }
        protected GridStateChecker stateChecker;

        public Board()
        {
            grid = new Tile[GridSize][];
            stateChecker = new GridStateChecker(grid, GridSize, BoxSize);

            //Initialise la grille
            for(int ctr = 0; ctr < GridSize; ctr++)
            {
                grid[ctr] = new Tile[GridSize];
            }

            for(int ctrRow = 0; ctrRow < GridSize; ctrRow++)
            {
                for(int ctrCol = 0; ctrCol < GridSize; ctrCol++)
                {
                    grid[ctrRow][ctrCol] = new Tile();
                }
            }
        }

        public Board(Tile[][] grid)
        {
            this.grid = grid;
        }

        //Set la réponse dans la case sélectionnée et vérifie l'état de la ligne, la colonne et la boîte
        public void SetAnswerAt(int indexRow, int indexColumn, int answer)
        {
            if (answer > 0 && answer <= GridSize)
            {
                grid[indexRow][indexColumn].SetAnswer(answer);
                //stateChecker.CheckLine(indexRow);
                //stateChecker.CheckColumn(indexColumn);
                //stateChecker.CheckBox(indexRow, indexColumn);
            }
        }

        public void SetIsValid(bool isValid, int ligne, int colone)
        {
            grid[ligne][colone].SetIsValid(isValid);
        }

        //Ajoute une note à la case sélectionnée
        public void AddNoteAt(int indexRow, int indexColumn, int note, int position)
        {
            grid[indexRow][indexColumn].AddNote(note, position);
        }

        //Ajoute une note à la case sélectionnée
        public void AddNoteAt(int indexRow, int indexColumn, int[] note, int position)
        {
            grid[indexRow][indexColumn].AddNote(note, position);
        }

        public int GetGridSize()
        {
            return GridSize;
        }

        public int GetBoxSize()
        {
            return BoxSize;
        }
    }
}
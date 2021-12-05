using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetCAOSudoku.Ressources
{
    public class GridStateChecker
    {
        private Tile[][] grid;
        private int gridSize;
        private int boxSize;

        public GridStateChecker(Tile[][] grid, int gridSize, int boxSize)
        {
            this.grid = grid;
            this.gridSize = gridSize;
            this.boxSize = boxSize;
        }

        //Vérifie si les réponses ne se répêtent pas dans une ligne
        public bool CheckLine(int index)
        {
            Tile[] line = grid[index];

            return CheckValues(line.ToList());
        }


        //Vérifie si les réponses ne se répêtent pas dans une colonne
        public bool CheckColumn(int index)
        {
            List<Tile> column = new();

            for (int ctr = 0; ctr < grid.Length; ctr++)
            {
                column.Add(grid[ctr][index]);
            }

            return CheckValues(column);
        }


        //Vérifie si les réponses ne se répêtent pas dans une boîte
        public bool CheckBox(int row, int column)
        {
            //Étant donné que c'est int/int, la réponse va être arrondie à l'entier inférieur.
            //Multiplié par la taille de la boîte, ça donne la première ligne où se situe la boîte.
            int boxRowStart = (row / boxSize) * boxSize;
            int boxColumnStart = (column / boxSize) * boxSize;

            var box = new List<Tile>();


            for (int ctrRow = boxRowStart; ctrRow < (boxRowStart + boxSize); ctrRow++)
            {
                for (int ctrCol = boxColumnStart; ctrCol < (boxColumnStart + boxSize); ctrCol++)
                {
                    box.Add(grid[ctrRow][ctrCol]);
                }
            }

            return CheckValues(box);
        }

        //Vérifie si les valeurs passées en paramètre se répètent et, s'ils se répètent, mets les cases répètées dans l'état non valide
        public bool CheckValues(List<Tile> values)
        {
            bool valid = true;
            foreach (Tile t in values)
            {
                if (t.answer != null && values.Count(x => x.answer == t.answer) > 1)
                {
                    valid = false;
                    t.isValid = false;
                }
                else if(t.answer != null && values.Count(x => x.answer == t.answer) == 1)
                {
                    t.isValid = true;
                }
            }
            return valid;
        }
    }
}

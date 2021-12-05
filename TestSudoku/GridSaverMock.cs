using ProjetCAOSudoku.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudoku
{
    public class GridSaverMock : GridSaver
    {
        List<string> savedGrid;

        public GridSaverMock()
        {
            savedGrid = new();
        }

        public void SaveGrid(Tile[][] grid, int gridSize)
        {
            this.savedGrid.Clear();

            //Parcourt la grille
            for (int ctrRow = 0; ctrRow < gridSize; ctrRow++)
            {
                for (int ctrColumn = 0; ctrColumn < gridSize; ctrColumn++)
                {
                    this.savedGrid.Add(grid[ctrRow][ctrColumn].ToString());
                }
            }

            //return savedGrid.ToArray();
        }


        public Tile[][] LoadGrid(int gridSize)
        {
            string[] tiles = savedGrid.ToArray();
            int ctrGrid = 0;
            Tile[][] newGrid = new Tile[gridSize][];

            //Parcours les rangées de la grille
            for (int ctrRow = 0; ctrRow < gridSize; ctrRow++)
            {
                newGrid[ctrRow] = new Tile[gridSize];
                //Pour chaque case de la rangée, crée une nouvelle
                for (int ctrColumn = 0; ctrColumn < gridSize; ctrColumn++)
                {
                    string[] newTile = tiles[ctrGrid++].Split("--");
                    //Si il n'y a pas de note dans la case à créer 
                    if (newTile.Length == 2)
                    {
                        if (newTile[0] != "null")
                            newGrid[ctrRow][ctrColumn] = new Tile(int.Parse(newTile[0]), bool.Parse(newTile[1]));
                        else
                            newGrid[ctrRow][ctrColumn] = new Tile(null, bool.Parse(newTile[1]));
                    }
                    //Si il y a des notes dans la case à créer
                    else if (newTile.Length > 2)
                    {
                        newGrid[ctrRow][ctrColumn] = new Tile(null, bool.Parse(newTile[1]), newTile[2]);
                    }

                }
            }

            return newGrid;
        }
    }
}

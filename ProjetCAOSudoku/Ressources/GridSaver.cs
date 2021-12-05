using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjetCAOSudoku.Ressources
{
    public class GridSaver
    {
        public void SaveGrid(Tile[][] grid, int gridSize)
        {
            string gridAsString = "";

            //Parcourt la grille
            for (int ctrRow = 0; ctrRow < gridSize; ctrRow++)
            {
                for (int ctrColumn = 0; ctrColumn < gridSize; ctrColumn++)
                {
                    gridAsString += grid[ctrRow][ctrColumn].ToString() + "\n";
                }
            }

            File.WriteAllText("SavedBoard.txt", gridAsString);
        }

        public Tile[][] LoadGrid(int gridSize)
        {
            try
            {
                int ctrGrid = 0;
                string[] tiles = File.ReadAllLines("SavedBoard.txt");
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
                        if(newTile.Length == 2)
                        {
                            if(newTile[0] != "null")
                                newGrid[ctrRow][ctrColumn] = new Tile(int.Parse(newTile[0]), bool.Parse(newTile[1]));
                            else
                                newGrid[ctrRow][ctrColumn] = new Tile(null, bool.Parse(newTile[1]));
                        }
                        //Si il y a des notes dans la case à créer
                        else if(newTile.Length > 2)
                        {
                            newGrid[ctrRow][ctrColumn] = new Tile(null, bool.Parse(newTile[1]), newTile[2]);
                        }

                    }
                }

                return newGrid;
            }
            catch (FileNotFoundException)
            {
                //Console.WriteLine("File not found");
                return null;
            }
            
        }
    }
}

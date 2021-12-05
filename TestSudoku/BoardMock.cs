using ProjetCAOSudoku.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudoku
{
    public class BoardMock : Board
    {
        public BoardMock()
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

        public BoardMock(Tile[][] grid)
        {
            this.grid = grid;
        }

        public override bool Equals(object obj)
        {
            bool isEqual = true;
            if(obj is BoardMock)
            {
                BoardMock toCompare = (BoardMock)obj;

                for(int ctrRow = 0; ctrRow < GetGridSize(); ctrRow++)
                {
                    for(int ctrColumn = 0; ctrColumn < GetGridSize(); ctrColumn++)
                    {
                        if (!grid[ctrRow][ctrColumn].Equals(toCompare.grid[ctrRow][ctrColumn]))
                            isEqual = false;
                    }
                }
            }

            return obj is BoardMock board && isEqual;
        }
    }
}

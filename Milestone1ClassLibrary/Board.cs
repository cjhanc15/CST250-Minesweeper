using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone1ConsoleApp
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] Grid { get; set; }
        public double Difficulty { get; set; }

        public Board(int size) 
        {
            Size = size;
            Grid = new Cell[Size, Size];
            Difficulty = 1;

            // Initialize each cell in the grid
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Grid[row, col] = new Cell();
                }
            }
        }

        public void SetupLiveNeighbors()
        {
            int numberOfBombs = (int)(Size * Size /(Difficulty * 10));
            Random random = new Random();

            // Place bombs randomly on the grid
            for (int i = 0; i < numberOfBombs; i ++)
            {
                int row = random.Next(Size);
                int col = random.Next(Size);

                Grid[row, col].Live = true;
            }
        }


        public void CalculateLiveNeighbors()
        {//nested loop
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {//liveNeighbors preset to 0
                    int liveNeighbors = 0;

                    if (!Grid[row, col].Live)
                    {
                        // Check neighboring cells for bombs
                        for (int i = Math.Max(0, row - 1); i <= Math.Min(row + 1, Size - 1); i++)
                        {
                            for (int j = Math.Max(0, col - 1); j <= Math.Min(col + 1, Size - 1); j++)
                            {//if live, increment number of liveNeighbors
                                if (Grid[i, j].Live)
                                {
                                    liveNeighbors++;
                                }
                            }
                        }
                    }

                    Grid[row, col].LiveNeighbors = liveNeighbors;
                }
            }
        }

        public void FloodFill(int row, int col)
        {
            // Check if coordinates are out of bounds
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return;

            Cell cell = Grid[row, col];

            // Check if cell is already visited or is live
            if (cell.Visited || cell.Live)
                return;

            // Mark the cell as visited
            cell.Visited = true;

            // Check if cell has no live neighbors
            if (cell.LiveNeighbors == 0)
            {
                // Recursive calls to flood-fill neighboring cells
                FloodFill(row - 1, col - 1);
                FloodFill(row - 1, col);
                FloodFill(row - 1, col + 1);
                FloodFill(row, col - 1);
                FloodFill(row, col + 1);
                FloodFill(row + 1, col - 1);
                FloodFill(row + 1, col);
                FloodFill(row + 1, col + 1);
            }
        }

    }
}

using System;

namespace Milestone1ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Setup game board
            Board board = new Board(10);
            board.setupLiveNeighbors();
            board.CalculateLiveNeighbors();
            PrintBoard(board); //used for testing

            bool endGame = false;

            while (!endGame)
            {
                // 1. Ask user for row & col number
                Console.WriteLine("Enter row number: ");
                int rowNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter column number: ");
                int colNumber = Convert.ToInt32(Console.ReadLine());

                // Mark the selected cell as visited
                board.Grid[rowNumber, colNumber].Visited = true;

                // 2. Check if grid contains a bomb at the current cell, player loses if so
                if (board.Grid[rowNumber, colNumber].Live)
                {
                    endGame = true;
                    Console.WriteLine("RIP! Game Over");
                }
                else
                {
                    // 3. Check if all non-bomb cells have been revealed
                    bool allRevealed = true;
                    for (int row = 0; row < board.Size; row++)
                    {
                        for (int col = 0; col < board.Size; col++)
                        {
                            if (!board.Grid[row, col].Live && !board.Grid[row, col].Visited)
                            {
                                allRevealed = false;
                                break;
                            }
                        }
                        if (!allRevealed)
                            break;
                    }
                    //if all are revealed (that are not bombs), player wins
                    if (allRevealed)
                    {
                        endGame = true;
                        Console.WriteLine("Congratulations! You Win!");
                    }
                }

                // 4. Print board during the game
                PrintBoardDuringGame(board);
            }
        }


        static void PrintBoard(Board board)
        {//nested loop
            Console.WriteLine("Minesweeper");
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {//check if cell is live, if so print X
                    Cell cell = board.Grid[row, col];
                    if (cell.Live)
                    {
                        Console.Write("X ");
                    }
                    else
                    {//if cell is not live, print number of live neighbors
                        Console.Write(cell.LiveNeighbors + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void PrintBoardDuringGame(Board board)
        {
            //nested loop
            Console.WriteLine("Minesweeper");
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    //check if cell has been visited
                    Cell cell = board.Grid[row, col];
                    if (cell.Visited)
                    {   //if cell has been visited & is live, print X
                        if (cell.Live)
                        {
                            Console.Write("X ");
                        }
                        else
                        {//if cell has been visited & is not live, print a blank space
                            Console.Write(cell.LiveNeighbors + " ");
                        }
                    }
                    else
                    {//if cell has not been visited, print ?
                        Console.Write("? ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}

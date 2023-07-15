using System;

namespace Milestone1ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Setup game board
            Board board = new Board(10);
            board.SetupLiveNeighbors();
            board.CalculateLiveNeighbors();
            PrintBoard(board); //used for testing

            bool endGame = false;

            while (!endGame)
            {
                // 1. Ask user for row & col number
                Console.WriteLine("Enter row number: ");
                int rowNumber;
                //error handling if users input invalid character
                while (!int.TryParse(Console.ReadLine(), out rowNumber) || rowNumber < 0 || rowNumber >= board.Size)
                {
                    Console.WriteLine("Invalid input. Enter row number: ");
                }

                Console.WriteLine("Enter column number: ");
                int colNumber;
                //error handling if users input invalid character
                while (!int.TryParse(Console.ReadLine(), out colNumber) || colNumber < 0 || colNumber >= board.Size)
                {
                    Console.WriteLine("Invalid input. Enter column number: ");
                }

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
        {   //print title
            Console.WriteLine("Minesweeper");

            // Print column numbers
            Console.Write("   ");
            for (int col = 0; col < board.Size; col++)
            {
                Console.Write(col + " ");
            }
            Console.WriteLine("\n   -------------------");

            for (int row = 0; row < board.Size; row++)
            {
                // Print row number
                Console.Write(row + " |");

                for (int col = 0; col < board.Size; col++)
                {
                    // check if cell is live, if so print X
                    Cell cell = board.Grid[row, col];
                    if (cell.Live)
                    {
                        Console.Write("X ");
                    }
                    else
                    {
                        // if cell is not live, print number of live neighbors
                        Console.Write(cell.LiveNeighbors + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void PrintBoardDuringGame(Board board)
        {
            // Print title
            Console.WriteLine("Minesweeper");

            // Print column numbers
            Console.Write("   ");
            for (int col = 0; col < board.Size; col++)
            {
                Console.Write(col + " ");
            }
            Console.WriteLine("\n   -------------------");

            for (int row = 0; row < board.Size; row++)
            {
                // Print row number
                Console.Write(row + "| ");

                for (int col = 0; col < board.Size; col++)
                {
                    // Check if cell has been visited
                    Cell cell = board.Grid[row, col];
                    if (cell.Visited)
                    {
                        //print number of live neighbors or ~
                        if (cell.LiveNeighbors == 0) Console.Write("~ ");
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow; // Set text color to yellow
                            Console.Write(cell.LiveNeighbors + " ");
                            Console.ResetColor(); // Reset text color to default
                        }
                    }
                    else
                    {
                        // Call FloodFill on unvisited cells
                        Console.Write("? ");
                        board.FloodFill(row, col);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}


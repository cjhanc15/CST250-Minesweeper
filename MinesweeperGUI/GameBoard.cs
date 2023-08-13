using Milestone1ClassLibrary;
using Milestone1ConsoleApp;
using MinesweeperGUI.Properties;

namespace MinesweeperGUI
{
    public partial class GameBoard : Form
    {
        public string difficulty = "";
        public string playerName = "";
        public Board board = new Board(10);
        public DateTime startTime;

        public GameBoard(string difficulty, string playerName)
        {
            InitializeComponent();

            // Set the difficulty level
            this.difficulty = difficulty;
            this.playerName = playerName;

            // Set up the Minesweeper board with the selected difficulty level
            int size = GetBoardSizeFromDifficulty(difficulty);
            board = new Board(size);
            board.SetupLiveNeighbors();
            board.CalculateLiveNeighbors();

            // Call the CreateBoard() method to render the buttons based on the board size
            CreateBoard();

            startTime = DateTime.Now;
        }


        public void CreateBoard()
        {
            // Set form size to fit the grid
            this.ClientSize = new System.Drawing.Size(80 * board.Size, 80 * board.Size);

            // Create the buttons and add them to the form
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Button button = new Button
                    {
                        Name = $"button_{row}_{col}",
                        Size = new System.Drawing.Size(80, 80),
                        Location = new System.Drawing.Point(80 * col, 80 * row),
                        TabIndex = row * board.Size + col,
                        Text = "",
                        UseVisualStyleBackColor = true
                    };

                    //set back color of buttons to pink
                    button.BackColor = ColorTranslator.FromHtml("#CAAAC1");

                    // Add click event handler to the button
                    button.MouseDown += new MouseEventHandler(Button_MouseDown);

                    // Add the button to the form
                    this.Controls.Add(button);
                }
            }
        }

        // Gets board size based on difficulty selected
        private int GetBoardSizeFromDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case "Easy": return 8;
                case "Intermediate": return 10;
                case "Difficult": return 12;
                default: return 10;
            }
        }

        private bool CheckIfWon()
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Cell currentCell = board.Grid[row, col];

                    // If a non-bomb cell is not visited, the winning condition is not met
                    if (!currentCell.Live && !currentCell.Visited)
                    {
                        return false;
                    }
                }
            }
            DisplayAllFlags();
            return true; // All non-bomb cells are revealed, player wins
        }

        //Helper method to display all flags
        private void DisplayAllFlags()
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Cell cell = board.Grid[row, col];
                    Button button = this.Controls[$"button_{row}_{col}"] as Button;

                    if (cell.Live)
                    {
                        button.BackgroundImage = Properties.Resources.flagPng;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                        button.BackColor = ColorTranslator.FromHtml("#EE0F5F");
                    }
                }
            }
        }

        // Helper method to display all bombs on board when player loses
        private void DisplayAllBombs()
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Cell cell = board.Grid[row, col];
                    Button button = this.Controls[$"button_{row}_{col}"] as Button;

                    if (cell.Live)
                    {
                        button.BackgroundImage = Properties.Resources.firePng;
                        button.BackgroundImageLayout = ImageLayout.Zoom;
                        button.BackColor = ColorTranslator.FromHtml("#EE0F5F");
                    }
                }
            }
        }

        //gets the time taken to win the game
        public int GetTimeElapsed()
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Format the elapsed time to seconds
            int formattedTime = (int)elapsedTime.TotalSeconds;

            return formattedTime;
        }

        //displays scores from top 5 users
        private void DisplayStats()
        {
            if(CheckIfWon())
            {
                PlayerStats playerStat = new PlayerStats
                {
                    PlayerName = playerName,
                    Difficulty = difficulty,
                    TimeElapsed = GetTimeElapsed()
                };

                // Append the PlayerStat to the file
                string dataToAppend = $"{playerStat.PlayerName},{playerStat.Difficulty},{playerStat.TimeElapsed}\n";
                File.AppendAllText(@"C:\Users\Shadow\Documents\scores.txt", dataToAppend);

            }
            // Show the HighScore form
            HighScore highScoreForm = new HighScore(difficulty);
            highScoreForm.Show();
        }


    private void Button_MouseDown(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = e as MouseEventArgs; // Cast the event args to MouseEventArgs
            Button clickedButton = (Button)sender; // Cast the sender back to a Button

            // Get the row and column information from the button's name
            string[] buttonNameParts = clickedButton.Name.Split('_');
            int row = int.Parse(buttonNameParts[1]);
            int col = int.Parse(buttonNameParts[2]);

            // Get the corresponding cell from the board
            Cell cell = board.Grid[row, col];

            // On left click
            if (mouseArgs.Button == MouseButtons.Left)
            {
                // If the cell is a live cell, handle it as a game-over scenario
                if (cell.Live)
                {
                    clickedButton.BackgroundImage = Properties.Resources.firePng;
                    clickedButton.BackgroundImageLayout = ImageLayout.Zoom;
                    clickedButton.BackColor = ColorTranslator.FromHtml("#EE0F5F");
                    DisplayAllBombs();
                    MessageBox.Show("You lose!");
                    DisplayStats();
                }
                else
                {
                    //reset background image to blank (in case of flag)
                    clickedButton.BackgroundImage = null;
                    // Perform flood-fill to reveal connected empty cells
                    board.FloodFill(row, col);
                    //for loop to reveal all visited cells
                    for (int r = 0; r < board.Size; r++)
                    {
                        for (int c = 0; c < board.Size; c++)
                        {
                            Cell currentCell = board.Grid[r, c];
                            Button currentButton = this.Controls[$"button_{r}_{c}"] as Button;
                            //if cell has been visited, change button text accordingly
                            if (currentCell.Visited)
                            {
                                //reset background image to blank (in case of flag)
                                currentButton.BackgroundImage = null;
                                if (currentCell.LiveNeighbors == 0)
                                {
                                    currentButton.BackColor = ColorTranslator.FromHtml("#EAE0EC"); // 40% transparency (102/255)
                                }
                                else
                                {
                                    currentButton.Text = currentCell.LiveNeighbors.ToString();
                                    currentButton.BackColor = ColorTranslator.FromHtml("#DED0DD"); // 40% transparency (102/255)
                                }
                            }
                        }
                    }
                    //if game has been won,end the game and display stats
                    if (CheckIfWon())
                    {
                        MessageBox.Show("You win!!\nTime elapsed: " + GetTimeElapsed() + " seconds");
                        DisplayStats();
                    }
                }
            }
            //On right click
            else if (mouseArgs.Button == MouseButtons.Right)
            {
                //if the cell has been visited, nothing happens
                //if the cell has not been visited, flag it
                if (!cell.Visited)
                {
                    clickedButton.BackgroundImage = Properties.Resources.flagPng;
                    clickedButton.BackgroundImageLayout = ImageLayout.Zoom;
                    clickedButton.BackColor = ColorTranslator.FromHtml("#EE0F5F");
                }
            }
        }
    }
}
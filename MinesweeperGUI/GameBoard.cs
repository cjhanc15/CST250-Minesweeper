using Milestone1ConsoleApp;

namespace MinesweeperGUI
{
    public partial class GameBoard : Form
    {
        public Board board = new Board(10);
        public GameBoard()
        {
            InitializeComponent();
            //calls method that automatically renders the game board full of clickable buttons
            CreateBoard();
        }

        public void CreateBoard()
        {
            // Set form size to fit the grid
            this.ClientSize = new System.Drawing.Size(40 * board.Size, 40 * board.Size);

            // Create the buttons and add them to the form
            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    Button button = new Button
                    {
                        Name = $"button_{row}_{col}",
                        Size = new System.Drawing.Size(40, 40),
                        Location = new System.Drawing.Point(40 * col, 40 * row),
                        TabIndex = row * board.Size + col,
                        Text = "",
                        UseVisualStyleBackColor = true
                    };

                    // Add click event handler to the button
                    button.Click += new EventHandler(Button_Click);

                    // Add the button to the form
                    this.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // This event handler will be called when any button in the grid is clicked.
            // You can add the logic for Minesweeper game here.
        }
    }
}
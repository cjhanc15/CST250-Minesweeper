using Milestone1ConsoleApp;

namespace MinesweeperGUI
{
    public partial class GameBoard : Form
    {
        public string difficulty = "";
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

                    // Add click event handler to the button
                    button.Click += new EventHandler(Button_Click);

                    // Add the button to the form
                    this.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender; // Cast the sender back to a Button

            // Change the color of the button to pink
            clickedButton.BackColor = Color.Pink;

            // Increment the button's text if it is numeric (assuming you have numbers as the button's text)
            int number;
            if (int.TryParse(clickedButton.Text, out number))
            {
                clickedButton.Text = (number + 1).ToString();
            }
            else
            {
                clickedButton.Text = "0";
            }
        }

    }
}
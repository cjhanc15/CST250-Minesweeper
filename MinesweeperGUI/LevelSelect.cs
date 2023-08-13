using Milestone1ConsoleApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    public partial class LevelSelect : Form
    {
        public LevelSelect()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Get the value from groupBox1 radio buttons
            string difficulty = "";
            string player = "";

            foreach (RadioButton radioButton in groupBox1.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    // Get the text of the selected radio button
                    difficulty = radioButton.Text;
                    break;
                }
            }

            if (playerName.Text.Length > 0) player = playerName.Text;
            else player = "Anonymous";

            // Create a new instance of GameBoard & set the difficulty level property
            GameBoard gameBoard = new GameBoard(difficulty, player); // Pass the difficulty level to the constructor
            gameBoard.Show();
        }
    }
}

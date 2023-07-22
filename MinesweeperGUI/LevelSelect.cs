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

        private void button1_Click(object sender, EventArgs e)
        {
            //close LevelSelect form
            this.Hide();

            //get value from groupBox1 radio buttons
            string difficulty = "";

            foreach (RadioButton radioButton in groupBox1.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked)
                {
                    difficulty = radioButton.Text; // Get the text of the selected radio button
                    break;
                }
            }

            //create new instance of GameBoard & open
            GameBoard gameBoard = new();
            gameBoard.difficulty = difficulty;
            gameBoard.Show();
        }
    }
}

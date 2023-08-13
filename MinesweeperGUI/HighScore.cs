using Milestone1ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperGUI
{
    public partial class HighScore : Form
    {
        public HighScore(string difficulty)
        {
            InitializeComponent();
            LoadTopScores(difficulty);
        }

        private void LoadTopScores(string selectedDifficulty)
        {
            string filePath = @"C:\Users\Shadow\Documents\scores.txt";

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read all the lines from the file
                var lines = File.ReadAllLines(filePath);

                // Parse the scores 
                var scores = lines.Select(line =>
                {
                    var parts = line.Split(',');
                    return new PlayerStats
                    {
                        PlayerName = parts[0],
                        Difficulty = parts[1],
                        TimeElapsed = int.Parse(parts[2])
                    };
                })
                .ToList();

                // Filter scores by selected difficulty and sort them by score
                var filteredScores = scores.Where(s => s.Difficulty == selectedDifficulty)
                                           .OrderBy(s => s.CalculateScore())
                                           .ToList();

                // Take top 5 scores
                var topScores = filteredScores.Take(5);

                // Display the scores in the listBox
                foreach (var score in topScores)
                {
                    scoresListBox.Items.Add($"{score.PlayerName} -- {score.Difficulty} -- Score: {score.CalculateScore() * 100:000}");
                }

            }
            else
            {
                // You can notify the user or handle it gracefully if there are no scores available.
                scoresListBox.Items.Add("No scores available for this difficulty!");
            }
        }

    }
}

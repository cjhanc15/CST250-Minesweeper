using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone1ClassLibrary
{
    public class PlayerStats : IComparable<PlayerStats>
    {
        public string PlayerName { get; set; }
        public string Difficulty { get; set; }
        public int TimeElapsed { get; set; }

        public double CalculateScore()
        {
            //score from time is multiplied based on difficulty
            double difficultyMultiplier = 1.0;

            switch (Difficulty)
            {
                case "Easy":
                    difficultyMultiplier = 100;
                    break;
                case "Intermediate":
                    difficultyMultiplier = 150;
                    break;
                case "Difficult":
                    difficultyMultiplier = 200;
                    break;
            }

            // Lower time = higher score
            return difficultyMultiplier / TimeElapsed;
        }

        public int CompareTo(PlayerStats other)
        {
            if (other == null) return 1;
            return CalculateScore().CompareTo(other.CalculateScore());
        }
    }
}

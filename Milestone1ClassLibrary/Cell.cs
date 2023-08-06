using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone1ConsoleApp
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public Boolean Visited { get; set; }
        public Boolean Live { get; set; }
        public int LiveNeighbors { get; set; }

        //initialize cell
        public Cell() 
        {
            Row = -1;
            Col = -1;
            Visited = false;
            Live = false;
            LiveNeighbors = 0;
        }
    }
}

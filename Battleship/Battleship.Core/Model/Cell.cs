using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Model
{
    public class Cell
    {
        public int X { get; private set; }
        public char Y { get; private set; }

        public Cell(int X, char Y)
        {
            if (X < 1 || X > 10) throw new ArgumentOutOfRangeException("X");
            if (Y < 'a' || Y > 'j') throw new ArgumentOutOfRangeException("Y");

            this.X = X;
            this.Y = Y;
        }

        public Cell(string Coord) : this(int.Parse(Coord.Substring(0, 1)), char.Parse(Coord.Substring(1, 1))) { }
    }
}

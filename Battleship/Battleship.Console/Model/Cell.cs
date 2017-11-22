using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.Model
{
    public class Cell
    {
        public bool IsDestroyed { get; set; }
        public Ship Parent { get; set; }
        public class CellEqualityComparer : IEqualityComparer<Cell>
        {
            public bool Equals(Cell x, Cell y)
            {
                return x.ToString() == y.ToString();
            }

            public int GetHashCode(Cell obj)
            {
                return obj.ToString().GetHashCode();
            }
        }

        public int X { get; private set; }
        public char Y { get; private set; }

        public Cell(int X, char Y)
        {
            if (X < 1 || X > 10) throw new ArgumentOutOfRangeException("X");
            if (Y < 'a' || Y > 'j') throw new ArgumentOutOfRangeException("Y");

            this.X = X;
            this.Y = Y;

            this.IsDestroyed = false;
        }

        //public Cell(string Coord) : this(int.Parse(Coord.Substring(0, 1)), char.Parse(Coord.Substring(1, 1))) { }

        public static IEnumerable<Cell> Parse(string coordinates)
        {
            return
                from coord in coordinates.Split(new char[] { ';', ' ', '.', '-', ',', '!', '/', '\\', '|' })
                select new Cell(coord);
        }

        public Cell(string coord)
        {
            if (coord.Length < 2 || coord.Length > 3) throw new ArgumentOutOfRangeException("coord");

            string n1;
            string n2;
            int x;
            char y;

            if (coord.Length == 2)
            {
                n1 = coord.Substring(0, 1);
                n2 = coord.Substring(1, 1);
            }
            else if (coord.Length == 3)
            {
                if (coord.EndsWith("0"))
                {
                    n1 = coord.Substring(0, 1);
                    n2 = coord.Substring(1, 2);
                }
                else if (coord.Substring(1, 1) == "0")
                {
                    n1 = coord.Substring(0, 2);
                    n2 = coord.Substring(1, 1);
                }
                else throw new ArgumentException("coord");
            }
            else throw new ArgumentException("coord");

            if (int.TryParse(n1, out x) && char.TryParse(n2, out y))
            {
                this.X = x;
                this.Y = y;
            }
            else if (int.TryParse(n2, out x) && char.TryParse(n1, out y))
            {
                this.X = x;
                this.Y = y;
            }
            else throw new ArgumentException("coord");
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", X, Y);
        }

        public override bool Equals(object obj)
        {
            return this.ToString() == obj.ToString();
        }
    }
}

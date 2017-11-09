using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.Model
{
    public class Ship
    {
        public IEnumerable<Cell> Cells;

        public Ship(IEnumerable<Cell> Cells)
        {
            if (Cells == null) throw new ArgumentNullException("Cells");

            this.Cells = Cells;
        }

        public int Length
        {
            get
            {
                return Cells.Count<Cell>();
            }
        }
    }
}

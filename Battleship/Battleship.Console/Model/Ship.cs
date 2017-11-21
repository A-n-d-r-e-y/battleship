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
        public bool IsDestroyed
        {
            get
            {
                return (from c in Cells
                        where !c.IsDestroyed
                        select c).Count() == 0;
            }
        }

        public Ship(IEnumerable<Cell> Cells)
        {
            if (Cells == null) throw new ArgumentNullException("Cells");

            foreach (var cell in Cells)
            {
                cell.Parent = this;
            }

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

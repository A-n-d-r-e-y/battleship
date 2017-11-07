using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core.Model
{
    public class Fleet
    {
        private Dictionary<int, int> fleetEmptySpaceMap = new Dictionary<int, int>();

        public Fleet()
        {
            // decks count, ships count 
            fleetEmptySpaceMap.Add(1, 4);
            fleetEmptySpaceMap.Add(2, 3);
            fleetEmptySpaceMap.Add(3, 2);
            fleetEmptySpaceMap.Add(4, 1);
        }

        public bool AddShip(Ship ship)
        {
            if (fleetEmptySpaceMap[ship.Length] > 0)
            {
                --fleetEmptySpaceMap[ship.Length];
                return true;
            }
            return false;
    }
}

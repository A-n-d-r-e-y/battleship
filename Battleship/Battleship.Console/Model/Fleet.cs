using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.Model
{
    public class Fleet
    {
        private Dictionary<int, int> fleetEmptySpaceMap = new Dictionary<int, int>();
        private Dictionary<int, List<Ship>> fleet = new Dictionary<int, List<Ship>>();

        public Fleet()
        {
            // (decks count, ships count) 
            fleetEmptySpaceMap.Add(1, 4);
            fleetEmptySpaceMap.Add(2, 3);
            fleetEmptySpaceMap.Add(3, 2);
            fleetEmptySpaceMap.Add(4, 1);
        }

        public bool AddShip(Ship ship)
        {
            if (fleetEmptySpaceMap[ship.Length] > 0)
            {
                if (fleet.ContainsKey(ship.Length))
                {
                    var ships = fleet[ship.Length];
                    ships.Add(ship);
                }
                else
                {
                    fleet.Add(ship.Length, new List<Ship>() { ship });
                }

                --fleetEmptySpaceMap[ship.Length];
                return true;
            }
            return false;
        }

        public IEnumerable<Cell> GetShipsCells()
        {
            return from list in fleet.Values
                   from ship in list
                   from cell in ship.Cells
                   select cell;
        }
    }
}

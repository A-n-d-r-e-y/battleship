using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core
{


    public enum ShipType
    {
        Submarine = ShipInfo.MIN_SHIP_SIZE,
        Destroyer = 2,
        Cruiser = 3,
        Battleship = 4,
        AircraftCarrier = ShipInfo.MAX_SHIP_SIZE
    }

    public class ShipInfo
    {
        public const int MIN_SHIP_SIZE = 1;
        public const int MAX_SHIP_SIZE = 5;

        public int ShipSize { get; set; }
        public ShipType ShipType { get; set; }

        public ShipInfo(int size)
        {
            if (size < ShipInfo.MIN_SHIP_SIZE || size > ShipInfo.MAX_SHIP_SIZE) throw new ArgumentOutOfRangeException("size");

            this.ShipSize = size;
            this.ShipType = ShipInfo.GetShipTypeBySize(size).Value;
        }

        public ShipInfo(ShipType type)
        {
            this.ShipType = type;
            this.ShipSize = (int)type;
        }

        public static ShipType? GetShipTypeBySize(int size)
        {
            switch (size)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return new Nullable<ShipType>((ShipType)size);
                default: return null;
            }
        }
    }
}

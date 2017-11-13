﻿using Battleship.Core;
using Battleship.Console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console
{
    public class BattleshipFakeRepository : BattleshipRepositoryBase
    {
        private Guid? GameId;
        private string GameName;
        private string HostPlayerName;
        private string GuestPlayerName;
        private Fleet HostPlayersFleet;
        private Fleet GuestPlayersFleet;

        public override Guid CreateGame(string GameName, string HostPlayerName)
        {
            this.GameId = new Nullable<Guid>(Guid.NewGuid());
            this.GameName = GameName;
            this.HostPlayerName = HostPlayerName;
            this.HostPlayersFleet = new Fleet();
            this.GuestPlayersFleet = new Fleet();

            return this.GameId.Value;
        }

        public override Nullable<Guid> FindGame(string GameName)
        {
            return this.GameName == GameName ? GameId : null;
        }

        public override bool JoinGame(Guid GameId, string GuestPlayerName)
        {
            if (GameId == this.GameId.Value)
            {
                this.GuestPlayerName = GuestPlayerName;
                return true;
            }
            return false;
        }

        public override bool AddShipToFleet(Guid GameId, string PlayerName, string Coordinates, int Size)
        {
            if (Coordinates == null) throw new ArgumentNullException("Coordinates");

            var cells =
                from coord in Coordinates.Split(new char[] { ';', ' ', '.', '-', ',', '!', '/', '\\', '|' })
                select new Cell(coord);

            if (cells.Count() != Size) throw new ArgumentException("Size");

            if (GameId == this.GameId.Value && PlayerName == this.GuestPlayerName)
            {
                return GuestPlayersFleet.AddShip(new Ship(cells));
            }

            if (GameId == this.GameId.Value && PlayerName == this.HostPlayerName)
            {
                return HostPlayersFleet.AddShip(new Ship(cells));
            }

            return false;
        }

        public override bool CheckShip(Guid GameId, int X, char Y)
        {
            var cell = new Cell(X, Y);
            var comparer = new Cell.CellEqualityComparer();

            if (GameId == this.GameId.Value)
            {
                return GuestPlayersFleet.GetShipsCells()
                    .Union(HostPlayersFleet.GetShipsCells())
                    .Contains<Cell>(cell, comparer);
            }

            return false;
        }

        public override int? SuggestNextShipSize(Guid GameId, string PlayerName)
        {
            if (GameId == this.GameId.Value && PlayerName == this.GuestPlayerName)
            {
                return GuestPlayersFleet.SuggestDeckToAdd();
            }
            else if (GameId == this.GameId.Value && PlayerName == this.HostPlayerName)
            {
                return HostPlayersFleet.SuggestDeckToAdd();
            }
            else return null;
        }

        public override bool? IsFleetFull(Guid gameId, string playerName)
        {
            if (GameId == this.GameId.Value && playerName == this.GuestPlayerName)
            {
                return GuestPlayersFleet.IsFleetFull;
            }
            else if (GameId == this.GameId.Value && playerName == this.HostPlayerName)
            {
                return HostPlayersFleet.IsFleetFull;
            }
            else return null;
        }
    }
}

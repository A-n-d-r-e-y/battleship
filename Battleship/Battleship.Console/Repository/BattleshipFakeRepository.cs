using Battleship.Core;
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
        private List<Cell> HostMissedShots;
        private List<Cell> GuestMissedShots;

        public override Guid CreateGame(string GameName, string HostPlayerName)
        {
            this.GameId = new Nullable<Guid>(Guid.NewGuid());
            this.GameName = GameName;
            this.HostPlayerName = HostPlayerName;
            this.HostPlayersFleet = new Fleet();
            this.GuestPlayersFleet = new Fleet();
            this.HostMissedShots = new List<Cell>();
            this.GuestMissedShots = new List<Cell>();

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

            var cells = Cell.Parse(Coordinates);

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

        public override CellState CheckCell(Guid GameId, string playerName, int X, char Y)
        {
            var check = new Cell(X, Y);
            var comparer = new Cell.CellEqualityComparer();
            var fleet = playerName == this.GuestPlayerName ? GuestPlayersFleet : HostPlayersFleet;
            var misses = playerName == this.GuestPlayerName ? GuestMissedShots : HostMissedShots;

            if (GameId == this.GameId.Value)
            {
                var cell = fleet
                    .GetShipsCells()
                    .Where(c => c.Equals(check))
                    .FirstOrDefault();

                if (cell == null)
                {
                    if (misses != null && misses.Where(m => m.Equals(check)).Count() > 0)
                    {
                        return CellState.HasMiss;
                    }
                    else return CellState.Empty;
                }
                else
                {
                    return cell.IsDestroyed ? CellState.Destroyed : CellState.HasShip;
                }
            }

            return CellState.Unknown;
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

        public override bool? IsGameOver(Guid gameId)
        {
            if (GameId == this.GameId.Value)
            {
                return GuestPlayersFleet.IsFleetEmpty || HostPlayersFleet.IsFleetEmpty;
            }
            else return null;
        }

        private Fleet GetOppositePlayersFleet(Guid gameId, string currentPlayer)
        {
            if (gameId == this.GameId.Value)
            {
                if (currentPlayer == this.GuestPlayerName) return this.HostPlayersFleet;
                if (currentPlayer == this.HostPlayerName) return this.GuestPlayersFleet;
            }

            return null;
        }

        public override Info<ShotResult> TakeTurn(Guid gameId, string playerName, string coordinates)
        {
            Cell shot = new Cell(coordinates);

            if (gameId == this.GameId.Value)
            {
                var fleet = GetOppositePlayersFleet(gameId, playerName);
                if (fleet == null) return null;

                var cell = (from c in fleet.GetShipsCells()
                            where c.Equals(shot)
                            select c).FirstOrDefault();

                // miss
                if (cell == null)
                {
                    if (playerName == this.GuestPlayerName) this.GuestMissedShots.Add(shot);
                    if (playerName == this.HostPlayerName) this.HostMissedShots.Add(shot);
                    return new Info<ShotResult>(ShotResult.Miss, "Miss");
                }
                // hit, ship destruction or second hit
                else
                {
                    if (cell.IsDestroyed) return new Info<ShotResult>(ShotResult.SecondHit, "You hit the same place twice!");
                    else
                    {
                        cell.IsDestroyed = true;
                        if (cell.Parent.IsDestroyed) return new Info<ShotResult>(ShotResult.ShipDestroyed, "Ship is destroyed!");
                        else return new Info<ShotResult>(ShotResult.Hit, "Hit!");
                    }
                }

                //return new Info<ShotResult>(ShotResult.Unknown, "Something went wrong!");
            }

            return null;
        }

        public override string GetNextPlayer(Guid gameId, string currentPlayer)
        {
            if (gameId == this.GameId.Value && currentPlayer == this.GuestPlayerName)
            {
                return this.HostPlayerName;
            }

            if (gameId == this.GameId.Value && currentPlayer == this.HostPlayerName)
            {
                return this.GuestPlayerName;
            }

            return null;
        }
    }
}

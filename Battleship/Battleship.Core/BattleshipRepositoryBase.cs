using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core
{
    public enum CellState
    {
        Empty, Destroyed, HasShip, HasMiss, Unknown
    }

    public enum ShotResult
    {
        Miss, Hit, SecondHit, ShipDestroyed
    }

    public class Info<T>
    {
        public string InfoString { get; private set; }
        public T Value { get; private set; }

        public Info(T value, string info)
        {
            this.Value = value;
            this.InfoString = info;
        }
    }

    public abstract class BattleshipRepositoryBase
    {
        public abstract Guid CreateGame(string GameName, string HostPlayerName);
        public abstract Nullable<Guid> FindGame(string GameName);
        public abstract bool JoinGame(Guid GameId, string GuestPlayerName);
        public abstract bool AddShipToFleet(Guid gameId, string playerName, string coordinates, int size);
        public abstract CellState CheckCell(Guid gameId, string playerName, int x, char y);
        public abstract int? SuggestNextShipSize(Guid gameId, string playerName);
        public abstract bool? IsFleetFull(Guid gameId, string playerName);
        public abstract bool? IsGameOver(Guid gameId);
        public abstract bool? IsGameStarted(Guid gameId);
        public abstract Info<ShotResult> TakeTurn(Guid gameId, string player, string coordinates);
        public abstract string GetNextPlayer(Guid gameId, string currentPlayer);
    }
}

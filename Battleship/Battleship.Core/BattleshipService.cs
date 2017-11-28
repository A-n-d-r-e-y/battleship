using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core
{
    public class BattleshipService
    {
        private readonly BattleshipRepositoryBase repository;

        public bool IsGameEnded(Guid GameId)
        {
            return repository.IsGameOver(GameId).Value;
        }

        public BattleshipService(BattleshipRepositoryBase repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");

            this.repository = repository;
        }

        public bool CreateGame(string GameName, string FirstPlayerName, string SecondPlayerName)
        {
            var game = repository.CreateGame(GameName, FirstPlayerName);
            return repository.JoinGame(game, SecondPlayerName);
        }

        public Nullable<Guid> FindGameByName(string GameName)
        {
            return repository.FindGame(GameName);
        }

        public bool AddShipToPlayersFleet(Guid GameId, string PlayerName, string coordinates, int size)
        {
            try
            {
                return repository.AddShipToFleet(GameId, PlayerName, coordinates, size);
            }
            catch
            {
                return false;
            }
        }

        public CellState CheckCell(Guid GameId, string playerName, int X, char Y)
        {
            return repository.CheckCell(GameId, playerName, X, Y);
        }

        public Tuple<int, string> SuggestNextShip(Guid GameId, string PlayerName)
        {
            int? size = repository.SuggestNextShipSize(GameId, PlayerName);
            string name = GetShipNameBySize(size.Value);

            return new Tuple<int, string>(size.Value, name);
        }

        public bool? IsFleetFull(Guid gameId, string playerName)
        {
            return repository.IsFleetFull(gameId, playerName);
        }

        public Info<ShotResult> TakeTurn(Guid gameId, string player, string coordinates)
        {
            return repository.TakeTurn(gameId, player, coordinates);
        }

        public string GetNextPlayerToTurn(Guid gameId, string currentPlayer)
        {
            return repository.GetNextPlayer(gameId, currentPlayer);
        }

        //TODO move it to the repository base
        private string GetShipNameBySize(int size)
        {
            switch (size)
            {
                case 1: return "Submarine";
                case 2: return "Destroyer";
                case 3: return "Cruiser";
                case 4: return "Battleship";
                case 5: return "Aircraft Carrier";
                default: return null;
            }
        }
    }
}

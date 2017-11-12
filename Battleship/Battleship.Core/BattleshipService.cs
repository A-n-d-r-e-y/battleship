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

        public bool CheckShip(Guid GameId, int X, char Y)
        {
            return repository.CheckShip(GameId, X, Y);
        }

        public Tuple<int, string> SuggestNextShip(Guid GameId, string PlayerName)
        {
            int? size = repository.SuggestNextShipSize(GameId, PlayerName);
            string name = GetShipNameBySize(size.Value);

            return new Tuple<int, string>(size.Value, name);
        }

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

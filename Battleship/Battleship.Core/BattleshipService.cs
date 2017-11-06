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

        public Guid FindGameByName(string GameName)
        {
            return repository.FindGame(GameName);
        }
    }
}

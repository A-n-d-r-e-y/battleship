using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core
{
    public abstract class BattleshipRepositoryBase
    {
        public abstract Guid CreateGame(string GameName, string HostPlayerName);
        public abstract Guid FindGame(string GameName);
        public abstract bool JoinGame(Guid GameId, string GuestPlayerName);
    }
}

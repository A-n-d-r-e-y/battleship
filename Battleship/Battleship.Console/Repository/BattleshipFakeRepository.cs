using Battleship.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console
{
    public class BattleshipFakeRepository : BattleshipRepositoryBase
    {
        public override Guid CreateGame(string GameName, string HostPlayerName)
        {
            return Guid.NewGuid();
        }

        public override Guid FindGame(string GameName)
        {
            return Guid.NewGuid();
        }

        public override bool JoinGame(Guid GameId, string GuestPlayerName)
        {
            return true;
        }
    }
}

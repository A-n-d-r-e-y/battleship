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
        private Nullable<Guid> GameId;
        private string GameName;
        private string HostPlayerName;
        private string GuestPlayerName;

        public override Guid CreateGame(string GameName, string HostPlayerName)
        {
            this.GameId = new Nullable<Guid>(Guid.NewGuid());
            this.GameName = GameName;
            this.HostPlayerName = HostPlayerName;

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
    }
}

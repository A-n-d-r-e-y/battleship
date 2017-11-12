﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Core
{
    public abstract class BattleshipRepositoryBase
    {
        public abstract Guid CreateGame(string GameName, string HostPlayerName);
        public abstract Nullable<Guid> FindGame(string GameName);
        public abstract bool JoinGame(Guid GameId, string GuestPlayerName);
        public abstract bool AddShipToFleet(Guid gameId, string playerName, string coordinates);
        public abstract bool CheckShip(Guid gameId, int x, char y);
    }
}

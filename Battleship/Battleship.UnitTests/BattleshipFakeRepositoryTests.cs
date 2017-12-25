using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship.Core;
using Battleship.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Battleship.UnitTests
{
    [TestClass]
    public class BattleshipFakeRepositoryTests
    {
        private BattleshipFakeRepository repository;
        private  BattleshipService service;

        private string GameName = "game";
        private string Player1 = "player1";
        private string Player2 = "player2";

        private Dictionary<int, string> FIRST_PLAYER_FLEET = new Dictionary<int, string>(7);

        [TestInitialize]
        public void InitializeTests()
        {
            repository = new BattleshipFakeRepository();
            service = new BattleshipService(repository);

            CreateFirstPlayerFleet();
        }

        private void CreateFirstPlayerFleet()
        {
            FIRST_PLAYER_FLEET.Add(1, "b2,b3,b4,b5,b6");
            FIRST_PLAYER_FLEET.Add(2, "d4,d5,d6,d7");
            FIRST_PLAYER_FLEET.Add(3, "f1,f2,f3");
            FIRST_PLAYER_FLEET.Add(4, "a8,b8");
            FIRST_PLAYER_FLEET.Add(5, "d10,e10");
            FIRST_PLAYER_FLEET.Add(6, "g9");
            FIRST_PLAYER_FLEET.Add(7, "j7");
        }

        private bool Contains(Dictionary<int, string> dict, char x, int y)
        {
            return (
                from v in dict.Values
                from s in v.Split(',')
                select new
                {
                    x = char.Parse(s.Substring(0, 1)),
                    y = int.Parse(s.Length == 2 ? s.Substring(1, 1) : s.Substring(1, 2)),
                }
                into raw
                where raw.x == x && raw.y == y
                select raw).Count() > 0;
        }

        private void DrawFleet(Dictionary<int, string> fleet)
        {
            var sb = new StringBuilder()
                .AppendLine()
                .Append(" \t1   2   3   4   5   6  7  8  9  10");

            for (char c = 'a'; c <= 'j'; c++)
            {
                sb
                    .AppendLine()
                    .AppendFormat("{0}\t", char.ToUpper(c));

                for (int i = 1; i <= 10; i++)
                {
                    sb.Append(this.Contains(fleet, c, i) ? "#  " : " *  ");
                }
            }

            sb.AppendLine();
            Debug.WriteLine(sb.ToString());
        }

        private static void DrawField(BattleshipService service, Guid? gameId, string player, string caption)
        {
            var sb = new StringBuilder()
                .AppendLine()
                .AppendLine(caption)
                .AppendLine()
                .Append("  1 2 3 4 5 6 7 8 9 10");

            for (char c = 'a'; c <= 'j'; c++)
            {
                sb
                    .AppendLine()
                    .AppendFormat("{0} ", char.ToUpper(c));

                for (int i = 1; i <= 10; i++)
                {
                    var result = service.CheckCell(gameId.Value, player, i, c);

                    switch (result)
                    {
                        case CellState.Empty:
                            sb.Append("* ");
                            break;
                        case CellState.Destroyed:
                            sb.Append("# ");
                            break;
                        case CellState.HasShip:
                            sb.Append("+ ");
                            break;
                        case CellState.Unknown:
                            sb.Append("? ");
                            break;
                        case CellState.HasMiss:
                            sb.Append("@ ");
                            break;
                        default:
                            break;
                    }
                }
            }

            sb.AppendLine();
            Debug.WriteLine(sb.ToString());
        }

        [TestMethod]
        public void NormalGameScenarioTest()
        {
            bool isGameCreated = service.CreateGame(GameName, Player1, Player2);
            Assert.IsTrue(isGameCreated);

            var gameId = service.FindGameByName(GameName).Value;
            Assert.IsNotNull(gameId);

            Assert.IsFalse(service.IsFleetFull(gameId, Player1).Value);

            var shipInfo = service.SuggestNextShipToAdd(gameId, Player1);
            Assert.AreEqual<ShipType>(ShipType.AircraftCarrier, shipInfo.ShipType);
            Assert.AreEqual<int>(5, shipInfo.ShipSize);

            bool isShipAdded = service.AddShipToPlayersFleet(gameId, Player1, "a1,a2,a3,a4", shipInfo);
            Assert.IsFalse(isShipAdded, "Ship size by coordinates doesn't match shipInfo.Size");

            isShipAdded = service.AddShipToPlayersFleet(gameId, Player1, "a1,a2,a3,a4,a5", shipInfo);
            Assert.IsTrue(isShipAdded);

            Assert.IsFalse(service.IsGameStarted(gameId), "Not all ships are added");
            Assert.IsFalse(service.IsGameEnded(gameId), "Neather of the players have their fleets destroyed");

            Assert.IsTrue(service.TakeTurn(gameId, Player2, "b3").Value == ShotResult.Miss, "so far there are no ships on this coordinate");

            //DrawField
            //DrawFleet(FIRST_PLAYER_FLEET);
        }

        //private void CreateFleetForPlayer(BattleshipService service, string playerName, Guid? gameId)
        //{
        //    while (!service.IsFleetFull(gameId.Value, playerName).Value)
        //    {
        //        var shipInfo = service.SuggestNextShip(gameId.Value, playerName);
        //        string coordinates;

        //        do
        //        {
        //            //Debug.Clear();
        //            Debug.WriteLine(string.Format("{2}, please enter coordinates for a {0} size[{1}].", shipInfo.Item2, shipInfo.Item1, playerName));
        //            coordinates = Debug.ReadLine();
        //        }
        //        while (!service.AddShipToPlayersFleet(gameId.Value, playerName, coordinates, shipInfo.Item1));

        //        DrawField(service, gameId, playerName, "Ship was successfully created!");
        //    }
        //}
    }
}

﻿using Battleship.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new BattleshipFakeRepository();
            var service = new BattleshipService(repository);

            System.Console.WriteLine("Please, enter a name for the game:");
            string GameName = System.Console.ReadLine();

            System.Console.WriteLine("Please, enter a name for the player one:");
            string Player1 = System.Console.ReadLine();

            System.Console.WriteLine("Please, enter a name for the player two:");
            string Player2 = System.Console.ReadLine();


            Guid? gameId = null;

            if (!service.CreateGame(GameName, Player1, Player2))
            {
                System.Console.WriteLine("By some reason the game was not created!");
                System.Console.WriteLine("The game is over");
                System.Console.ReadKey();
                return;
            }

            gameId = service.FindGameByName(GameName);
            System.Console.WriteLine("Game successfully created!");
            System.Console.WriteLine(String.Format("Game id is: {0}", gameId.Value));
            System.Console.WriteLine();

            // creating fleets
            foreach (var player in new string[] { Player1, Player2 })
            {
                DrawField(service, gameId, player, string.Format("{0} - create your fleet!", player)); // <--- add player parameter!!!
                CreateFleetForPlayer(service, player, gameId);
            }

            // rolling dices
            // the winner takes the first step
            string currentPlayer = Player1;

            // the game cycle
            while (!service.IsGameEnded(gameId.Value))
            {

                System.Console.WriteLine(string.Format("{0} - it's your turn!", currentPlayer));
                Info<ShotResult> shotInfo;
                var misses = new[] { ShotResult.Miss, ShotResult.SecondHit };

                do
                {
                    System.Console.WriteLine(string.Format("{0}, please enter coordinates for a shot!", currentPlayer));
                    string coordinates = System.Console.ReadLine();
                    shotInfo = service.TakeTurn(gameId.Value, currentPlayer, coordinates);

                    if (shotInfo == null) continue;

                    DrawField(service, gameId, currentPlayer, shotInfo.InfoString);
                }
                while (misses.Contains(shotInfo.Value)); // while you miss

                // next player
                currentPlayer = service.GetNextPlayerToTurn(gameId.Value, currentPlayer);
            }

            System.Console.WriteLine("The end!");
            System.Console.ReadKey();
        }

        private static void CreateFleetForPlayer(BattleshipService service, string playerName, Guid? gameId)
        {
            while (!service.IsFleetFull(gameId.Value, playerName).Value)
            {
                var shipInfo = service.SuggestNextShipToAdd(gameId.Value, playerName);
                string coordinates;

                do
                {
                    //System.Console.Clear();
                    System.Console.WriteLine(string.Format("{2}, please enter coordinates for a {0} size[{1}].", shipInfo.ShipType, shipInfo.ShipSize, playerName));
                    coordinates = System.Console.ReadLine();
                }
                while (!service.AddShipToPlayersFleet(gameId.Value, playerName, coordinates, shipInfo));

                DrawField(service, gameId, playerName, "Ship was successfully created!");
            }
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
            System.Console.WriteLine(sb.ToString());
        }
    }
}

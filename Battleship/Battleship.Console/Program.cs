using Battleship.Core;
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
                DrawField(service, gameId, string.Format("{0} - create your fleet!", player));
                CreateFleetForPlayer(service, player, gameId);
            }

            // the game cycle
            while (!service.IsGameEnded)
            {
                // rolling dices

                // the winner takes the first step
                string player = service.GetNextPlayerToTurn(gameId.Value);
                System.Console.WriteLine(string.Format("{0} - it's your turn!", player));
                Tuple<bool, string> shotInfo;

                do
                {
                    System.Console.WriteLine(string.Format("{0}, please enter coordinates for a shot!", player));
                    string coordinates = System.Console.ReadLine();
                    shotInfo = service.TakeTurn(gameId, player, coordinates);

                    if (shotInfo == null) continue;

                    DrawField(service, gameId, shotInfo.Item2);
                }
                while (shotInfo.Item1);

                
            }

            System.Console.WriteLine("The end!");
            System.Console.ReadKey();
        }

        private static void CreateFleetForPlayer(BattleshipService service, string playerName, Guid? gameId)
        {
            while (!service.IsFleetFull(gameId.Value, playerName).Value)
            {
                var shipInfo = service.SuggestNextShip(gameId.Value, playerName);
                string coordinates;

                do
                {
                    //System.Console.Clear();
                    System.Console.WriteLine(string.Format("{2}, please enter coordinates for a {0} size[{1}].", shipInfo.Item2, shipInfo.Item1, playerName));
                    coordinates = System.Console.ReadLine();
                }
                while (!service.AddShipToPlayersFleet(gameId.Value, playerName, coordinates, shipInfo.Item1));

                DrawField(service, gameId, "Ship was successfully created!");
            }
        }

        private static void DrawField(BattleshipService service, Guid? gameId, string caption)
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
                    sb.AppendFormat(service.CheckShip(gameId.Value, i, c) ? "+ " : "* ", i);
                }
            }

            sb.AppendLine();
            System.Console.WriteLine(sb.ToString());
        }
    }
}

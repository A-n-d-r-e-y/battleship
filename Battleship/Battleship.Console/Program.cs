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


            if (service.CreateGame(GameName, Player1, Player2))
            {
                System.Console.WriteLine("Game successfully created!");
                System.Console.WriteLine(String.Format("Game id is: {0}", service.FindGameByName(GameName)));
            }


            var field = new Dictionary<string, bool>();
            var sb = new StringBuilder();
            sb.Append("  1 2 3 4 5 6 7 8 9 10");

            for (char c = 'a'; c <= 'j'; c++)
            {
                sb.AppendLine();
                sb.AppendFormat("{0} ", char.ToUpper(c));
                for (int i = '0'; i <= '9'; i++)
                {
                    field.Add(string.Format("{0}{1}", c, i), false);

                    sb.AppendFormat("* ", i);
                }
            }

            System.Console.WriteLine(sb.ToString());

            System.Console.ReadKey();
        }
    }
}

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

            System.Console.ReadKey();
        }
    }
}

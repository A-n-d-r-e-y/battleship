using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship.Core;
using Battleship.Console;

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

        [TestInitialize]
        public void InitializeTests()
        {
            repository = new BattleshipFakeRepository();
            service = new BattleshipService(repository);
        }

        [TestMethod]
        public void CreateGameTest()
        {
            Assert.IsTrue(service.CreateGame(GameName, Player1, Player2));
        }

        [TestMethod]
        public void FindGameTest()
        {
            if (service.CreateGame(GameName, Player1, Player2))
            {
                Assert.IsNotNull(service.FindGameByName(GameName));
            }
        }
    }
}

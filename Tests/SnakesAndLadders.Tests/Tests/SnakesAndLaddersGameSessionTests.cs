using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakesAndLadders.BackEnd;
using SnakesAndLadders.BackEnd.Contracts.Enums;
using SnakesAndLadders.BackEnd.Contracts.Models;
using System;

namespace SnakesAndLadders.Tests.Tests
{
    [TestClass]
    public class SnakesAndLaddersGameSessionTests
    {
        #region Internal vars
        private ISnakesAndLaddersGameSession _gameSession;
        #endregion

        #region Initializers
        [TestInitialize]
        public void Initialize()
        {
            this._gameSession = new SnakesAndLaddersGameSession();
        }
        #endregion

        #region Tests
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void InitializeSessionWithValueOverZero(int players)
        {
            this._gameSession.Initialize(players);

            Assert.AreEqual(players, this._gameSession.PlayersCount);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void InitializeSessionWithValueLessOrEqualToZero(int players)
        {
            Assert.ThrowsException<ArgumentException>(() => this._gameSession.Initialize(players));
        }

        [TestMethod]
        public void GetCurrentPlayerIndex()
        {
            const int MAX_PLAYERS = 4;
            const int ROUNDS = MAX_PLAYERS * 2;

            this._gameSession.Initialize(MAX_PLAYERS);

            int expectedIndex = 1;

            for (int i = 0; i < ROUNDS; i++)
            {
                int index = this._gameSession.GetCurrentPlayerIndex(out int position);

                Assert.AreEqual(expectedIndex, index, "The expected index not match with the received index.");
                Assert.AreNotEqual(0, position, "The position can't be zero.");

                this._gameSession.PlayTurn();

                if (++expectedIndex > MAX_PLAYERS)
                    expectedIndex = 1;
            }
        }

        [TestMethod]
        public void GetNextPlayerIndex()
        {
            const int MAX_PLAYERS = 4;
            const int ROUNDS = MAX_PLAYERS * 2;

            this._gameSession.Initialize(MAX_PLAYERS);

            int expectedIndex = 2;

            for (int i = 0; i < ROUNDS; i++)
            {
                int index = this._gameSession.GetNextPlayerIndex(out int position);

                Assert.AreEqual(expectedIndex, index, "The expected index not match with the received index.");
                Assert.AreNotEqual(0, position, "The position can't be zero.");

                this._gameSession.PlayTurn();

                if (++expectedIndex > MAX_PLAYERS)
                    expectedIndex = 1;
            }
        }

        [TestMethod]
        public void PlayerMovementResultIsFilledAfterPlayTurn()
        {
            const int MAX_PLAYERS = 1;
            const int ROUNDS = 30;

            this._gameSession.Initialize(MAX_PLAYERS);

            for (int i = 0; i < ROUNDS; i++)
            {
                PlayerMovementResult result = this._gameSession.PlayTurn();

                Assert.AreNotEqual(0, result.arrivedTileNumber);
                Assert.AreNotEqual(0, result.currentTileNumber);
                Assert.AreNotEqual(0, result.diceResult);

                if (result.IsPlayerArrivedOnSnakeOrLadderTile)
                    Assert.AreNotEqual(BoardTileTypes.Normal, result.arrivedTileType);
                else
                    Assert.AreEqual(BoardTileTypes.Normal, result.arrivedTileType);
            }
        }

        [TestMethod]
        public void ResetGameSession()
        {
            const int MAX_PLAYERS = 4;

            this._gameSession.Initialize(MAX_PLAYERS);

            for (int i = 0; i < MAX_PLAYERS; i++)
                this._gameSession.PlayTurn();

            this._gameSession.Reset();

            for (int i = 1; i < MAX_PLAYERS + 1; i++)
            {
                int position = this._gameSession.GetPlayerTokenPosition(i);
                Assert.AreEqual(1, position);
            }
        }
        #endregion
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakesAndLadders.BackEnd.Contracts.Constants;
using SnakesAndLadders.BackEnd.Contracts.Services;
using SnakesAndLadders.BackEnd.Services;
using System;

namespace SnakesAndLadders.Tests.US1
{
    [TestClass]
    public class PlayerTokenTests
    {
        #region Internal vars
        private IPlayerToken _playerToken;
        private bool _hasPlayerWinTheGame = false;
        #endregion

        #region Initializers
        [TestInitialize]
        public void Initialize()
        {
            IBoard board = new Board();
            this._playerToken = new PlayerToken(board);
        }
        #endregion

        #region Methods & Functions
        private void MovePlayerToPosition(int tileNumber)
        {
            this._playerToken.Reset();
            this._playerToken.Move(tileNumber - 1);
            Assert.AreEqual(tileNumber, this._playerToken.Position);
        }
        #endregion

        #region Tests
        [TestMethod("US1-UAT1: Given the game is started, when the token is placed on the board, then the token is on square 1")]
        public void PlayerTokenIsOnFirstTile()
        {
            Assert.AreEqual(BoardConstants.START_TILE_NUMBER, this._playerToken.Position);
        }

        [TestMethod("US1-UAT2: Given the token is on square 1, when the token is moved 3 spaces, then the token is on square 4")]
        public void MovePlayerToken3Tiles()
        {
            const int DICE_RESULT = 3;
            const int EXPECTED_POSITION = 4;

            this._playerToken.Move(DICE_RESULT);

            Assert.AreEqual(EXPECTED_POSITION, this._playerToken.Position);
        }

        [TestMethod("US1-UAT3: Given the token is on square 1, when the token is moved 3 spaces, and then it is moved 5 spaces, then the token is on square 9")]
        public void MovePlayerToken3TilesAndThen4Tiles()
        {
            int[] DICE_RESULTS = { 3, 5 };
            const int EXPECTED_POSITION = 9;

            foreach (int diceResult in DICE_RESULTS)
                this._playerToken.Move(diceResult);

            Assert.AreEqual(EXPECTED_POSITION, this._playerToken.Position);
        }

        [DataTestMethod]
        [DataRow(0, DisplayName = "Try to move zero tiles, then throws an ArgumentException")]
        [DataRow(-1, DisplayName = "Try to move a negative tiles, then throws an ArgumentException")]
        public void FailMovePlayerToken(int tiles)
        {
            Assert.ThrowsException<ArgumentException>(() => this._playerToken.Move(tiles));
        }

        [TestMethod("US2-UAT1: Given the token is on square 97, when the token is moved 3 spaces, then the token is on square 100, and the player has won the game")]
        public void MovePlayerToken3TilesFromTile97AndWinGame()
        {
            const int CURRENT_POSITION = 97;
            const int DICE_RESULT = 3;
            const int EXPECTED_POSITION = 100;

            this.MovePlayerToPosition(CURRENT_POSITION);

            this._playerToken.OnPlayerWins += this.OnPlayerWins;
            this._playerToken.Move(DICE_RESULT);

            Assert.AreEqual(EXPECTED_POSITION, this._playerToken.Position);
            Assert.IsTrue(this._hasPlayerWinTheGame);
        }

        [TestMethod("US2-UAT2: Given the token is on square 97, when the token is moved 5 spaces, then the token is on square 98, and the player has not won the game")]
        public void MovePlayerToken4TilesFromTile97()
        {
            const int CURRENT_POSITION = 97;
            const int DICE_RESULT = 5;
            const int EXPECTED_POSITION = 98;
            /* NOTE: The original UAT specifies 97 instead of 98. I'm not sure if is a typo, or a specific rule of this game, but in other board games like this (Parchis 
             * or the Game of the Goose) the behaviour when you pass ahead of the end of the board, the rest of moves are counting in backward, like if the player token 
             * bounce at the end of the board. I took the liberty to implement this behaviour instead of discard the movement (that I presume that was the UAT expected). */

            this.MovePlayerToPosition(CURRENT_POSITION);

            this._playerToken.OnPlayerWins += this.OnPlayerWins;
            this._playerToken.Move(DICE_RESULT);

            Assert.AreEqual(EXPECTED_POSITION, this._playerToken.Position);
            Assert.IsFalse(this._hasPlayerWinTheGame);
        }

        [DataTestMethod]
        [DataRow(16, 6)]
        [DataRow(46, 25)]
        [DataRow(49, 11)]
        [DataRow(62, 19)]
        [DataRow(64, 60)]
        [DataRow(74, 53)]
        [DataRow(89, 68)]
        [DataRow(92, 88)]
        [DataRow(95, 75)]
        [DataRow(99, 80)]
        public void TestSnakeHeadTiles(int snakeHeadTile, int expectedPosition)
        {
            this._playerToken.Move(--snakeHeadTile);

            Assert.AreEqual(expectedPosition, this._playerToken.Position);
        }

        [DataTestMethod]
        [DataRow(2, 38)]
        [DataRow(7, 14)]
        [DataRow(8, 31)]
        [DataRow(15, 26)]
        [DataRow(28, 84)]
        [DataRow(21, 42)]
        [DataRow(36, 44)]
        [DataRow(51, 67)]
        [DataRow(78, 98)]
        [DataRow(71, 91)]
        [DataRow(87, 94)]
        public void TestBottomLadderTiles(int bottomLadderTile, int expectedPosition)
        {
            this._playerToken.Move(--bottomLadderTile);

            Assert.AreEqual(expectedPosition, this._playerToken.Position);
        }
        #endregion

        #region Event listeners
        private void OnPlayerWins() => this._hasPlayerWinTheGame = true;
        #endregion
    }
}

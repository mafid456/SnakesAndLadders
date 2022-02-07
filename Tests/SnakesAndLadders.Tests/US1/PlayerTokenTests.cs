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
        #endregion

        #region Initializers
        [TestInitialize]
        public void Initialize()
        {
            IBoard board = new Board();
            this._playerToken = new PlayerToken(board);
        }
        #endregion

        #region Tests
        [TestMethod("UAT1: Given the game is started, when the token is placed on the board, then the token is on square 1")]
        public void PlayerTokenIsOnFirstTile()
        {
            Assert.AreEqual(BoardConstants.START_TILE_NUMBER, this._playerToken.Position);
        }

        [TestMethod("UAT2: Given the token is on square 1, when the token is moved 3 spaces, then the token is on square 4")]
        public void MovePlayerToken3Tiles()
        {
            const int DICE_RESULT = 3;
            const int EXPECTED_POSITION = 4;

            this._playerToken.Move(DICE_RESULT);

            Assert.AreEqual(EXPECTED_POSITION, this._playerToken.Position);
        }

        [TestMethod("UAT3: Given the token is on square 1, when the token is moved 3 spaces, and then it is moved 4 spaces, then the token is on square 8")]
        public void MovePlayerToken3TilesAndThen4Tiles()
        {
            int[] DICE_RESULTS = { 3, 4 };
            const int EXPECTED_POSITION = 8;

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
        #endregion
    }
}

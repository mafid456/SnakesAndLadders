using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakesAndLadders.BackEnd.Contracts.Services;
using SnakesAndLadders.BackEnd.Services;
using SnakesAndLadders.Tests.Mocks;

namespace SnakesAndLadders.Tests
{
    [TestClass]
    public class DiceTests
    {
        #region Tests
        [TestMethod("US3-UAT1: Given the game is started, when the player rolls a die, then the result should be between 1 - 6 inclusive")]
        public void RollDiceAlwaysReturnsANumberBetween1And6Inclusive()
        {
            const int MIN_DICE_VALUE = 1;
            const int MAX_DICE_VALUE = 6;

            IDice dice = new Dice();

            for (int i = 0; i < 1000; i++)
            {
                int number = dice.Roll();
                Assert.IsTrue(number >= MIN_DICE_VALUE && number <= MAX_DICE_VALUE);
            }
        }

        [TestMethod("US3-UAT2: Given the player rolls a 4, when they move their token, then the token should move 4 spaces")]
        public void RollsDiceGets4AndPlayerTokenMoves4Tiles()
        {
            const int EXPECTED_DICE_RESULT = 4;
            const int EXPECTED_PLAYER_POSITION = 5;

            IDice dice = new Mock4Dice();
            IBoard board = new Board();
            IPlayerToken playerToken = new PlayerToken(board);

            int number = dice.Roll();
            Assert.AreEqual(EXPECTED_DICE_RESULT, number);

            playerToken.Move(number);
            Assert.AreEqual(EXPECTED_PLAYER_POSITION, playerToken.Position);
        }
        #endregion
    }
}

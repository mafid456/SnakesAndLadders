using SnakesAndLadders.BackEnd.Contracts.Services;

namespace SnakesAndLadders.Tests.Mocks
{
    public class Mock4Dice : IDice
    {
        #region Methods & Functions
        public int Roll() => 4;
        #endregion
    }
}

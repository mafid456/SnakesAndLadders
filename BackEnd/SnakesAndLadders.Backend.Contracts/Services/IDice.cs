namespace SnakesAndLadders.BackEnd.Contracts.Services
{
    /// <summary>
    /// Dice service.
    /// </summary>
    /// <remarks>This represents the dice used to get the tiles to move a <see cref="IPlayerToken"/> on a <see cref="IBoard"/>.</remarks>
    public interface IDice
    {
        #region Methods & Functions
        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <returns>Returns a random number between 1 and 6, both inclusive.</returns>
        int Roll();
        #endregion
    }
}

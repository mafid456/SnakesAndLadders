namespace SnakesAndLadders.BackEnd.Contracts.Constants
{
    /// <summary>
    /// <see cref="Services.IBoard"/> related constants.
    /// </summary>
    public static class BoardConstants
    {
        #region Constants
        /// <summary>
        /// Maximum number of <see cref="Models.BoardTile"/>s that contains a <see cref="Services.IBoard"/> object.
        /// </summary>
        public const int MAX_BOARD_TILES = 100;

        /// <summary>
        /// First valid tile number in board.
        /// </summary>
        public const int START_TILE_NUMBER = 1;

        /// <summary>
        /// Last valid tile number in board.
        /// </summary>
        public const int FINISH_TILE_NUMBER = BoardConstants.MAX_BOARD_TILES;
        #endregion
    }
}

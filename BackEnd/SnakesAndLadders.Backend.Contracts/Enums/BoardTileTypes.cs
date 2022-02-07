namespace SnakesAndLadders.BackEnd.Contracts.Enums
{
    /// <summary>
    /// List of board <see cref="Models.BoardTile"/> types.
    /// </summary>
    public enum BoardTileTypes
    {
        /// <summary>
        /// Normal tile.
        /// </summary>
        Normal,

        /// <summary>
        /// Snake head tile.
        /// </summary>
        /// <remarks>The snake head tile defines the start end-point to player token jump to another <see cref="Models.BoardTile"/> position.</remarks>
        Snake,

        /// <summary>
        /// Bottom ladder tile.
        /// </summary>
        /// <remarks>The bottom ladder tile defines the start end-point to player token jump to another <see cref="Models.BoardTile"/> position.</remarks>
        Ladder,
    }
}

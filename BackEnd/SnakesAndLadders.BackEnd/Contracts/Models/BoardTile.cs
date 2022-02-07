using SnakesAndLadders.BackEnd.Contracts.Enums;

namespace SnakesAndLadders.BackEnd.Contracts.Models
{
    /// <summary>
    /// Defines a board tile data.
    /// </summary>
    public struct BoardTile
    {
        #region Public vars
        /// <summary>
        /// The type of the board tile.
        /// </summary>
        public readonly BoardTileTypes type;

        /// <summary>
        /// The number of the board tile target.
        /// </summary>
        /// <remarks>This value is required when the board tile is a <see cref="BoardTileTypes.Snake"/> or <see cref="BoardTileTypes.Ladder"/>.</remarks>
        public readonly int jumpToTileNumber;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the tile data.
        /// </summary>
        /// <param name="type">Sets the tile type.</param>
        /// <param name="jumpToTileNumber">Sets the target tile number (not the array index).</param>
        /// <remarks>Use this constructor when needs to setup a snake head or bottom ladder tile.</remarks>
        public BoardTile(BoardTileTypes type, int jumpToTileNumber)
        {
            this.type = type;
            this.jumpToTileNumber = jumpToTileNumber;
        }
        #endregion
    }
}

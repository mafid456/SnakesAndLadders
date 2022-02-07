using SnakesAndLadders.BackEnd.Contracts.Models;

namespace SnakesAndLadders.BackEnd.Contracts.Services
{
    /// <summary>
    /// Game board service.
    /// </summary>
    /// <remarks>This represents the game board with all tile data.</remarks>
    public interface IBoard
    {
        #region Properties
        /// <summary>
        /// Read-only array that contains all <see cref="BoardTile"/>s data.
        /// </summary>
        BoardTile[] Tiles { get; }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Gets the <see cref="BoardTile"/> using his number position in the board.
        /// </summary>
        /// <param name="tileNumber">The number of the tile in the board.</param>
        /// <returns>Returns the <see cref="BoardTile"/> data of the desired tile.</returns>
        BoardTile GetTileByNumberPosition(int tileNumber);
        #endregion
    }
}

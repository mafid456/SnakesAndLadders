using SnakesAndLadders.BackEnd.Contracts.Enums;
using SnakesAndLadders.BackEnd.Contracts.Services;

namespace SnakesAndLadders.BackEnd.Contracts.Models
{
    /// <summary>
    /// The <see cref="IPlayerToken"/> movement result.
    /// </summary>
    /// <remarks>This data stores the all statistics of the <see cref="IPlayerToken"/> after roll the <see cref="IDice"/> and completes a turn.</remarks>
    public struct PlayerMovementResult
    {
        #region Public vars
        /// <summary>
        /// The result of the dice after roll it.
        /// </summary>
        public readonly int diceResult;

        /// <summary>
        /// Tile number where the <see cref="IPlayerToken"/> arrived after it moved.
        /// </summary>
        public readonly int arrivedTileNumber;

        /// <summary>
        /// The tile type the <see cref="IPlayerToken"/> arrived after it moved.
        /// </summary>
        public readonly BoardTileTypes arrivedTileType;

        /// <summary>
        /// The current tile number where the <see cref="IPlayerToken"/> is placed.
        /// </summary>
        /// <remarks>This value is the final tile position after it moved and jump to final tile if reach a <see cref="BoardTileTypes.Snake"/> head or bottom <see cref="BoardTileTypes.Ladder"/> tile.</remarks>
        public readonly int currentTileNumber;
        #endregion

        #region Properties
        /// <summary>
        /// Gets if the <see cref="IPlayerToken"/> has arrived on a <see cref="BoardTileTypes.Snake"/> head or bottom <see cref="BoardTileTypes.Ladder"/> tile after it moved.
        /// </summary>
        public bool IsPlayerArrivedOnSnakeOrLadderTile => this.arrivedTileType != BoardTileTypes.Normal;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the player movement result data.
        /// </summary>
        /// <param name="diceResult">The result of the dice after roll it.</param>
        /// <param name="arrivedTileNumber">Tile number where the <see cref="IPlayerToken"/> arrived after it moved.</param>
        /// <param name="arrivedTileType">The tile type the <see cref="IPlayerToken"/> arrived after it moved.</param>
        /// <param name="currentTileNumber">The current tile number where the <see cref="IPlayerToken"/> is placed.</param>
        public PlayerMovementResult(int diceResult, int arrivedTileNumber, BoardTileTypes arrivedTileType, int currentTileNumber)
        {
            this.diceResult = diceResult;
            this.arrivedTileNumber = arrivedTileNumber;
            this.arrivedTileType = arrivedTileType;
            this.currentTileNumber = currentTileNumber;
        }
        #endregion
    }
}

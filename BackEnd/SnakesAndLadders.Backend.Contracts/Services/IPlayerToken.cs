using SnakesAndLadders.BackEnd.Contracts.Enums;
using SnakesAndLadders.BackEnd.Contracts.Models;
using System;

namespace SnakesAndLadders.BackEnd.Contracts.Services
{
    /// <summary>
    /// Player token service.
    /// </summary>
    /// <remarks>This represents the player token used to move on a <see cref="IBoard"/>.</remarks>
    public interface IPlayerToken
    {
        #region Properties
        /// <summary>
        /// Current position on the board (the tile number, not his index in the array).
        /// </summary>
        int Position { get; }
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the player reach the last tile in the board.
        /// </summary>
        event Action OnPlayerWins;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Moves the token a number of tiles in the board.
        /// </summary>
        /// <param name="tiles">Number of tiles that the token must be moved.</param>
        /// <returns>Returns the stats of the player movement, that include the dice roll result, if the player token has arrived to a <see cref="BoardTileTypes.Snake"/> head 
        /// or bottom <see cref="BoardTileTypes.Ladder"/> tile, his position, and the final tile position.</returns>
        /// <remarks>Use this method when needs to move the token using a roll dice result.</remarks>
        PlayerMovementResult Move(int tiles);

        /// <summary>
        /// Resets the player token to the start tile in the board.
        /// </summary>
        void Reset();
        #endregion
    }
}

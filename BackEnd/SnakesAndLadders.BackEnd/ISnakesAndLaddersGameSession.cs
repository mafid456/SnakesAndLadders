using SnakesAndLadders.BackEnd.Contracts.Models;
using System;

namespace SnakesAndLadders.BackEnd
{
    /// <summary>
    /// A Snakes and Ladders game session.
    /// </summary>
    /// <remarks>This represents a game session with all logic required to play the game. 
    /// This object is fully independent, the program not needed to know the inner services, and is ready to use.</remarks>
    public interface ISnakesAndLaddersGameSession
    {
        #region Properties
        /// <summary>
        /// Gets the game board data.
        /// </summary>
        /// <remarks>This array provides the data of each game board tile.</remarks>
        BoardTile[] GameBoardData { get; }
        #endregion

        #region Events
        /// <summary>
        /// Notifies when a player token reach the finish tile in the game board.
        /// </summary>
        /// <remarks>Returns the player token index [1..n] that are win the game.</remarks>
        event Action<int> OnPlayerWins;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Initializes a new game session.
        /// </summary>
        /// <param name="players">The number of players that want to play this game session.</param>
        void Initialize(int players);

        /// <summary>
        /// Gets the current player token data.
        /// </summary>
        /// <param name="position">Output parameter that returns the player position in the board.</param>
        /// <returns>Returns the current player token index [1..n].</returns>
        int GetCurrentPlayerIndex(out int position);

        /// <summary>
        /// Gets the player token index for next turn.
        /// </summary>
        /// <param name="position">Output parameter that returns the player position in the board.</param>
        /// <returns>Returns the player token index [1..n] for the next turn.</returns>
        int GetNextPlayerIndex(out int position);

        /// <summary>
        /// Plays the current turn.
        /// </summary>
        /// <returns>Returns the <see cref="PlayerMovementResult"/> result after finished the current player token movement.</returns>
        /// <remarks>This method performs the roll dice action and moves the current player token over the game board. 
        /// If an player token reach the finish tile in the game board, then the <see cref="OnPlayerWins"/> event is raised.</remarks>
        PlayerMovementResult PlayTurn();

        /// <summary>
        /// Resets the current game session.
        /// </summary>
        /// <remarks>Starts a new game with the current players.</remarks>
        void Reset();
        #endregion
    }
}

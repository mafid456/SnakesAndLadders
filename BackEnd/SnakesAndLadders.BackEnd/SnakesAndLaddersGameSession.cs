using SnakesAndLadders.BackEnd.Contracts.Models;
using SnakesAndLadders.BackEnd.Contracts.Services;
using System;

namespace SnakesAndLadders.BackEnd
{
    public class SnakesAndLaddersGameSession : ISnakesAndLaddersGameSession
    {
        #region Internal vars
        private readonly IDice _dice;
        private readonly IBoard _board;
        private IPlayerToken[] _players;
        private int _currentPlayerArrayIndex;
        #endregion

        #region Properties
        public BoardTile[] GameBoardData => _board.Tiles;
        public bool IsInitialized => this._players is null;
        #endregion

        #region Events
        public event Action<int> OnPlayerWins;
        #endregion

        #region Constructors
        public SnakesAndLaddersGameSession()
        {
            this._dice = InternalServiceManager.GetService<IDice>();
            this._board = InternalServiceManager.GetService<IBoard>();
        }
        #endregion

        #region Methods & Functions
        public void Initialize(int players)
        {
            if (players < 1)
                throw SnakesAndLaddersGameSession.FormatNegativePlayersNumberException(callerMethod: nameof(this.Initialize), argumentName: nameof(players), argumentValue: players);

            this.CreatePlayers(players);
            this.Reset();
        }

        public IPlayerToken[] GetPlayers()
        {
            return !this.IsInitialized
                ? this._players.Clone() as IPlayerToken[]
                : throw SnakesAndLaddersGameSession.FormatGameSessionNotInitializedException(callerMethod: nameof(this.GetCurrentPlayerIndex));
        }

        public int GetCurrentPlayerIndex(out int position)
        {
            if (this.IsInitialized)
                throw SnakesAndLaddersGameSession.FormatGameSessionNotInitializedException(callerMethod: nameof(this.GetCurrentPlayerIndex));

            IPlayerToken playerData = this.GetCurrentPlayerData(out int playerIndex);
            position = playerData.Position;

            return playerIndex;
        }

        public int GetNextPlayerIndex(out int position)
        {
            if (this.IsInitialized)
                throw SnakesAndLaddersGameSession.FormatGameSessionNotInitializedException(callerMethod: nameof(this.GetNextPlayerIndex));

            IPlayerToken playerData = this.GetPlayerData(this._currentPlayerArrayIndex, out int playerIndex);
            position = playerData.Position;

            return playerIndex;
        }

        public PlayerMovementResult PlayTurn()
        {
            if (this.IsInitialized)
                throw SnakesAndLaddersGameSession.FormatGameSessionNotInitializedException(callerMethod: nameof(this.PlayTurn));

            PlayerMovementResult result = this.PlayCurrentPlayer();
            this._currentPlayerArrayIndex = this.UpdateCurrentPlayerArrayIndex(this._currentPlayerArrayIndex);

            return result;
        }

        public void Reset()
        {
            foreach (IPlayerToken playerToken in this._players)
                playerToken.Reset();

            this._currentPlayerArrayIndex = 0;
        }

        private void CreatePlayer(int index)
        {
            this._players[index] = InternalServiceManager.GetService<IPlayerToken>();
            this._players[index].OnPlayerWins += this.OnPlayerTokenWins;
        }

        private void CreatePlayers(int players)
        {
            this._players = new IPlayerToken[players];

            for (int i = 0; i < this._players.Length; i++)
                this.CreatePlayer(i);
        }

        private IPlayerToken GetPlayerData(int playerArrayIndex, out int playerIndex)
        {
            playerIndex = playerArrayIndex + 1;

            return this._players[playerArrayIndex];
        }

        private IPlayerToken GetCurrentPlayerData(out int playerIndex) => this.GetPlayerData(this._currentPlayerArrayIndex, out playerIndex);

        private PlayerMovementResult PlayCurrentPlayer()
        {
            int tiles = this._dice.Roll();
            IPlayerToken playerData = this.GetCurrentPlayerData(out _);

            return playerData.Move(tiles);
        }

        private int UpdateCurrentPlayerArrayIndex(int currentArrayIndex)
        {
            if (++currentArrayIndex == this._players.Length)
                currentArrayIndex = 0;

            return currentArrayIndex;
        }

        private static InvalidOperationException FormatGameSessionNotInitializedException(string callerMethod)
        {
            return new($"{FormatExceptionMessageHeader(callerMethod)}: The game session is not initialized yet. You must call {nameof(ISnakesAndLaddersGameSession.Initialize)}() method before start to play.");
        }

        private static ArgumentException FormatNegativePlayersNumberException(string callerMethod, string argumentName, int argumentValue)
        {
            return new($"{FormatExceptionMessageHeader(callerMethod)}: The \"{argumentName}\" argument must be value over zero ({argumentName}: {argumentValue}).");
        }

        private static string FormatExceptionMessageHeader(string callerMethodName) => $"{nameof(ISnakesAndLaddersGameSession)}::{callerMethodName}";
        #endregion

        #region Event listeners
        private void OnPlayerTokenWins() => this.OnPlayerWins?.Invoke(this.GetCurrentPlayerIndex(out _));
        #endregion
    }
}

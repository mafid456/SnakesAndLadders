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
            this.CreatePlayers(players);
            this.Reset();
        }

        public IPlayerToken[] GetPlayers() => this._players.Clone() as IPlayerToken[];

        public int GetCurrentPlayerIndex(out int position)
        {
            if (this._players is null)
                throw SnakesAndLaddersGameSession.FormatGameSessionNotInitializedException(nameof(this.GetCurrentPlayerIndex));

            IPlayerToken playerData = this.GetCurrentPlayerData(out int playerIndex);
            position = playerData.Position;

            return playerIndex;
        }

        public int GetNextPlayerIndex(out int position)
        {
            if (this._players is null)
                throw SnakesAndLaddersGameSession.FormatGameSessionNotInitializedException(nameof(this.GetNextPlayerIndex));

            IPlayerToken playerData = this.GetPlayerData(this._currentPlayerArrayIndex, out int playerIndex);
            position = playerData.Position;

            return playerIndex;
        }

        public PlayerMovementResult PlayTurn()
        {
            if (this._players is null)
                throw SnakesAndLaddersGameSession.FormatGameSessionNotInitializedException(nameof(this.PlayTurn));

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
            return new($"{nameof(ISnakesAndLaddersGameSession)}::{callerMethod}: The game session is not initialized yet. You must call {nameof(ISnakesAndLaddersGameSession.Initialize)}() method before start to play.");
        }
        #endregion

        #region Event listeners
        private void OnPlayerTokenWins() => this.OnPlayerWins?.Invoke(this.GetCurrentPlayerIndex(out _));
        #endregion
    }
}

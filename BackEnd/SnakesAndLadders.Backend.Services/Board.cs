using SnakesAndLadders.BackEnd.Contracts.Constants;
using SnakesAndLadders.BackEnd.Contracts.Enums;
using SnakesAndLadders.BackEnd.Contracts.Models;
using SnakesAndLadders.BackEnd.Contracts.Services;
using System;

namespace SnakesAndLadders.BackEnd.Services
{
    public class Board : IBoard
    {
        #region Properties
        public BoardTile[] Tiles { get; private set; }
        #endregion

        #region Constructors
        public Board() => this.CreateBoard();
        #endregion

        #region Methods & Functions
        public BoardTile GetTileByNumberPosition(int tileNumber)
        {
            return Board.IsTileNumberValid(tileNumber)
                    ? this.Tiles[--tileNumber]
                    : throw Board.FormatTileNumberOutOfRangeException(
                        callerMethodName: nameof(this.GetTileByNumberPosition),
                        argumentName: nameof(tileNumber),
                        argumentValue: tileNumber);
        }

        private void CreateBoard()
        {
            this.InitializeTileArray();

            // TODO: Implements a service to load a board definition from CSV or JSON file:
            this.SetBottomLadder(tileNumber: 2, targetNumber: 38);
            this.SetBottomLadder(tileNumber: 7, targetNumber: 14);
            this.SetBottomLadder(tileNumber: 8, targetNumber: 31);
            this.SetBottomLadder(tileNumber: 15, targetNumber: 26);
            this.SetSnakeHead(tileNumber: 16, targetNumber: 6);
            this.SetBottomLadder(tileNumber: 21, targetNumber: 42);
            this.SetBottomLadder(tileNumber: 28, targetNumber: 84);
            this.SetBottomLadder(tileNumber: 36, targetNumber: 44);
            this.SetSnakeHead(tileNumber: 46, targetNumber: 25);
            this.SetSnakeHead(tileNumber: 49, targetNumber: 11);
            this.SetBottomLadder(tileNumber: 51, targetNumber: 67);
            this.SetSnakeHead(tileNumber: 62, targetNumber: 19);
            this.SetSnakeHead(tileNumber: 64, targetNumber: 60);
            this.SetBottomLadder(tileNumber: 71, targetNumber: 91);
            this.SetBottomLadder(tileNumber: 78, targetNumber: 98);
            this.SetBottomLadder(tileNumber: 87, targetNumber: 94);
            this.SetSnakeHead(tileNumber: 89, targetNumber: 68);
            this.SetSnakeHead(tileNumber: 92, targetNumber: 88);
            this.SetSnakeHead(tileNumber: 95, targetNumber: 75);
            this.SetSnakeHead(tileNumber: 99, targetNumber: 80);
        }

        private void InitializeTileArray()
        {
            this.Tiles = new BoardTile[BoardConstants.MAX_BOARD_TILES];

            for (int i = 0; i < this.Tiles.Length; i++)
                this.Tiles[i] = new BoardTile();
        }

        private void SetBottomLadder(int tileNumber, int targetNumber) => this.SetTileData(callerMethodName: nameof(this.SetBottomLadder), tileNumber, BoardTileTypes.Ladder, targetNumber);

        private void SetSnakeHead(int tileNumber, int targetNumber) => this.SetTileData(callerMethodName: nameof(this.SetSnakeHead), tileNumber, BoardTileTypes.Snake, targetNumber);

        private void SetTileData(string callerMethodName, int tileNumber, BoardTileTypes type, int targetNumber)
        {
            if (!Board.IsTileNumberValidForSetTile(tileNumber))
                throw Board.FormatTileNumberOutOfRangeException(
                    callerMethodName,
                    argumentName: nameof(tileNumber),
                    argumentValue: tileNumber);

            if (!Board.IsTileNumberValidForSetTile(targetNumber))
                throw Board.FormatTileNumberOutOfRangeException(
                    callerMethodName,
                    argumentName: nameof(targetNumber),
                    argumentValue: targetNumber);

            if (type == BoardTileTypes.Ladder && tileNumber >= targetNumber)
            {
                throw Board.FormatBadTileParametersException(
                    callerMethodName,
                    tileNumberArgumentName: nameof(tileNumber),
                    targetNumberArgumentName: nameof(targetNumber),
                    BoardTileTypes.Ladder);
            }
            else if (type == BoardTileTypes.Snake && tileNumber <= targetNumber)
            {
                throw Board.FormatBadTileParametersException(
                    callerMethodName,
                    tileNumberArgumentName: nameof(tileNumber),
                    targetNumberArgumentName: nameof(targetNumber),
                    BoardTileTypes.Snake);
            }

            if (!this.IsTileFree(tileNumber))
                throw Board.FormatTileIsNotFreeException(
                    callerMethodName,
                    argumentName: nameof(tileNumber),
                    argumentValue: tileNumber);

            if (!this.IsTileFree(targetNumber))
                throw Board.FormatTileIsNotFreeException(
                    callerMethodName,
                    argumentName: nameof(targetNumber),
                    argumentValue: targetNumber);

            this.Tiles[--tileNumber] = new BoardTile(type, targetNumber);
        }

        public static bool IsTileNumberValid(int tileNumber) => (tileNumber > 0) && (tileNumber <= BoardConstants.MAX_BOARD_TILES);

        public static bool IsTileNumberValidForSetTile(int tileNumber) => (tileNumber > 1) && (tileNumber < BoardConstants.MAX_BOARD_TILES);

        private bool IsTileFree(int tileNumber) => this.GetTileByNumberPosition(tileNumber).type == BoardTileTypes.Normal;

        public static ArgumentOutOfRangeException FormatTileNumberOutOfRangeException(string callerMethodName, string argumentName, int argumentValue)
        {
            const string MESSAGE_TEMPLATE = "{0}: The \"{1}\" argument must be a value between 1 and {3} ({1}: {2}).";
            string message = string.Format(MESSAGE_TEMPLATE, Board.FormatExceptionMessageHeader(callerMethodName), argumentName, argumentValue, BoardConstants.MAX_BOARD_TILES - 1);

            return new(message);
        }

        private static ArgumentException FormatBadTileParametersException(string callerMethodName, string tileNumberArgumentName, string targetNumberArgumentName, BoardTileTypes tileType)
        {
            string condition = tileType == BoardTileTypes.Ladder ? "major" : "lower";

            return new($"{Board.FormatExceptionMessageHeader(callerMethodName)}: The \"{targetNumberArgumentName}\" must be {condition} than \"{tileNumberArgumentName}\".");
        }

        private static ArgumentException FormatTileIsNotFreeException(string callerMethodName, string argumentName, int argumentValue)
        {
            return new($"{Board.FormatExceptionMessageHeader(callerMethodName)}: The \"{argumentName}\" argument value must be a free tile ({argumentName}: {argumentValue}).");
        }

        private static string FormatExceptionMessageHeader(string callerMethodName) => $"{nameof(IBoard)}::{callerMethodName}";
        #endregion
    }
}

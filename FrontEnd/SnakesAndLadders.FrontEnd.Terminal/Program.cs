using SnakesAndLadders.BackEnd;
using SnakesAndLadders.BackEnd.Contracts.Models;
using System;

namespace SnakesAndLadders.FrontEnd.Terminal
{
    /// <summary>
    /// A simple terminal frontend implementation for the Snakes and Ladders game.
    /// </summary>
    class Program
    {
        #region Constants
        private const int MAX_PLAYERS = 4;
        private static readonly ConsoleColor[] PLAYER_COLORS = { ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Red };
        private const ConsoleKey PLAY_KEY = ConsoleKey.Spacebar;
        private const ConsoleKey EXIT_KEY = ConsoleKey.Escape;
        #endregion

        #region Internal vars
        private static ISnakesAndLaddersGameSession _gameSession;
        private static bool _gameSessionActive;
        private static int _currentPlayerIndex;
        private static int _currentPlayerPosition;
        #endregion

        #region Methods & Functions
        private static void Main(string[] args)
        {
            Program.InitializeGame();
            Program.GameLoop();
            Program.ShowGameOverMessage();
        }

        private static void InitializeGame()
        {
            int players = Program.RequestNumberOfPlayers();

            Program._gameSession = new SnakesAndLaddersGameSession();
            Program._gameSession.Initialize(players);
            Program._gameSession.OnPlayerWins += Program.OnPlayerWins;

            Program._gameSessionActive = true;
        }

        private static int RequestNumberOfPlayers()
        {
            int players;

            do
            {
                Program.Print($"How many players want to play? (min 1, max {Program.MAX_PLAYERS}): ", skipBreakLine: true);

                if (!Program.RequestInputNumberOfPlayers(out players))
                {
                    Program.Print($"Enter a valid number between 1 and {Program.MAX_PLAYERS}.\n");
                    players = 0;
                }

            } while (players == 0);

            Console.WriteLine();

            return players;
        }

        private static bool RequestInputNumberOfPlayers(out int players) => int.TryParse(Console.ReadLine(), out players) && Program.IsPlayerNumberValid(players);

        private static bool IsPlayerNumberValid(int players) => players > 0 && players <= Program.MAX_PLAYERS;

        private static void GameLoop()
        {
            while (Program._gameSessionActive)
            {
                Program._currentPlayerIndex = Program._gameSession.GetCurrentPlayerIndex(out int position);
                Program._currentPlayerPosition = position;
                Program.ProcessKeyInput();
            }
        }

        private static void ProcessKeyInput()
        {
            Program.PrintPlayerMessage($"turn. Current position is {Program._currentPlayerPosition}.\nPress {Program.PLAY_KEY} to roll the dice or {Program.EXIT_KEY} to finish the game.");

            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case Program.PLAY_KEY:
                        Program.PlayTurn();
                        return;

                    case Program.EXIT_KEY:
                        Program.FinishGame();
                        return;
                }
            }
        }

        private static PlayerMovementResult RollDice()
        {
            PlayerMovementResult result = Program._gameSession.PlayTurn();
            Program.PrintPlayerMessage($"dice result is {result.diceResult}.");

            return result;
        }

        private static void ShowPlayerTurnResult(PlayerMovementResult result)
        {
            if (result.IsPlayerArrivedOnSnakeOrLadderTile)
                Program.PrintPlayerMessage($"has arrived to a {result.arrivedTileType} in position {result.arrivedTileNumber}. Jump to the position {result.currentTileNumber}.\n");
            else
                Program.PrintPlayerMessage($"position is {result.currentTileNumber}.\n");
        }

        private static void ShowGameOverMessage()
        {
            Program.Print("\n\nGame over.\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void PlayTurn()
        {
            PlayerMovementResult turnResult = Program.RollDice();
            Program.ShowPlayerTurnResult(turnResult);
        }

        private static void FinishGame() => Program._gameSessionActive = false;

        private static void Print(string message, bool skipBreakLine = false)
        {
            Console.ResetColor();

            if (skipBreakLine)
                Console.Write(message);
            else
                Console.WriteLine(message);
        }

        private static void PrintPlayerMessage(string message)
        {
            Console.ForegroundColor = Program.PLAYER_COLORS[_currentPlayerIndex - 1];
            Console.CursorLeft = 0;
            Console.WriteLine($"Player #{Program._currentPlayerIndex} {message}");
        }
        #endregion

        #region Event listeners
        private static void OnPlayerWins(int playerIndex)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Player #{playerIndex} wins the game!\n");
            Program.FinishGame();
        }
        #endregion
    }
}

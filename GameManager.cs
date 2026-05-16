using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_BoardGames_Framework
{
    public enum GameMode
    {
        HumanVsHuman,
        HumanVsAI,
    }

    public enum GameType
    {
        ConnectFour,
        Gomoku,
        TicTacToe,
        NumericalTicTacToe,
        Notakto,
        // Add more game types as needed
    }

    internal class GameManager
    {
        private int turnCounter;
        private List<Player> players;
        public GameMode Mode { get; }
        public GameType Type { get; }

        public GameManager(GameMode mode, GameType type, Player player1, Player player2)
        {
            Mode = mode;
            Type = type;
            players = new List<Player> { player1, player2 };
            turnCounter = 0;
        }

        public void Run()
        {
            bool gameEnded = false;
            // Mock board and rules for demonstration
            Board board = new Board();
            GameRules rules = new ConnectFourRules(); // Replace with actual rules createst woth the factorey pattern
            while (!gameEnded)
            {
                int currentPlayerIndex = turnCounter % 2;
                Player currentPlayer = players[currentPlayerIndex];
                Console.WriteLine($"Turn {turnCounter + 1}: Player {currentPlayer.PlayerId}'s move.");
                // Execute the player's action

                string action = currentPlayer.RequestAction(board, rules, turnCounter + 1);
                Console.WriteLine($"Player {currentPlayer.PlayerId} action: {action}");


                // For now, simulate a move and end after 10 turns
                turnCounter++;

                bool checkForWin = turnCounter >= 10; // Replace with actual win condition check
                if (checkForWin)
                {
                    Console.WriteLine("Game ended (demo mode, 10 turns max).");
                    gameEnded = true;
                }
            }
        }
    }
}

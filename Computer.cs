using System;
using System.Collections.Generic;

namespace OOP_BoardGames_Framework
{
    public class Computer : Player
    {
        public Computer(int playerNumber, string gamePiece)
            : base(playerNumber, gamePiece)
        {
        }

        public override string RequestAction(Board board, GameRules rules, int turn)
        {
            PlayerMove move = CheckForWinningMove(board, rules); // try to win first

            if (move == null)
            {
                move = ChooseRandomValidMove(board, rules); // if no winning move, pick random valid move
            }

            return move?.Move ?? "";
        }

        private PlayerMove CheckForWinningMove(Board board, GameRules rules)
        {
            List<PlayerMove> moves = rules.GetValidMoves(board, this); // get all valid moves for AI testing

            foreach (PlayerMove move in moves)
            {
                if (rules.ApplyMove(board, move))
                {
                    bool gameEnd = rules.CheckForWinning(board);

                    rules.UndoMove(board, move);

                    if (rules is NotaktoRules) // Notakto uses opposite win logic
                    {
                        if (!gameEnd)
                        {
                            return move; // choose a move that avoids ending the game
                        }
                    }
                    else if (gameEnd)
                    {
                        return move;
                    }
                }
            }

            return null;
        }

        private PlayerMove ChooseRandomValidMove(Board board, GameRules rules)
        {
            List<PlayerMove> moves = rules.GetValidMoves(board, this);

            if (moves.Count == 0)
            {
                return null;
            }

            Random random = new Random();

            return moves[random.Next(moves.Count)];
        }
    }
}
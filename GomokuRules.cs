using System.Collections.Generic;

namespace OOP_BoardGames_Framework
{
    public class GomokuRules : LineBasedGameRules
    {
        public override int Columns { get; } = 15;
        public override int Rows { get; } = 15;
        public override int WinLength { get; } = 5;
        public override string Player1Symbol { get; } = "X";
        public override string Player2Symbol { get; } = "O";
        public override bool ValidatePlayerMove(Board board, PlayerMove move)
        {
            if (!TryParseMove(move, out int column, out int row))
            {
                return false;
            }
            if (!IsInsideBoard(column, row))
            {
                return false;
            }
            //return board.IsCellEmpty(column, row);
            return true;
        }
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            if (!TryParseMove(move, out int column, out int row))
            {
                throw new InvalidOperationException("Invalid Gomoku move.");
            }
            board.AddElement(column, row, playerSymbol);
        }
        public override List<PlayerMove> GetValidMoves(Board board, Player player) // find all moves the player can play for AI testing
        {
            List<PlayerMove> moves = new List<PlayerMove>();

            for (int row = 1; row <= Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    char columnLetter = (char)('A' + column);
                    string moveText = columnLetter + row.ToString();

                    PlayerMove move = new PlayerMove(moveText, player, 0);

                    if (ValidatePlayerMove(board, move))
                    {
                        moves.Add(move);
                    }
                }
            }

            return moves;
        }
        public override void UndoMove(Board board, PlayerMove move) // undo the AI test move
        {
            if (TryParseMove(move, out int column, out int row))
            {
                List<int[]> spots = new List<int[]>();
                spots.Add(new int[] { column, row });

                board.RemoveElements(spots, move.Player.GamePiece);
            }
        }
        private bool TryParseMove(PlayerMove move, out int column, out int row)
        {
            column = 0;
            row = 0;
            if (move == null || string.IsNullOrWhiteSpace(move.Move))
            {
                return false;
            }
            string input = move.Move.Trim().ToUpper();
            if (input.Length < 2 || input.Length > 3)
            {
                return false;
            }
            char columnLetter = input[0];
            if (columnLetter < 'A' || columnLetter > 'O')
            {
                return false;
            }
            column = columnLetter - 'A'; //convert to num
            string rowText = input.Substring(1);
            if (!int.TryParse(rowText, out row))
            {
                return false;
            }
            row--;
            return true;
        }
    }
}
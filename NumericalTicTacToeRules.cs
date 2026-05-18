using System.Collections.Generic;

namespace OOP_BoardGames_Framework
{
    public class NumericalTicTacToeRules : GameRules
    {
        public override int Columns { get; }
        public override int Rows { get; }
        public override int WinLength { get; }
        public NumericalTicTacToeRules(int gridSize = 3) //Set grid size to 3 by defualt if not custom
        {
            if (gridSize < 3)
            {
                throw new ArgumentException("Numerical Tic-Tac-Toe must be at least 3x3.");
            }
            Columns = gridSize;
            Rows = gridSize;
            WinLength = gridSize;
        }
        public override bool ValidatePlayerMove(Board board, PlayerMove move) //Validate player move
        {
            if (!TryParseMove(move, out int column, out int row, out int number)) //Asks for column, row, number
            {
                return false;
            }
            return IsInsideBoard(column, row) && board.IsCellEmpty(column, row) && number >= 1 && number <= Columns * Rows && !NumberAlreadyUsed(board, number) && IsCorrectNumberForPlayer(move, number);
        }
        public override bool CheckForWinning(Board board) //Checks for win
        {
            for (int column = 0; column < Columns; column++)
            { //Differs from LineBased as it is checking for sum not symbol
                for (int row = 0; row < Rows; row++)
                {
                    if (CheckDirection(board, column, row, 1, 0) || CheckDirection(board, column, row, 0, 1) || CheckDirection(board, column, row, 1, 1) || CheckDirection(board, column, row, 1, -1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol) //Executes move
        {
            if (!TryParseMove(move, out int column, out int row, out int number))
            {
                throw new InvalidOperationException("Invalid Numerical Tic-Tac-Toe move.");
            }
            if (!board.AddElement(column, row, number.ToString()))
            {
                throw new InvalidOperationException("That space is already taken.");
            }
        }
        public override List<PlayerMove> GetValidMoves(Board board, Player player) // find all moves the player can play for AI testing
        {
            List<PlayerMove> moves = new List<PlayerMove>();

            int max = Rows * Columns;

            for (int position = 1; position <= max; position++)
            {
                for (int number = 1; number <= max; number++)
                {
                    PlayerMove move = new PlayerMove(position + "," + number, player, 0);

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
            if (TryParseMove(move, out int column, out int row, out int number))
            {
                List<int[]> spots = new List<int[]>();
                spots.Add(new int[] { column, row });

                board.RemoveElements(spots, number.ToString());
            }
        }
        private bool TryParseMove(PlayerMove move, out int column, out int row, out int number) //Convert mentioned above into useable numbers
        {
            column = row = number = 0; //Prevent a crash if return false
            if (move == null || string.IsNullOrWhiteSpace(move.Move))
            {
                return false;
            }
            string[] parts = move.Move.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }
            if (!int.TryParse(parts[0].Trim(), out int position))
            {
                return false;
            }
            if (!int.TryParse(parts[1].Trim(), out number))
            {
                return false;
            }
            if (position < 1 || position > Columns * Rows)
            {
                return false;
            }
            position--; // converts screen input into zero-based board index
            row = position / Columns;
            column = position % Columns;
            return true;
        }//Validates the 3 parts of the move
        private bool CheckDirection(Board board, int initialColumn, int initialRow, int columnDirectionStep, int rowDirectionStep)
        { //Adjusted version of my code from LineBasedGame
            int total = 0;
            for (int count = 0; count < WinLength; count++)
            {
                int column = initialColumn + (count * columnDirectionStep);
                int row = initialRow + (count * rowDirectionStep);
                if (!IsInsideBoard(column, row) ||
                !int.TryParse(board.GetCell(column, row), out int number))
                {
                    return false;
                }
                total += number;
            }
            return total == WinLength * ((WinLength * WinLength) + 1) / 2;
        }
        private bool NumberAlreadyUsed(Board board, int number) //Checks the board for an existing number as each can only be used once
        {
            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    if (int.TryParse(board.GetCell(column, row), out int existingNumber) && existingNumber == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsCorrectNumberForPlayer(PlayerMove move, int number) //Makes sure that p1 is odd p2 is even
        {
            if (move.Player == null)
            {
                return true;
            }
            return move.Player.PlayerId == 1 ? number % 2 == 1 : number % 2 == 0; //Does this by checking the remained after dividing 2
        }
    }
}
namespace OOP_BoardGames_Framework
{
    // Inhertis from Line based game rules
    public class ConnectFourRules : LineBasedGameRules
    {
        //Polymorphic 
        public override int Columns { get; } = 7;
        public override int Rows { get; } = 6;
        public override int WinLength { get; } = 4;
        public override string Player1Symbol { get; } = "X";
        public ConsoleColor Player1Colour { get; } = ConsoleColor.Red;
        public override string Player2Symbol { get; } = "O";
        public ConsoleColor Player2Colour { get; } = ConsoleColor.Yellow;
        // Checks for a valid move by checking if user input is:
        // A number
        // In the range
        // If not full (this checks the top row for the column in this case)
        public override bool ValidatePlayerMove(Board board, PlayerMove move)
        {
            if (!TryParseMove(move, out int column))
            {
                return false;
            }
            if (column < 0 || column >= Columns)
            {
                return false;
            }
            return board.IsCellEmpty(column, 0);
        }

        public ConsoleColor GetColorForSymbol(string symbol)
        {
            if (symbol == Player1Symbol) return Player1Colour;
            if (symbol == Player2Symbol) return Player2Colour;
            return Console.ForegroundColor;
        }
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            if (!TryParseMove(move, out int column))
            {
                throw new InvalidOperationException("Invalid Connect Four move.");
            }
            for (int row = Rows - 1; row >= 0; row--) // Apply gravity.
            {
                if (board.IsCellEmpty(column, row))
                {
                    board.AddElement(column, row, playerSymbol);
                    return;
                }
            }
            throw new InvalidOperationException("Column is full.");
        }
        // find all moves the player can play for AI testing
        public override List<PlayerMove> GetValidMoves(Board board, Player player)
        {
            List<PlayerMove> moves = new List<PlayerMove>();
            for (int column = 1; column <= Columns; column++)
            {
                PlayerMove move = new PlayerMove(column.ToString(), player, 0);
                if (ValidatePlayerMove(board, move))
                {
                    moves.Add(move);
                }
            }
            return moves;
        }
        // undo the AI test move
        public override void UndoMove(Board board, PlayerMove move)
        {
            if (!TryParseMove(move, out int column))
            {
                return;
            }
            for (int row = 0; row < Rows; row++)
            {
                if (!board.IsCellEmpty(column, row))
                {
                    RemovePiece(board, column, row, move.Player.GamePiece);
                    return;
                }
            }
        }
        // Encapsulation
        private bool TryParseMove(PlayerMove move, out int column)
        {
            column = 0;
            if (move == null || string.IsNullOrWhiteSpace(move.Move))
            {
                return false;
            }
            if (!int.TryParse(move.Move.Trim(), out column))
            {
                return false;
            }
            column--;
            return true;
        }
    }
}
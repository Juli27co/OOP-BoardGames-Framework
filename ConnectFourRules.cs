namespace OOP_BoardGames_Framework
{
    public class ConnectFourRules : LineBasedGameRules
    {
        public override int Columns { get; } = 7;
        public override int Rows { get; } = 6;
        public override int WinLength { get; } = 4;
        public override string Player1Symbol { get; } = "X";
        public ConsoleColor Player1Colour { get; } = ConsoleColor.Red;
        public override string Player2Symbol { get; } = "O";
        public ConsoleColor Player2Colour { get; } = ConsoleColor.Yellow;
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
        public override List<PlayerMove> GetValidMoves(Board board, Player player) // find all moves the player can play for AI testing
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
        public override void UndoMove(Board board, PlayerMove move) // undo the AI test move
        {
            if (!TryParseMove(move, out int column))
            {
                return;
            }
            for (int row = 0; row < Rows; row++)
            {
                if (!board.IsCellEmpty(column, row))
                {
                    List<int[]> spots = new List<int[]>();
                    spots.Add(new int[] { column, row });
                    board.RemoveElements(spots, move.Player.GamePiece);
                    return;
                }
            }
        }
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
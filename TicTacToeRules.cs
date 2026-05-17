namespace OOP_BoardGames_Framework
{
    public class TicTacToeRules : LineBasedGameRules
    {
        public override int Columns { get; } = 3;
        public override int Rows { get; } = 3;
        public override int WinLength { get; } = 3;
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
            return board.IsCellEmpty(column, row);
        }
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            if (!TryParseMove(move, out int column, out int row))
            {
                throw new InvalidOperationException("Invalid Tic-Tac-Toe move.");
            }

            board.AddElement(column, row, playerSymbol);
        }
        private bool TryParseMove(PlayerMove move, out int column, out int row)
        {
            column = 0;
            row = 0;
            if (move == null || string.IsNullOrWhiteSpace(move.Move))
            {
                return false;
            }
            if (!int.TryParse(move.Move.Trim(), out int position))
            {
                return false;
            }
            if (position < 1 || position > 9)
            {
                return false;
            }
            position--; // converts screen input 1-9 into board index 0-8
            row = position / Columns;
            column = position % Columns;
            return true;
        }
    }
}
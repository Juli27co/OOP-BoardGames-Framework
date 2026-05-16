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
            //return board.IsCellEmpty(column, row);
            return false;
        }
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            if (!TryParseMove(move, out int column, out int row))
            {
                throw new InvalidOperationException("Invalid Tic-Tac-Toe move.");
            }

            //board.PlaceSymbol(column, row, playerSymbol);
        }
        private bool TryParseMove(PlayerMove move, out int column, out int row)
        {
            column = 0;
            row = 0;
            if (move == null || string.IsNullOrWhiteSpace(move.Move))
            {
                return false;
            }
            string[] parts = move.Move.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }
            return int.TryParse(parts[0].Trim(), out column) && int.TryParse(parts[1].Trim(), out row);
        }
    }
}
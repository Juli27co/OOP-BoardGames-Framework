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
            //if (!TryParseMove(move, out int column))
            //{
            //    return false;
            //}
            //if (column < 0 || column >= Columns)
            //{
            //    return false;
            //}
            //return board.IsCellEmpty(column, 0);
            return true;
        }
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            if (!TryParseMove(move, out int column))
            {
                throw new InvalidOperationException("Invalid Connect Four move.");
            }
            //for (int row = Rows - 1; row >= 0; row--) // Apply gravity.
            //{
            //    if (board.IsCellEmpty(column, row))
            //    {
            //        board.PlaceSymbol(column, row, playerSymbol);
            //        return;
            //    }
            //}
            throw new InvalidOperationException("Column is full.");
        }
        private bool TryParseMove(PlayerMove move, out int column)
        {
            column = 0;
            if (move == null || string.IsNullOrWhiteSpace(move.Move))
            {
                return false;
            }
            return int.TryParse(move.Move.Trim(), out column);
        }
    }
}
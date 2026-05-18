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
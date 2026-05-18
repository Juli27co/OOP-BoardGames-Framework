namespace OOP_BoardGames_Framework
{
    public abstract class GameRules
    {
        public abstract int Columns { get; }
        public abstract int Rows { get; }
        public abstract int WinLength { get; }
        public abstract bool ValidatePlayerMove(Board board, PlayerMove move);
        public abstract bool CheckForWinning(Board board);
        public abstract void ExecuteMove(Board board, PlayerMove move, string playerSymbol);

        public virtual Board CreateGrid()
        {
            return new Board(Rows, Columns);
        }
        protected bool IsInsideBoard(int column, int row)
        {
            return column >= 0 && column < Columns && row >= 0 & row < Rows;
        }
        public virtual bool CheckForDraw(Board board)
        {
            return board.IsBoardFull();
        }
    }
}
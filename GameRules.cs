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
        public virtual bool ApplyMove(Board board, PlayerMove move) // let AI test a move on the board
        {
            if (!ValidatePlayerMove(board, move))
            {
                return false;
            }
            ExecuteMove(board, move, move.Player.GamePiece);
            return true;
        }
        public virtual void UndoMove(Board board, PlayerMove move) // remove the test move and go back
        {
        }
        public virtual List<PlayerMove> GetValidMoves(Board board, Player player) // find all moves the player can play for AI testing
        {
            return new List<PlayerMove>();
        }
        protected bool IsInsideBoard(int column, int row)
        {
            return column >= 0 && column < Columns && row >= 0 && row < Rows;
        }
        public virtual bool CheckForDraw(Board board)
        {
            return board.IsBoardFull();
        }

    }
} 
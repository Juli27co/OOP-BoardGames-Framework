namespace OOP_BoardGames_Framework
{
    // No need to create game rules directly 
    public abstract class GameRules
    {
        // Abstract to force every game to provide this data
        public abstract int Columns { get; }
        public abstract int Rows { get; }
        public abstract int WinLength { get; }
        // Abstract as individual games MUST override these methods
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
        // Can override but don't need to
        public virtual void UndoMove(Board board, PlayerMove move) // ai remove the test move and go back
        {
        }
        public virtual List<PlayerMove> GetValidMoves(Board board, Player player) // find all moves the player can play for AI testing
        {
            return new List<PlayerMove>();
        }
        // Only useable by gamerules and the child classes
        protected void RemovePiece(Board board, int column, int row, string piece) // helper method for removing one move from the board
        {
            List<int[]> spots = new List<int[]>();
            spots.Add(new int[] { column, row });
            board.RemoveElements(spots, piece);
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
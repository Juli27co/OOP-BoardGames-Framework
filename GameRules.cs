namespace OOP_BoardGames_Framework
{
    public interface GameRules
    {
        int Columns { get; }
        int Rows { get; }
        int WinLength { get; }
        string Player1Symbol { get; }
        string Player2Symbol { get; }
        public bool ValidatePlayerMove(Board board, PlayerMove move);
        public bool CheckForWinning(Board board);
        public bool CheckForDraw(Board board);
        void ExecuteMove(Board board, PlayerMove move, string playerSymbol);
    }
}
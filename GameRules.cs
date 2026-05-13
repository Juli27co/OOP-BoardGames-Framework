public interface GameRules
{
    int Columns { get; }
    int Rows { get; }
    int WinLength { get; }
    string Player1Symbol { get; }
    string Player2Symbol { get; }
    public bool ValidatePlayerMove(Board board, Move move);
    public bool CheckForWinning(Board board);
    public bool CheckForDraw(Board board);
    void ExecuteMove(Board board, Move move, string playerSymbol);
}
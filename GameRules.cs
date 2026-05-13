public interface GameRules
{
    public abstract int Columns { get; }
    public abstract int Rows { get; }
    public abstract int WinLength { get; }
    public abstract string Player1Symbol { get; }
    public abstract string Player2Symbol { get; }
    public bool ValidatePlayerMove(Board board, Move move)
    {
        return true; //Placeholder Test
    }
    public bool CheckForWinnning(Board board)
    {
        return true; //Placeholder Test
    }
    public bool CheckForDraw(Board board)
    {
        return true; //Placeholder Test
    }
    public abstract void ExecuteMove(Board board, Move move, string playerSymbol)
    {
    }
    protected void CreateGrid()
    {
    }
}
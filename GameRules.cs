public interface GameRules
{
    public bool CheckForDraw(Board board)
    {
        return true; //Placeholder Test
    }

    public bool CheckForWinnning(Board board)
    {
        return true; //Placeholder Test
    }
    public bool ValidatePlayerMove(Board board, Move move)
    {
        {
            return true; //Placeholder Test
        }
    }
    protected void ExecuteMove()
    {

    }
    protected void CreateGrid()
    {

    }
}
namespace OOP_BoardGames_Framework
{
    public class NotaktoRules : GameRules
    {
        public override int Columns => throw new NotImplementedException();
        public override int Rows => throw new NotImplementedException();
        public override int WinLength => throw new NotImplementedException();
        public override bool ValidatePlayerMove(Board board, PlayerMove move)
        {
            {
                return true; //Placeholder Test
            }
        }
        public override bool CheckForWinning(Board board)
        {
            return true; //Placeholder Test
        }
        public override bool CheckForDraw(Board board)
        {
            return true; //Placeholder Test
        }
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            // Stub
        }
    }
}
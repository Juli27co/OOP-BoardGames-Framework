namespace OOP_BoardGames_Framework
{
    public class GomokuRules : GameRules
    {
        public int Columns { get; } = 15;
        public int Rows { get; } = 15;
        public int WinLength { get; } = 5;
        public string Player1Symbol { get; } = "X";
        public string Player2Symbol { get; } = "O";
        public bool ValidatePlayerMove(Board board, PlayerMove move)
        {
            return true; //Needs implementation
        }
        public bool CheckForWinning(Board board) //Reused from my assignment 1 (refactored though)
        {
            return false;
        }
        public bool CheckForDraw(Board board)
        {
            //return board.IsBoardFull(); //Needs implementation in board
            return false;
        }
        public void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            //board.PlaceSymbol(move.Column, move.Row, playerSymbol);
        }
    }
}
namespace OOP_BoardGames_Framework
{
    public abstract class LineBasedGameRules : BoardGameRules
    {
        public abstract string Player1Symbol { get; }
        public abstract string Player2Symbol { get; }
        public override bool CheckForWinning(Board board)
        {
            return CheckForWinningSymbol(board, Player1Symbol) || CheckForWinningSymbol(board, Player2Symbol);
        }
        protected bool CheckForWinningSymbol(Board board, string playerSymbol) //Reused from my assignment 1 (refactored though)
        {
            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    if (board.GetCell(column, row) != playerSymbol)
                    {
                        continue;
                    }
                    if (CheckDirection(board, column, row, 1, 0, playerSymbol)) // horizontal
                    {
                        return true;
                    }
                    if (CheckDirection(board, column, row, 0, 1, playerSymbol)) // vertical
                    {
                        return true;
                    }
                    if (CheckDirection(board, column, row, 1, 1, playerSymbol)) // diagonal down-right
                    {
                        return true;
                    }
                    if (CheckDirection(board, column, row, 1, -1, playerSymbol)) // diagonal up-right
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        protected bool CheckDirection(Board board, int initialColumn, int initialRow, int columnDirectionStep, int rowDirectionStep, string playerSymbol)
        {//Reused from my assignment 1 (refactored though)
            for (int count = 0; count < WinLength; count++)
            {
                int column = initialColumn + (count * columnDirectionStep);
                int row = initialRow + (count * rowDirectionStep);
                if (!IsInsideBoard(column, row))
                {
                    return false;
                }
                if (board.GetCell(column, row) != playerSymbol)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
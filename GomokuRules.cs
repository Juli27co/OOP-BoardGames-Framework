public class GomokuRules : GameRules
{
    public int Columns { get; } = 15;
    public int Rows { get; } = 15;
    public int WinLength { get; } = 5;
    public string Player1Symbol { get; } = "X";
    public string Player2Symbol { get; } = "O";
    public bool ValidatePlayerMove(Board board, Move move)
    {
        if (move.Column < 0 || move.Column >= Columns)
        {
            return false;
        }

        if (move.Row < 0 || move.Row >= Rows)
        {
            return false;
        }

        return board.IsCellEmpty(move.Column, move.Row);
    }
    public bool CheckForWinning(Board board, string playerSymbol) //Reused from my assignment 1 (refactored though)
    {
        for (int column = 0; column < Columns; column++)
        {
            for (int row = 0; row < Rows; row++)
            {
                if (board.GetCell(column, row) != playerSymbol)//Need a method from elswhere
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
    private bool CheckDirection(Board board, int initialColumn, int initialRow, int columnDirectionStep, int rowDirectionStep, string playerSymbol)
    {
        for (int count = 0; count < WinLength; count++)
        {
            int column = initialColumn + (count * columnDirectionStep);
            int row = initialRow + (count * rowDirectionStep);

            if (column < 0 || column >= Columns || row < 0 || row >= Rows)
            {
                return false;
            }

            if (board.GetCell(column, row) != playerSymbol) //Need method from elsewhere
            {
                return false;
            }
        }

        return true;
    }
    public bool CheckForDraw(Board board)
    {
        return board.IsBoardFull(); //Needs implementation in board
    }
    public void ExecuteMove(Board board, Move move, string playerSymbol)
    {
        board.PlaceSymbol(move.Column, move.Row, playerSymbol);
    }
}
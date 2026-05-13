public class ConnectFourRules : GameRules
{
    public override int Columns { get; } = 7;
    public override int Rows { get; } = 6;
    public override int WinLength { get; } = 4;
    public override string Player1Symbol { get; } = "X";
    public ConsoleColor Player1Colour { get; } = ConsoleColor.Red;
    public override string Player2Symbol { get; } = "O";
    public ConsoleColor Player2Colour { get; } = ConsoleColor.Yellow;
    public override bool ValidatePlayerMove(Board board, Move move)
    {
        if (move.Column < 0 || move.Column >= Columns)
        {
            return false;
        }
        return board.IsCellEmpty(move.Column, 0);
    }
    public override bool CheckForWinning(Board board, string playerSymbol) //Reused from my assignment 1 (refactored though)
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
    public override bool CheckForDraw(Board board)
    {
        return board.IsBoardFull(); //Needs implementation in board
    }
    public override void ExecuteMove(Board board, Move move, string playerSymbol)
    {
        for (int row = Rows - 1; row >= 0; row--) // Apply gravity. Didn't use a method as no other game uses gravity
        {
            if (board.IsCellEmpty(move.Column, row))
            {
                board.PlaceSymbol(move.Column, row, playerSymbol);
                return;
            }
        }
        throw new InvalidOperationException("Column is full.");
    }
}
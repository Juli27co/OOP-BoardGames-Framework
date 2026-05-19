namespace OOP_BoardGames_Framework
{
    public class BoardDisplay
    {
        public void ShowCommonBoard(Board board)
        {
            PrintCommonBoard(board);
        }
        public void ShowConnectFourBoard(Board board)
        {
            PrintColumnHeader(board.GetColumnCount());
            PrintConnectFourBoard(board);
        }
        public void ShowGomokuBoard(Board board)
        {
            PrintNumberBoard(board);
        }
        public void ShowNumericalTicTacToeBoard(Board board)
        {
            PrintNumericalTicTacToeBoard(board);
        }

        public void ShowNotaktoBoard(Board board)
        {
            int boardSize = 3;
            for (int b = 0; b < 3; b++)
            {
                Console.Write($"    Board {b + 1}    ");
            }
            Console.WriteLine();

            for (int row = 0; row < boardSize; row++)
            {
                for (int b = 0; b < 3; b++)
                {
                    int colOffset = b * boardSize;
                    for (int col = 0; col < boardSize; col++)
                    {
                        Console.Write(" " + board.GetCell(colOffset + col, row) + " ");
                        if (col < boardSize - 1)
                        {
                            Console.Write("|");
                        }
                    }
                    Console.Write("         ");
                }
                Console.WriteLine();

                if (row < boardSize - 1)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        Console.Write("------------");
                        Console.Write("     ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        private void PrintCommonBoard(Board board) //for games using a basic grid layout (e.g. Tic-Tac-Toe, Numerical Tic-Tac-Toe, Notakto)
        {
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    Console.Write(" " + board.GetCell(column, row) + " ");
                    if (column < board.GetColumnCount() - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                if (row < board.GetRowCount() - 1)
                {
                    PrintRowLine(board.GetColumnCount());
                }
            }
        }
        private void PrintConnectFourBoard(Board board)
        {
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    Console.Write("| " + board.GetCell(column, row) + " ");
                }
                Console.WriteLine("|");
            }
        }
        private void PrintNumberBoard(Board board) //display Gomoku board with row and column numbers.
        {
            Console.Write("   ");
            for (int column = 0; column < board.GetColumnCount(); column++)
            {
                Console.Write((column + 1).ToString().PadLeft(3));
            }
            Console.WriteLine();
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                Console.Write((row + 1).ToString().PadLeft(2) + " ");
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    Console.Write(board.GetCell(column, row).PadLeft(3));
                }
                Console.WriteLine();
            }
        }
        private void PrintNumericalTicTacToeBoard(Board board)
        {
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    string cell = board.GetCell(column, row);
                    if (string.IsNullOrWhiteSpace(cell))
                    {
                        int position = (row * board.GetColumnCount()) + column + 1;
                        cell = $"({position})";
                    }
                    else
                    {
                        cell = $" {cell} ";
                    }
                    Console.Write(cell.PadLeft(4).PadRight(5));
                    if (column < board.GetColumnCount() - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                if (row < board.GetRowCount() - 1)
                {
                    PrintWideRowLine(board.GetColumnCount());
                }
            }
        }
        private void PrintColumnHeader(int columns)
        {
            for (int column = 0; column < columns; column++)
            {
                Console.Write("  " + (column + 1) + " ");
            }
            Console.WriteLine();
        }
        private void PrintRowLine(int columns)
        {
            for (int column = 0; column < columns; column++)
            {
                Console.Write("---");
                if (column < columns - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();
        }
        private void PrintWideRowLine(int columns)
        {
            for (int column = 0; column < columns; column++)
            {
                Console.Write("-----");
                if (column < columns - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();
        }
    }
}
namespace OOP_BoardGames_Framework
{
    public class BoardDisplay
    {
        public void ShowCommonBoard(Board board)
        {
            PrintGridBoard(board, false);
        }
        public void ShowNumericalTicTacToeBoard(Board board)
        {
            PrintGridBoard(board, true);
        }
        public void ShowNotaktoBoard(Board board)
        {
            PrintNotaktoBoard(board);
        }
        public void ShowConnectFourBoard(Board board)
        {
            PrintColumnHeader(board.GetColumnCount());
            PrintConnectFourBoard(board);
        }
        public void ShowGomokuBoard(Board board)
        {
            PrintGomokuBoard(board);
        }
        private void PrintGridBoard(Board board, bool showPositionNumbers) //for games using a basic grid layout (e.g. Tic-Tac-Toe, Numerical Tic-Tac-Toe)
        {
            int cellWidth;
            if (showPositionNumbers)
            {
                cellWidth = 5;
            }
            else
            {
                cellWidth = 3;
            }
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    string cell = board.GetCell(column, row);
                    if (showPositionNumbers && string.IsNullOrWhiteSpace(cell))
                    {
                        int position = (row * board.GetColumnCount()) + column + 1;
                        cell = "(" + position + ")";
                    }
                    else
                    {
                        cell = " " + cell + " ";
                    }
                    Console.Write(cell.PadLeft(cellWidth));
                    if (column < board.GetColumnCount() - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                if (row < board.GetRowCount() - 1)
                {
                    PrintRowLine(board.GetColumnCount(), cellWidth);
                }
            }
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
                    bool isEndOfSmallBoard = (column + 1) % 3 == 0;
                    bool isLastColumn = column == board.GetColumnCount() - 1;
                    if (!isEndOfSmallBoard && !isLastColumn)
                    {
                        Console.Write("|");
                    }
                    else if (isEndOfSmallBoard && !isLastColumn)
                    {
                        Console.Write("     ");
                    }
                }
                Console.WriteLine();
                if (row < board.GetRowCount() - 1)
                {
                    PrintNotaktoRowLine(board.GetColumnCount());
                }
            }
        }
        private void PrintConnectFourBoard(Board board)
        {
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    Console.Write("| ");
                    string cell =board.GetCell(column, row);
                    if (cell == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(cell);
                        Console.ResetColor();
                    }
                    else if (cell == "O")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(cell);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(cell);
                    }
                    Console.Write(" ");
                }
                Console.WriteLine("|");
            }
        }
        private void PrintGomokuBoard(Board board)
        {
            Console.Write("   ");
            for (int column = 0; column < board.GetColumnCount(); column++)
            {
                char columnLetter = (char)('A' + column);
                Console.Write(columnLetter.ToString().PadLeft(3));
            }
            Console.WriteLine();
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                Console.Write((row + 1).ToString().PadLeft(2) + " ");
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    string cell = board.GetCell(column, row);
                    if (string.IsNullOrWhiteSpace(cell))
                    {
                        cell = ".";
                    }
                    Console.Write(cell.PadLeft(3));
                }
                Console.WriteLine();
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
        private void PrintRowLine(int columns, int cellWidth)
        {
            for (int column = 0; column < columns; column++)
            {
                Console.Write(new string('-', cellWidth));
                if (column < columns - 1)
                {
                    Console.Write("+");
                }
            }
            Console.WriteLine();
        }
        private void PrintNotaktoRowLine(int columns)
        {
            for (int column = 0; column < columns; column++)
            {
                Console.Write("---");
                bool isEndOfSmallBoard = (column + 1) % 3 == 0;
                bool isLastColumn = column == columns - 1;
                if (!isEndOfSmallBoard && !isLastColumn)
                {
                    Console.Write("+");
                }
                else if (isEndOfSmallBoard && !isLastColumn)
                {
                    Console.Write("     ");
                }
            }
            Console.WriteLine();
        }
    }
}
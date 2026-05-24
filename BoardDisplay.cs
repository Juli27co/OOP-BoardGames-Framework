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
        public void ShowConnectFourBoard(Board board, ConnectFourRules? rules = null)
        {
            PrintColumnHeader(board.GetColumnCount());
            PrintConnectFourBoard(board, rules);
        }
        public void ShowGomokuBoard(Board board)
        {
            PrintGomokuBoard(board);
        }
        private void PrintGridBoard(Board board, bool showPositionNumbers) //for games using a basic grid layout
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

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(cell.PadLeft(cellWidth));
                        Console.ResetColor();
                    }
                    else
                    {
                        cell = " " + cell + " ";
                        Console.Write(cell.PadLeft(cellWidth));
                    }

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

        public void PrintNotaktoBoard(Board board)
        {
            int boardSize = 3;
            for (int b = 0; b < 3; b++)
            {
                Console.Write($"   Board {b + 1}         ");
            }
            Console.WriteLine();

            for (int row = 0; row < boardSize; row++)
            {
                for (int b = 0; b < 3; b++)
                {
                    int colOffset = b * boardSize;
                    for (int col = 0; col < boardSize; col++)
                    {
                        string cell = board.GetCell(colOffset + col, row);

                        if (string.IsNullOrWhiteSpace(cell))
                        {
                            cell = " ";
                        }

                        Console.Write(" " + cell + " ");
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
                        Console.Write("-----------");
                        Console.Write("         ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        private void PrintConnectFourBoard(Board board, ConnectFourRules? rules)
        {
            for (int row = 0; row < board.GetRowCount(); row++)
            {
                for (int column = 0; column < board.GetColumnCount(); column++)
                {
                    string cell = board.GetCell(column, row);
                    Console.Write("| ");

                    if (rules != null && !string.IsNullOrWhiteSpace(cell))
                    {
                        ConsoleColor colour = rules.GetColorForSymbol(cell);
                        ConsoleColor original = Console.ForegroundColor;
                        Console.ForegroundColor = colour;
                        Console.Write(cell);
                        Console.ForegroundColor = original;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void PrintColumnHeader(int columns)
        {
            Console.Write(" ");

            for (int column = 0; column < columns; column++)
            {
                Console.Write(" " + (column + 1) + " ");
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
    }
}

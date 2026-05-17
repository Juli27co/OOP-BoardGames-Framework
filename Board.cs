using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_BoardGames_Framework
{
    public class Board
    {
        private List<string[]> gridMatrix;
        public List<string[]> GridMatrix
        {
            get { return gridMatrix; }
            set { gridMatrix = value; }
        }

        public Board() { gridMatrix = new List<string[]>(); }
        public Board(int rows, int columns)
        {
            gridMatrix = new List<string[]>();

            for (int row = 0; row < rows; row++)
            {
                string[] newRow = new string[columns];

                for (int column = 0; column < columns; column++)
                {
                    newRow[column] = " ";
                }

                gridMatrix.Add(newRow);
            }
        }

        public int GetRowCount() //get total rows for displaying the board.
        {
            return gridMatrix.Count;
        }

        public int GetColumnCount() //get total cols for displaying the board.
        {
            return gridMatrix[0].Length;
        }

        public string GetCell(int column, int row)
        {
            return gridMatrix[row][column];
        }

        public bool IsCellEmpty(int column, int row)
        {
            if (!IsInsideBoard(column, row))
            {
                return false;
            }

            return gridMatrix[row][column] == " ";
        }

        public bool AddElement(int column, int row, string gamePiece)
        {
            if (!IsCellEmpty(column, row))
            {
                return false;
            }

            gridMatrix[row][column] = gamePiece;
            return true;
        }

        public void RemoveElements(List<int[]> positions, string gamePieceToRemove)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                int column = positions[i][0];
                int row = positions[i][1];

                if (IsInsideBoard(column, row) &&
                    gridMatrix[row][column] == gamePieceToRemove)
                {
                    gridMatrix[row][column] = " ";
                }
            }
        }

        /* public bool IsBoardFull()
        {
            for (int row = 0; row < GetRowCount(); row++)
            {
                for (int column = 0; column < GetColumnCount(); column++)
                {
                    if (gridMatrix[row][column] == " ")
                    {
                        return false;
                    }
                }
            } 

            return true;
        } */

        private bool IsInsideBoard(int column, int row) //to avoid invalid positions
        {
            return row >= 0 &&
                   row < GetRowCount() &&
                   column >= 0 &&
                   column < GetColumnCount();
        }


    }

}

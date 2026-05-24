namespace OOP_BoardGames_Framework
{
    public class Board
    {
        // A list of string arrays for the board 
        private List<string[]> gridMatrix;
        // Allows other classes to get and set the board matrix 
        public List<string[]> GridMatrix
        {
            get { return gridMatrix; }
            set { gridMatrix = value; }
        }
        // Board Constructor 
        public Board()
        {
            gridMatrix = new List<string[]>();
        }
        // Constructor that iterates rows and creates " " columns
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
        //get total rows for displaying the board.
        public int GetRowCount()
        {
            return gridMatrix.Count;
        }
        //get total cols for displaying the board.
        public int GetColumnCount()
        {
            return gridMatrix[0].Length;
        }
        public string GetCell(int column, int row)
        {
            return gridMatrix[row][column];
        }
        // Returns empty if " "
        public bool IsCellEmpty(int column, int row)
        {
            if (!IsInsideBoard(column, row))
            {
                return false;
            }
            return gridMatrix[row][column] == " ";
        }
        // Adds the game piece if empty 
        public bool AddElement(int column, int row, string gamePiece)
        {
            if (!IsCellEmpty(column, row))
            {
                return false;
            }
            gridMatrix[row][column] = gamePiece;
            return true;
        }
        // Removes game piece. Only does it if it is the correct one to remove
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
        public void Clear()
        {
            for (int row = 0; row < GetRowCount(); row++)
            {
                for (int column = 0; column < GetColumnCount(); column++)
                {
                    gridMatrix[row][column] = " ";
                }
            }
        }
        public bool IsBoardFull()
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
        }
        //to avoid invalid positions
        private bool IsInsideBoard(int column, int row)
        {
            return row >= 0 &&
                row < GetRowCount() &&
                column >= 0 &&
                column < GetColumnCount();
        }
    }
}
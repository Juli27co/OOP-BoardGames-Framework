namespace OOP_BoardGames_Framework
{
    public class NotaktoRules : GameRules
    {
        private const int BoardSize = 3;
        private const int NumberOfBoards = 3;
        private const string Piece = "X";
        public override int Columns => BoardSize;
        public override int Rows => BoardSize;
        public override int WinLength => BoardSize;
        // Checking valid moves
        public override bool ValidatePlayerMove(Board board, PlayerMove move)
        {
            if (!TryParseMove(move, out int boardIndex, out int column, out int row))
            {
                return false;
            }
            if (IsBoardDead(board, boardIndex))
            {
                return false;
            }
            int globalColumn = ToGlobalColumn(boardIndex, column);
            return board.IsCellEmpty(globalColumn, row);
        }
        // Place x at the cell of the selected board
        public override void ExecuteMove(Board board, PlayerMove move, string playerSymbol)
        {
            if (!TryParseMove(move, out int boardIndex, out int column, out int row))
            {
                throw new InvalidOperationException("Invalid move. Try again");
            }
            int globalColumn = ToGlobalColumn(boardIndex, column);
            board.AddElement(globalColumn, row, Piece);
        }
        public override bool CheckForWinning(Board board)
        {
            for (int b = 0; b < NumberOfBoards; b++)
            {
                if (!IsBoardDead(board, b))
                {
                    return false;
                }
            }
            return true;
        }
        //There are no draws
        public override bool CheckForDraw(Board board)
        {
            return false;
        }

        public bool[] GetDeadBoards(Board board)
        {
            bool[] dead = new bool[NumberOfBoards];
            for (int b = 0; b < NumberOfBoards; b++)
            {
                dead[b] = IsBoardDead(board, b);
            }
            return dead;
        }
        // Check whether the board has a three-in-a-row of X.
        private bool IsBoardDead(Board board, int boardIndex)
        {
            int colOffset = boardIndex * BoardSize;
            for (int row = 0; row < BoardSize; row++)
            {
                if (IsLineComplete(board, colOffset, row, 1, 0))
                {
                    return true;
                }
            }
            for (int col = 0; col < BoardSize; col++)
            {
                if (IsLineComplete(board, colOffset + col, 0, 0, 1))
                {
                    return true;
                }
            }
            if (IsLineComplete(board, colOffset, 0, 1, 1))
            {
                return true;
            }
            if (IsLineComplete(board, colOffset + BoardSize - 1, 0, -1, 1))
            {
                return true;
            }
            return false;
        }
        // Return ture if all cells have X
        private bool IsLineComplete(Board board, int startCol, int startRow, int dc, int dr)
        {
            for (int i = 0; i < WinLength; i++)
            {
                int col = startCol + i * dc;
                int row = startRow + i * dr;
                if (col < 0 || col >= board.GetColumnCount() ||
                    row < 0 || row >= board.GetRowCount())
                {
                    return false;
                }
                if (board.GetCell(col, row) != Piece)
                {
                    return false;
                }
            }
            return true;
        }
        private int ToGlobalColumn(int boardIndex, int localColumn)
        {
            return boardIndex * BoardSize + localColumn;
        }
        //Return all valid moves
        public override List<PlayerMove> GetValidMoves(Board board, Player player)
        {
            List<PlayerMove> moves = new List<PlayerMove>();
            for (int boardNumber = 1; boardNumber <= NumberOfBoards; boardNumber++)
            {
                for (int position = 1; position <= BoardSize * BoardSize; position++)
                {
                    PlayerMove move = new PlayerMove(boardNumber + "," + position, player, 0);
                    if (ValidatePlayerMove(board, move))
                    {
                        moves.Add(move);
                    }
                }
            }
            return moves;
        }
        //Remove the previously placed X
        public override void UndoMove(Board board, PlayerMove move)
        {
            if (TryParseMove(move, out int boardIndex, out int column, out int row))
            {
                int globalColumn = ToGlobalColumn(boardIndex, column);
                RemovePiece(board, globalColumn, row, Piece);
            }
        }
        private bool TryParseMove(PlayerMove move, out int boardIndex, out int column, out int row)
        {
            boardIndex = 0;
            column = 0;
            row = 0;
            if (move == null || string.IsNullOrWhiteSpace(move.Move))
            {
                return false;
            }
            string[] parts = move.Move.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }
            if (!int.TryParse(parts[0].Trim(), out int boardNumber) ||
                !int.TryParse(parts[1].Trim(), out int position))
            {
                return false;
            }
            if (boardNumber < 1 || boardNumber > NumberOfBoards)
            {
                return false;
            }
            if (position < 1 || position > BoardSize * BoardSize)
            {
                return false;
            }
            boardIndex = boardNumber - 1;
            position--;
            row = position / BoardSize;
            column = position % BoardSize;
            return true;
        }
    }
}
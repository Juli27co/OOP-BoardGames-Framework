namespace OOP_BoardGames_Framework
{
    public class GameRecord
    {
        public GameType GameType { get; set; }
        public GameMode GameMode { get; set; }
        public Board CurrentBoard { get; set; } = null!;
        // public int TotalRows { get; set; }
        // public int TotalColumns { get; set; }
        public Dictionary<string, int> GameParameters { get; set; } = new();
        public Stack<PlayerMove> MovesLog { get; set; } = new();
        public Stack<PlayerMove> RedoMoves { get; set; } = new();
        public GameRecord() { }
        public void UpdateGameState(Board board, GameType gameType, GameMode gameMode, Dictionary<string, int> gameParameters)
        {
            this.CurrentBoard = board;
            this.GameType = gameType;
            this.GameMode = gameMode;
            // this.TotalRows = board.GetRowCount();
            // this.TotalColumns = board.GetColumnCount();
            this.GameParameters = gameParameters;
        }
        public void LogMove(PlayerMove move)
        {
            MovesLog.Push(move);
            RedoMoves.Clear();
        }
        public void UndoMove()
        {
            if (MovesLog.Count == 0)
            {
                Logger.WriteLine("There are no moves to undo.");
                return;
            }
            PlayerMove removedMove = MovesLog.Pop();
            RedoMoves.Push(removedMove);
        }
        public void RedoMove()
        {
            PlayerMove redoMove = RedoMoves.Pop();
            MovesLog.Push(redoMove);
        }
    }
}
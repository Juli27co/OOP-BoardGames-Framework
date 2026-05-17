namespace OOP_BoardGames_Framework
{
    public class GameRecord
    {
        public GameType GameType { get; set; }
        public Board CurrentBoard { get; set; }
        public GameMode GameMode { get; set; }
        public Stack<PlayerMove> movesLog { get; set; } = new Stack<PlayerMove>();
        public Stack<PlayerMove> redoMoves { get; set; } = new Stack<PlayerMove>();

        public GameRecord() { }
        public GameRecord(Board board, GameType gameType, GameMode gameMode)
        {
            this.CurrentBoard = board;
            this.GameType = gameType;
            this.GameMode = gameMode;
        }


        public void LogMove(PlayerMove move)
        {
            movesLog.Push(move);
            redoMoves.Clear();
        }
        public PlayerMove Undo()
        {
            PlayerMove removedMove = movesLog.Pop();
            redoMoves.Push(removedMove);
            return movesLog.Peek();
        }
        public PlayerMove Redo()
        {
            PlayerMove redoMove = redoMoves.Pop();
            movesLog.Push(redoMove);
            return movesLog.Peek();
        }

    }
}
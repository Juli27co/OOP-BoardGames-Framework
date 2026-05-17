namespace OOP_BoardGames_Framework
{
    public class GameRecord
    {
        public GameType GameType { get; set; }
        public Board CurrentBoard { get; set; } = null!;
        public GameMode GameMode { get; set; }
        public Stack<PlayerMove> MovesLog { get; set; } = new Stack<PlayerMove>();
        public Stack<PlayerMove> RedoMoves { get; set; } = new Stack<PlayerMove>();
        public GameRecord() { }
        public void UpdateGameState(Board board, GameType gameType, GameMode gameMode)
        {
            this.CurrentBoard = board;
            this.GameType = gameType;
            this.GameMode = gameMode;
        }
        public void LogMove(PlayerMove move)
        {
            MovesLog.Push(move);
            RedoMoves.Clear();
        }
        public void UndoMove()
        {
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
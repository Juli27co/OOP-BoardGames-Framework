namespace OOP_BoardGames_Framework
{
    public class GameRecord
    {
        public GameType GameType { get; set; }
        public Board CurrentBoard { get; set; }
        public GameMode GameMode { get; set; }
        public Stack<PlayerMove> movesLog { get; set; } = new Stack<PlayerMove>();
        public Stack<PlayerMove> RedoMoves { get; set; } = new Stack<PlayerMove>();

        public GameRecord() { }
        public GameRecord(Board board, GameType gameType, GameMode gameMode)
        {
            this.CurrentBoard = board;
            this.GameType = gameType;
            this.GameMode = gameMode;
        }

        public void LogMove(Player player, PlayerMove move) { }
        public void Undo() { }
        public void Redo() { }

    }
}
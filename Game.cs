namespace OOP_BoardGames_Framework
{
    public class Game
    {
        private Board currentBoard;
        private GameRules gameRules;
        public Game(Board currentBoard, GameRules gameRules)
        {
            this.currentBoard = currentBoard;
            this.gameRules = gameRules;
        }
        public bool ExecutePlayerAction(PlayerMove move)
        {
            if (!gameRules.ValidatePlayerMove(currentBoard, move))
            {
                return false;
            }
            string playerSymbol = move.Player.GamePiece;
            gameRules.ExecuteMove(currentBoard, move, playerSymbol);
            return true;
        }
        public Board GetCurrentBoard()
        {
            return currentBoard;
        }
        public GameRules GetGameRules()
        {
            return gameRules;
        }
    }
}
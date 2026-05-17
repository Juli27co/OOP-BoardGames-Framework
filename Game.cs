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
        public void ExecutePlayerAction(PlayerMove move)
        {
            if (!gameRules.ValidatePlayerMove(currentBoard, move))
            {
                return;
            }
            string playerSymbol = move.Player.GamePiece;
            gameRules.ExecuteMove(currentBoard, move, playerSymbol);
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
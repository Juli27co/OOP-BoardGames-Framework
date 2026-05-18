namespace OOP_BoardGames_Framework
{
    public static class GameRulesFactory
    {
        public static GameRules CreateGameRules(GameType gameType, int gridSize = 3)
        {
            switch (gameType)
            {
                case GameType.TicTacToe:
                    return new TicTacToeRules();
                case GameType.ConnectFour:
                    return new ConnectFourRules();
                case GameType.NumericalTicTacToe:
                    return new NumericalTicTacToeRules(gridSize);
                case GameType.Notakto:
                    return new NotaktoRules();
                case GameType.Gomoku:
                    return new GomokuRules();
                default:
                    throw new ArgumentException("Unknown game type.");
            }
        }
    }
}
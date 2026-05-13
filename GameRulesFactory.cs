public static class GameRulesFactory
{
    public static GameRules CreateRules(string gameType) //Worried about this limiting extensibility. Thoughts on a dictionary here instead?
    {
        if (gameType == TicTacToe)
        {
            return new TicTacToeRules();
        }
        if (gameType == ConnectFour)
        {
            return new ConnectFourRules();
        }
        if (gameType == NumericalTicTacToe)
        {
            return new NumericalTicTacToeRules();
        }
        if (gameType == Notakto)
        {
            return new NotaktoRules();
        }
        if (gameType == Gomoku)
        {
            return new GomokuRules();
        }
    }
}
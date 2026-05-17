using OOP_BoardGames_Framework;

class Program
{
    static void Main(string[] args)
    {
        // Create two players, leaveing it as Human for now so we can keep working on it
        Player player1 = new Human(1, "X");
        Player player2 = new Human(2, "O");

        // Also defaulted to HumanVsHuman and ConnectFour for now
        GameMode mode = GameMode.HumanVsHuman;
        GameType type = GameType.ConnectFour;

        GameManager gameManager = new GameManager(mode, type, player1, player2);
        gameManager.Run();
    }
}
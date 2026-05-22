using OOP_BoardGames_Framework;
class Program
{
    static void Main(string[] args)
    {
        bool keepPlaying = true;
        while (keepPlaying)
        {
            GameManager gameManager = new GameManager();
            gameManager.Run();
            Logger.PrintHeader("GAME ENDED");
            Logger.PrintOption(1, "Start New Game");
            Logger.PrintOption(2, "Exit");
            int selection = Logger.ReadInt("Enter your choice (1-2): ", 1, 2);
            if (selection == 2)
            {
                keepPlaying = false;
            }
        }
        Logger.WriteLine("Thanks for playing.");
    }
}
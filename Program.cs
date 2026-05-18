using OOP_BoardGames_Framework;
class Program
{
    static void Main(string[] args) // Create two players, leaveing it as Human for now so we can keep working on it
    {
        Player player1 = new Human(1, "X");
        Player player2 = new Human(2, "O");
        // Also defaulted to HumanVsHuman and ConnectFour for now
        GameMode mode = GameMode.HumanVsHuman;
        GameType type = GameType.ConnectFour;
        GameManager gameManager = new GameManager(mode, type, player1, player2);
        gameManager.Run();



        BoardDisplay display = new BoardDisplay();

        // Test Tic-Tac-Toe
        GameRules tttRules = new TicTacToeRules();
        Board tttBoard = tttRules.CreateGrid();

        tttBoard.AddElement(0, 0, "X");
        tttBoard.AddElement(1, 1, "O");

        Console.WriteLine("Tic-Tac-Toe Board:");
        display.ShowCommonBoard(tttBoard);

        Console.WriteLine();


        // Test Connect Four
        GameRules connectRules = new ConnectFourRules();
        Board connectBoard = connectRules.CreateGrid();

        connectBoard.AddElement(0, 5, "X");
        connectBoard.AddElement(1, 5, "O");

        Console.WriteLine("Connect Four Board:");
        display.ShowConnectFourBoard(connectBoard);

        Console.WriteLine();


        // Test Gomoku
        GameRules gomokuRules = new GomokuRules();
        Board gomokuBoard = gomokuRules.CreateGrid();

        gomokuBoard.AddElement(7, 7, "X");
        gomokuBoard.AddElement(8, 7, "O");

        Console.WriteLine("Gomoku Board:");
        display.ShowGomokuBoard(gomokuBoard);
    }
}
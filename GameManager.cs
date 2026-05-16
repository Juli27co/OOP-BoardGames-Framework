namespace OOP_BoardGames_Framework
{
    public enum GameMode
    {
        HumanVsHuman,
        HumanVsAI,
    }

    public enum GameType
    {
        ConnectFour,
        Gomoku,
        TicTacToe,
        NumericalTicTacToe,
        Notakto,
        // Add more game types as needed
    }

    internal class GameManager
    {
        private int turnCounter;
        private List<Player> players;
        private Board? board;
        private GameRules? rules;
        
        public GameMode Mode { get; private set; }
        public GameType Type { get; private set; }

        public GameManager(GameMode mode, GameType type, Player player1, Player player2)
        {
            Mode = mode;
            Type = type;
            players = new List<Player> { player1, player2 };
            turnCounter = 0;
        }

        public GameManager()
        {
            players = new List<Player>();
            turnCounter = 0;
        }

        public void Run()
        {
            Initialize();
            
            bool gameEnded = false;
            while (!gameEnded)
            {
                int currentPlayerIndex = turnCounter % 2;
                Player currentPlayer = players[currentPlayerIndex];
                Logger.WriteLine($"Turn {turnCounter + 1}: Player {currentPlayer.PlayerId}'s move.");
                // Execute the player's action

                string action = currentPlayer.RequestAction(board!, rules!, turnCounter + 1);
                Logger.WriteLine($"Player {currentPlayer.PlayerId} action: {action}");


                // For now, simulate a move and end after 10 turns
                turnCounter++;

                bool checkForWin = turnCounter >= 10; // Replace with actual win condition check
                if (checkForWin)
                {
                    Logger.WriteLine("Game ended (demo mode, 10 turns max).");
                    gameEnded = true;
                }
            }
        }

        private void Initialize()
        {
            // Select game type
            GameType selectedGameType = SelectGameType();
            Type = selectedGameType;

            // Get any game-specific parameters (extensible for future iterations)
            Dictionary<string, object> gameParameters = GetAdditionalGameParameters(selectedGameType);

            // Select game mode
            GameMode selectedGameMode = SelectGameMode();
            Mode = selectedGameMode;

            // Create players based on selected mode
            players = CreatePlayers(selectedGameMode);

            // Initialize board and rules
            board = new Board();
            rules = GameRulesFactory.CreateGameRules(selectedGameType);

            Logger.WriteLine($"\nGame initialized: {Type} - {Mode}");
            Logger.WriteLine($"Player 1: {players[0].GetType().Name}, Player 2: {players[1].GetType().Name}\n");
        }

        private GameType SelectGameType()
        {
            Logger.PrintHeader("SELECT GAME TYPE");
            GameType[] gameTypes = (GameType[])Enum.GetValues(typeof(GameType));

            for (int i = 0; i < gameTypes.Length; i++)
            {
                Logger.PrintOption(i + 1, gameTypes[i].ToString());
            }

            int selection = Logger.ReadInt($"Enter your choice (1-{gameTypes.Length}): ", 1, gameTypes.Length);
            return gameTypes[selection - 1];
        }

        private GameMode SelectGameMode()
        {
            Logger.PrintHeader("SELECT GAME MODE");
            GameMode[] gameModes = (GameMode[])Enum.GetValues(typeof(GameMode));

            for (int i = 0; i < gameModes.Length; i++)
            {
                Logger.PrintOption(i + 1, gameModes[i].ToString());
            }

            int selection = Logger.ReadInt($"Enter your choice (1-{gameModes.Length}): ", 1, gameModes.Length);
            return gameModes[selection - 1];
        }

        private List<Player> CreatePlayers(GameMode mode)
        {
            if (mode == GameMode.HumanVsHuman)
            {
                return new List<Player>
                {
                    new Human(1),
                    new Human(2)
                };
            }
            else if (mode == GameMode.HumanVsAI)
            {
                return new List<Player>
                {
                    new Human(1),
                    new Human(2) // TODO: Replace with AI player when available
                };
            }

            throw new ArgumentException("Unknown game mode.");
        }

        private Dictionary<string, object> GetAdditionalGameParameters(GameType gameType)
        {
            // Placeholder for future iterations to collect game-specific parameters
            // (e.g., board size for Gomoku)
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            switch (gameType)
            {
                case GameType.Gomoku:
                    // TODO: Ask user for board size in future iteration
                    break;
                case GameType.ConnectFour:
                case GameType.TicTacToe:
                case GameType.NumericalTicTacToe:
                case GameType.Notakto:
                    // No additional parameters needed
                    break;
            }

            return parameters;
        }
    }
}

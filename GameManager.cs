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
        private Game? gameState;
        
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
                //TODO: Display the current game state here (board, scores, etc.)

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

            // Initialize board and rules based on selected game
            int rows = 3;
            int cols = 3;
            if (gameParameters.ContainsKey("rows") && gameParameters.ContainsKey("cols"))
            {
                rows = (int)gameParameters["rows"];
                cols = (int)gameParameters["cols"];
            }

            board = new Board(rows, cols);
            rules = GameRulesFactory.CreateGameRules(selectedGameType);
            gameState = new Game(board, rules);

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
            // Collect board size (rows/cols) according to game-specific rules
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            switch (gameType)
            {
                case GameType.TicTacToe:
                case GameType.Notakto:
                    // Fixed 3x3 with 3 boards for Notakto
                    parameters["rows"] = 3;
                    parameters["cols"] = 3;
                    parameters["numberOfBoards"] = 3;
                    break;

                case GameType.ConnectFour:
                    // Standard Connect Four board is 6 rows x 7 columns
                    parameters["rows"] = 6;
                    parameters["cols"] = 7;
                    break;

                case GameType.Gomoku:
                    // Standard Gomoku board is 15x15
                    parameters["rows"] = 15;
                    parameters["cols"] = 15;
                    break;

                case GameType.NumericalTicTacToe:
                    // Ask user for custom board size
                    Logger.PrintHeader("NUMERICAL TIC TAC TOE - BOARD SIZE");
                    Logger.WriteLine("You chose Numerical TicTacToe. Please enter the desired board dimensions.");
                    int minSize = 1;
                    int maxSize = 50;
                    int rows = Logger.ReadInt($"Number of rows ({minSize}-{maxSize}): ", minSize, maxSize);
                    int cols = Logger.ReadInt($"Number of columns ({minSize}-{maxSize}): ", minSize, maxSize);
                    parameters["rows"] = rows;
                    parameters["cols"] = cols;
                    break;

                default:
                    // Fallback to 3x3
                    parameters["rows"] = 3;
                    parameters["cols"] = 3;
                    break;
            }

            return parameters;
        }
    }
}

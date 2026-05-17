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

        public GameRecord GameRecord { get; private set; }

        private FileManager fileManager;

        public GameManager(GameMode mode, GameType type, Player player1, Player player2)
        {
            Mode = mode;
            Type = type;
            players = new List<Player> { player1, player2 };
            turnCounter = 0;
            GameRecord = new GameRecord();
            fileManager = new FileManager();
        }

        public GameManager()
        {
            players = new List<Player>();
            turnCounter = 0;
            GameRecord = new GameRecord();
            fileManager = new FileManager();
        }

        public void Run()
        {
            Initialize();

            bool gameEnded = false;
            while (!gameEnded)
            {
                //TODO: Display the current game state here (board, scores, etc.)
                DisplayCurrentBoard();

                int currentPlayerIndex = turnCounter % 2;
                Player currentPlayer = players[currentPlayerIndex];
                Logger.WriteLine($"Turn {turnCounter + 1}: Player {currentPlayer.PlayerId}'s move.");
                // Execute the player's action

                string action = currentPlayer.RequestAction(board!, rules!, turnCounter + 1);
                Logger.WriteLine($"Player {currentPlayer.PlayerId} action: {action}");

                if (action == "save")
                {
                    GameRecord.UpdateGameState(board, Type, Mode);
                    fileManager.SaveGame(GameRecord);
                    Logger.WriteLine($"Your game will be saved and you'll exit the game.");
                    gameEnded = true;
                    break;
                }

                // Put disc in board
                PlayerMove move = new PlayerMove(action, currentPlayer, turnCounter + 1);
                gameState!.ExecutePlayerAction(move);
                GameRecord.LogMove(move);
                if (rules!.CheckForWinning(board!))
                {
                    DisplayCurrentBoard();
                    Logger.WriteLine($"Player {currentPlayer.PlayerId} wins!");
                    gameEnded = true;
                }
                else if (rules.CheckForDraw(board!))
                {
                    DisplayCurrentBoard();
                    Logger.WriteLine("Game ended in a draw.");
                    gameEnded = true;
                }
                else
                {
                    turnCounter++;
                }
            }
        }
        private void DisplayCurrentBoard()
        {
            BoardDisplay boardDisplay = new BoardDisplay();

            if (Type == GameType.ConnectFour)
            {
                boardDisplay.ShowConnectFourBoard(board!);
            }
            else if (Type == GameType.Gomoku)
            {
                boardDisplay.ShowGomokuBoard(board!);
            }
            else
            {
                boardDisplay.ShowCommonBoard(board!);
            }
        }
        private void Initialize()
        {
            GameType selectedGameType;
            GameMode selectedGameMode;
            // Select new game or load game
            bool isNewGame = SelectStartOption() == 1;
            if (isNewGame)
            {
                // Select game type & game mode
                selectedGameType = SelectGameType();
                Type = selectedGameType;
                selectedGameMode = SelectGameMode();
                Mode = selectedGameMode;
            }
            else
            {
                FileManager fileManager = new FileManager();
                GameRecord = fileManager.LoadGame();
                // Restore game type & game mode
                Type = GameRecord.GameType;
                selectedGameType = GameRecord.GameType;
                Mode = GameRecord.GameMode;
                selectedGameMode = GameRecord.GameMode;
            }

            // Get any game-specific parameters (extensible for future iterations)
            Dictionary<string, object> gameParameters = GetAdditionalGameParameters(selectedGameType);

            rules = GameRulesFactory.CreateGameRules(selectedGameType);

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

            if (isNewGame)
            {
                board = new Board(rows, cols);
            }
            else
            {
                board = GameRecord.CurrentBoard;
                turnCounter = GameRecord.MovesLog.Count() + 1;
            }
            gameState = new Game(board, rules);



            Logger.WriteLine($"\nGame initialized: {Type} - {Mode}");
            Logger.WriteLine($"Player 1: {players[0].GetType().Name}, Player 2: {players[1].GetType().Name}\n");
        }


        private int SelectStartOption()
        {
            if (!File.Exists(fileManager.SaveDirectory + fileManager.SaveFileName + ".json"))
            {
                return 1;
            }
            else
            {
                Logger.PrintHeader("SELECT AN OPTION");
                Logger.PrintOption(1, "Start New Game");
                Logger.PrintOption(2, "Continue Saved Game");
                int selection = Logger.ReadInt($"Enter your choice (1-2): ", 1, 2);
                return selection;
            }
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
            LineBasedGameRules lineRules = (LineBasedGameRules)rules!;

            if (mode == GameMode.HumanVsHuman)
            {
                return new List<Player>
                {
                    new Human(1, lineRules.Player1Symbol),
                    new Human(2, lineRules.Player2Symbol)
                };
            }
            else if (mode == GameMode.HumanVsAI)
            {
                return new List<Player>
                {
                    new Human(1, lineRules.Player1Symbol),
                    new Human(2, lineRules.Player2Symbol) // TODO: Replace with AI player when available
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

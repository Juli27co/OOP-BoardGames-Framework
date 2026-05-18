namespace OOP_BoardGames_Framework
{
    public enum GameMode
    { HumanVsHuman, HumanVsAI, }
    public enum GameType
    { ConnectFour, Gomoku, TicTacToe, NumericalTicTacToe, Notakto, } // Add more game types as needed
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
                Console.Clear();
                int currentPlayerIndex = turnCounter % 2;
                Player currentPlayer = players[currentPlayerIndex];

                // Display turn information header
                Logger.PrintTurnHeader(Type, Mode, turnCounter + 1, currentPlayer);

                // Display the current board
                DisplayCurrentBoard();

                // Request player action
                string action = currentPlayer.RequestAction(board!, rules!, turnCounter + 1);
                Logger.WriteLine($"Player {currentPlayer.PlayerId} action: {action}");

                if (action == "save")
                {
                    GameRecord.UpdateGameState(board, Type, Mode);
                    fileManager.SaveGame(GameRecord);
                    Logger.WriteLine($"Your game will be saved and you'll exit the game.");
                    break;
                }
                else if (action == "undo" || action == "redo")
                {
                    if (action == "undo")
                    {
                        GameRecord.UndoMove();
                    }
                    else
                    {
                        GameRecord.RedoMove();
                    }

                    board.Clear();
                    foreach (PlayerMove restoreMove in GameRecord.MovesLog.Reverse())
                    {
                        gameState!.ExecutePlayerAction(restoreMove);
                    }

                    turnCounter = GameRecord.MovesLog.Count();
                    continue;
                }
                else if (action == "help")
                {
                    Logger.ShowHelpMessage();
                    Logger.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    continue;
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
            Logger.PrintBoardSectionStart();
            BoardDisplay boardDisplay = new BoardDisplay();
            if (Type == GameType.ConnectFour)
            {
                boardDisplay.ShowConnectFourBoard(board!);
            }
            else if (Type == GameType.Gomoku)
            {
                boardDisplay.ShowGomokuBoard(board!);
            }
            else if (Type == GameType.NumericalTicTacToe)
            {
                boardDisplay.ShowNumericalTicTacToeBoard(board!);
            }
            else
            {
                boardDisplay.ShowCommonBoard(board!);
            }
            Logger.PrintBoardSectionEnd();
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
                turnCounter = GameRecord.MovesLog.Count();
            }
            gameState = new Game(board, rules);


            Logger.WriteLine($"\nGame initialized: {Type} - {Mode}");
            Logger.WriteLine($"Player 1: {players[0].GetType().Name}, Player 2: {players[1].GetType().Name}\n");
            Logger.Clear();
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
            string player1Symbol;
            string player2Symbol;
            if (Type == GameType.NumericalTicTacToe)
            {
                player1Symbol = "Odd";
                player2Symbol = "Even";
            }
            else if (Type == GameType.Notakto)
            {
                player1Symbol = "X";
                player2Symbol = "X"; // Placeholder for Notakto
            }
            else
            {
                LineBasedGameRules lineRules = (LineBasedGameRules)rules!;
                player1Symbol = lineRules.Player1Symbol;
                player2Symbol = lineRules.Player2Symbol;
            }
            if (mode == GameMode.HumanVsHuman)
            {
                return new List<Player>
        {
            new Human(1, player1Symbol),
            new Human(2, player2Symbol)
        };
            }
            else if (mode == GameMode.HumanVsAI)
            {
                return new List<Player>
        {
            new Human(1, player1Symbol),
            new Human(2, player2Symbol) // TODO: Replace with AI player when available
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
                    parameters["rows"] = 3;
                    parameters["cols"] = 3;
                    break;

                case GameType.Notakto:
                    parameters["rows"] = 3;
                    parameters["cols"] = 9;
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
                    Logger.PrintHeader("NUMERICAL TIC TAC TOE - BOARD SIZE");
                    Logger.WriteLine("Enter one number only. Example: 3 creates a 3x3 board.");
                    int gridSize = Logger.ReadInt("Board size: ", 3, 9);
                    parameters["rows"] = gridSize;
                    parameters["cols"] = gridSize;
                    parameters["gridSize"] = gridSize;
                    break;
                default:
                    throw new ArgumentException("Unknown game type.");
            }
            return parameters;
        }
    }
}
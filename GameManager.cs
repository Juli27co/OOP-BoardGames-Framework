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
        private Dictionary<string, int> gameParameters;
        private FileManager fileManager;
        public GameManager()
        {
            players = new List<Player>();
            turnCounter = 0;
            GameRecord = new GameRecord();
            fileManager = new FileManager();
            gameParameters = new Dictionary<string, int>();
        }
        public void Run()
        {
            Initialize();
            GameRecord.UpdateGameState(board!, Type, Mode, gameParameters);
            bool gameEnded = false;
            while (!gameEnded)
            {
                Console.Clear();
                int currentPlayerIndex = turnCounter % 2;
                Player currentPlayer = players[currentPlayerIndex];
                Logger.PrintTurnHeader(Type, Mode, turnCounter + 1, currentPlayer);// Display turn information header
                DisplayCurrentBoard();// Display the current board
                string action = currentPlayer.RequestAction(board!, rules!, turnCounter + 1);// Request player action
                bool isValidate = true;
                if (string.IsNullOrWhiteSpace(action))
                {
                    Logger.WriteLine("No valid move found.");
                    gameEnded = true;
                    continue;
                }
                Logger.WriteLine($"Player {currentPlayer.PlayerId} action: {action}");
                if (action == "save")
                {
                    Logger.PrintHeader("SELECT SAVE FORMAT");
                    Logger.PrintOption(1, "Save as txt file");
                    Logger.PrintOption(2, "Save as json file");
                    int selection = Logger.ReadInt($"Enter your choice (1-2): ", 1, 2);
                    GameRecord.UpdateGameState(board!, Type, Mode, gameParameters);
                    fileManager.SaveGame(GameRecord, selection);

                    Logger.PrintHeader("CONTINUE CURRENT GAME?");
                    Logger.PrintOption(1, "Yes");
                    Logger.PrintOption(2, "No");
                    int gameContinue = Logger.ReadInt("Enter your choice (1-2): ", 1, 2);
                    if (gameContinue == 1) continue;
                    if (gameContinue == 2) break;
                }
                else if (action == "undo" || action == "redo")
                {
                    if (action == "undo")
                    {
                        if (GameRecord.MovesLog.Count() == 0)
                        {
                            Logger.WriteLine("There are no previous turns available. Press Enter to try again.");
                            Console.ReadLine();
                            continue;
                        }
                        GameRecord.UndoMove();
                    }
                    else
                    {
                        if (GameRecord.RedoMoves.Count() == 0)
                        {
                            Logger.WriteLine("There are no turns available to redo. Press Enter to try again.");
                            Console.ReadLine();
                            continue;
                        }
                        GameRecord.RedoMove();
                    }
                    board!.Clear();
                    foreach (PlayerMove restoreMove in GameRecord.MovesLog.Reverse())
                    {
                        isValidate = gameState!.ExecutePlayerAction(restoreMove);
                        if (!isValidate)
                        {
                            Logger.WriteLine("Invalid move. Press Enter to try again.");
                            Console.ReadLine();
                            continue;
                        }
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
                PlayerMove move = new PlayerMove(action, currentPlayer, turnCounter + 1);// Put disc in board
                isValidate = gameState!.ExecutePlayerAction(move);
                if (!isValidate)
                {
                    Logger.WriteLine("Invalid move. Press Enter to try again.");
                    Console.ReadLine();
                    continue;
                }
                GameRecord.LogMove(move);

                if (rules!.CheckForWinning(board!))
                {
                    DisplayCurrentBoard();
                    if (Type == GameType.Notakto)
                        Logger.WriteLine($"Player {currentPlayer.PlayerId} loses!");
                    else
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
            else if (Type == GameType.NumericalTicTacToe)
            {
                boardDisplay.ShowNumericalTicTacToeBoard(board!);
            }
            else if (Type == GameType.Notakto)
            {
                boardDisplay.ShowNotaktoBoard(board!);
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
            bool isNewGame;

            while (true) // Return to the menu if loading fails.
            {
                isNewGame = SelectStartOption() == 1;// Select new game or load game
                if (isNewGame)
                {
                    selectedGameType = SelectGameType();// Select game type & game mode
                    Type = selectedGameType;
                    selectedGameMode = SelectGameMode();
                    Mode = selectedGameMode;
                    gameParameters = GetAdditionalGameParameters(selectedGameType);
                }
                else
                {
                    Logger.PrintHeader("SELECT LOAD FORMAT");
                    Logger.PrintOption(1, "Load from txt file");
                    Logger.PrintOption(2, "Load from json file");
                    int selection = Logger.ReadInt($"Enter your choice (1-2): ", 1, 2);
                    FileManager fileManager = new FileManager();
                    try
                    {
                        GameRecord = fileManager.LoadGame(selection);// Restore game type & game mode
                        Type = GameRecord.GameType;
                        selectedGameType = GameRecord.GameType;
                        Mode = GameRecord.GameMode;
                        selectedGameMode = GameRecord.GameMode;
                        gameParameters = GameRecord.GameParameters;
                    }
                    catch (FileNotFoundException ex)
                    {
                        Logger.WriteLine(ex.Message);
                        continue;
                    }

                }
                break;
            }
            int gridSize = gameParameters.ContainsKey("gridSize") ? (int)gameParameters["gridSize"] : 3;
            rules = GameRulesFactory.CreateGameRules(selectedGameType, gridSize);
            players = CreatePlayers(selectedGameMode);// Create players based on selected mode
            int rows = 3;// Initialize board and rules based on selected game
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
            // Check if saved game folder exists. If not, only allow starting a new game (Option 1).
            if (!Directory.Exists(fileManager.SaveDirectory))
            {
                return 1;
            }
            else
            {
                Logger.PrintHeader("SELECT AN OPTION");
                Logger.PrintOption(1, "Start New Game");
                Logger.PrintOption(2, "Load Saved Game");
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
            new Computer(2, player2Symbol)
        };
            }
            throw new ArgumentException("Unknown game mode.");
        }
        private Dictionary<string, int> GetAdditionalGameParameters(GameType gameType)
        {
            Dictionary<string, int> parameters = new Dictionary<string, int>();// Collect board size (rows/cols) according to game-specific rules
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
                case GameType.ConnectFour:// Standard Connect Four board is 6 rows x 7 columns
                    parameters["rows"] = 6;
                    parameters["cols"] = 7;
                    break;
                case GameType.Gomoku:// Standard Gomoku board is 15x15
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
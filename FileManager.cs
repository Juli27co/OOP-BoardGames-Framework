namespace OOP_BoardGames_Framework
{
    using System.Text.Json;
    using System.Text;
    public class FileManager
    {
        public string SaveDirectory { get; } = "savedFile/";
        private string saveFileName = "savedGameFile";

        private string GetFilePath(int fileFormat)
        {
            string extension = fileFormat == 1 ? ".txt" : ".json";
            return SaveDirectory + saveFileName + extension;
        }

        // Saves the game record using the selected file format. ( [fileFormat] 1:txt / 2:json )
        public void SaveGame(GameRecord game, int fileFormat)
        {
            //  Generate the file contents to save.
            string contents = "";
            if (fileFormat == 1)
            {
                StringBuilder savedData = new();
                savedData = SaveTxt(game, savedData);
                contents = savedData.ToString();
            }
            else
            {
                Object savedData = new
                {
                    GameType = game.GameType,
                    GameMode = game.GameMode,
                    GameParameters = game.GameParameters,
                    CurrentBoard = game.CurrentBoard,
                    MovesLog = game.MovesLog.Reverse()
                };

                string jsonString = JsonSerializer.Serialize(savedData);
                contents = jsonString;

            }
            // Make a folder empty to avoid save 2 types of files.
            if (Directory.Exists(SaveDirectory))
            {
                foreach (string oldFile in Directory.GetFiles(SaveDirectory))
                {
                    File.Delete(oldFile);
                }
            }

            Directory.CreateDirectory(SaveDirectory);
            File.WriteAllText(GetFilePath(fileFormat), contents);
        }

        // Load the game record using the selected file format. ( [fileFormat] 1:txt / 2:json )
        public GameRecord LoadGame(int fileFormat)
        {
            string filePath = GetFilePath(fileFormat);
            if (!File.Exists(filePath))
            {
                if (Directory.Exists(SaveDirectory) && Directory.GetFiles(SaveDirectory).Length > 0)
                {
                    throw new FileNotFoundException("The save file was not found for the selected format.");
                }
                else
                {
                    throw new FileNotFoundException("Unable to load the game because the save file was not found.");
                }
            }

            GameRecord? loadedData = new GameRecord();
            if (fileFormat == 1)
            {
                LoadTxt(filePath, loadedData);
            }
            else
            {
                string? jsonStringRead = File.ReadAllText(filePath);
                loadedData = JsonSerializer.Deserialize<GameRecord>(jsonStringRead);
            }
            return loadedData!;
        }

        public StringBuilder SaveTxt(GameRecord game, StringBuilder savedData)
        {
            savedData.AppendLine($"GameType:{game.GameType}");
            savedData.AppendLine($"GameMode:{game.GameMode}");
            //Generate gameParameters
            foreach (var parameter in game.GameParameters)
            {
                savedData.AppendLine(
                    $"GameParameter[{parameter.Key}]:{parameter.Value}");
            }

            //Generate board row data. 
            int rows = game.CurrentBoard.GetRowCount();
            int columns = game.CurrentBoard.GetColumnCount();
            for (int row = 0; row < rows; row++)
            {
                savedData.Append($"GridMatrix[{row}]:[");
                for (int col = 0; col < columns; col++)
                {
                    savedData.Append($"{game.CurrentBoard.GetCell(col, row)},");
                }
                savedData.AppendLine($"]");
            }
            //Generate move records (oldest -> newest)
            foreach (PlayerMove move in game.MovesLog.Reverse())
            {
                savedData.AppendLine(
                    $"Turn:{move.Turn};" +
                    $"PlayerId:{move.Player.PlayerId};" +
                    $"Piece:{move.Player.GamePiece};" +
                    $"Move:{move.Move}");
            }

            return savedData;
        }

        public GameRecord LoadTxt(string filePath, GameRecord loadedData)
        {

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string? gameTypeValue = GetLineValue(line, "GameType:");
                if (gameTypeValue != null)
                {
                    loadedData.GameType = Enum.Parse<GameType>(gameTypeValue);
                    continue;
                }
                string? gameModeValue = GetLineValue(line, "GameMode:");
                if (gameModeValue != null)
                {
                    loadedData.GameMode = Enum.Parse<GameMode>(gameModeValue);
                    continue;
                }

                if (line.StartsWith("GameParameter"))
                {
                    string key = line.Split('[')[1].Split(']')[0];
                    string value = line.Split(':')[1];
                    loadedData.GameParameters[key] = int.Parse(value);
                    if (loadedData.GameParameters.ContainsKey("rows") &&
                        loadedData.GameParameters.ContainsKey("cols"))
                    {
                        loadedData.CurrentBoard = new Board(
                            (int)loadedData.GameParameters["rows"],
                            (int)loadedData.GameParameters["cols"]);
                    }
                    continue;
                }
                // Rebuild board from the saved file.
                if (line.StartsWith("GridMatrix["))
                {
                    int row = int.Parse(line.Split('[')[1].Split(']')[0]);
                    string rowData = line.Split(':')[1].Trim().Trim('[', ']');
                    string[] cells = rowData.Split(',');

                    for (int col = 0; col < cells.Length; col++)
                    {
                        string piece = cells[col].Trim();
                        if (piece.Length == 0)
                        {
                            piece = " "; // Place space into empty cell.
                        }
                        loadedData.CurrentBoard.AddElement(col, row, piece);
                    }
                    continue;
                }
                // Rebuild each move from the saved file.
                if (line.StartsWith("Turn:"))
                {
                    string[] elements = line.Split(';');
                    int turn = int.Parse(elements[0].Split(':')[1]);
                    int playerId = int.Parse(elements[1].Split(':')[1]);
                    string piece = elements[2].Split(':')[1];
                    string move = elements[3].Split(':')[1];
                    Player player = new Player(playerId, piece);
                    PlayerMove playerMove = new PlayerMove(move, player, turn);
                    loadedData.MovesLog.Push(playerMove);
                }
            }
            return loadedData;
        }

        private string? GetLineValue(string line, string keyword)
        {
            if (line.StartsWith(keyword))
            {
                return line.Split(':')[1].Trim();
            }
            return null;
        }
    }
}
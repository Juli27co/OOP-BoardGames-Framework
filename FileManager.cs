namespace OOP_BoardGames_Framework
{
    using System.Text.Json;
    public class FileManager
    {
        public string SaveDirectory = "savedFile/";
        public string SaveFileName = "savedGameFile";
        public void SaveGame(GameRecord game) // Save Game: it saves game record into json file and txt file.
        {
            Object savedData = new
            {
                GameType = game.GameType,
                CurrentBoard = game.CurrentBoard,
                GameMode = game.GameMode,
                MovesLog = game.MovesLog
            };
            string jsonString = JsonSerializer.Serialize(savedData);
            Directory.CreateDirectory(SaveDirectory);
            File.WriteAllText(SaveDirectory + SaveFileName + ".json", jsonString);
            File.WriteAllText(SaveDirectory + SaveFileName + ".txt", jsonString);
        }
        public GameRecord LoadGame() // Load Game: retrieve saved game data.
        {
            string filePath = SaveDirectory + SaveFileName + ".json";
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Unable to load the game because the save file was not found.");
            }
            string? jsonStringRead = File.ReadAllText(filePath);
            GameRecord? loadedData = JsonSerializer.Deserialize<GameRecord>(jsonStringRead);
            return loadedData!;
        }
    }
}
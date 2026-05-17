namespace OOP_BoardGames_Framework

{
    using System.Text.Json;
    public class FileManager
    {

        private string saveDirectory = "savedFile/";
        private string saveFileName = "savedGameFile";

        // Save Game: it saves game record into json file and txt file.
        public void SaveGame(GameRecord game)
        {
            Object savedData = new
            {
                GameType = game.GameType,
                CurrentBoard = game.CurrentBoard,
                GameMode = game.GameMode,
                MovesLog = game.movesLog
            };

            string jsonString = JsonSerializer.Serialize(savedData);
            Directory.CreateDirectory(saveDirectory);
            File.WriteAllText(saveDirectory + saveFileName + ".json", jsonString);
            File.WriteAllText(saveDirectory + saveFileName + ".txt", jsonString);
            Console.WriteLine("The game is saved successfully.");

        }

        // Load Game: retrieve saved game data.
        public GameRecord LoadGame()
        {
            string filePath = saveDirectory + ".json";
            string? jsonStringRead = File.ReadAllText(filePath);
            GameRecord? loadedData = JsonSerializer.Deserialize<GameRecord>(jsonStringRead);
            if (loadedData == null) { throw new Exception("Saved data was not found."); }
            return loadedData;

        }

    }
}
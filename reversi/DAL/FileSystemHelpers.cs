namespace DAL;

public static class FileSystemHelpers
{
    public static string GetRootPath()
    {
        var path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            ".icd0008",
            "reversi-othello");
        
        return path;
    }

    public static string GetConfigsPath() => Path.Combine(GetRootPath(), "configs");
    public static string GetGamesPath() => Path.Combine(GetRootPath(), "games");
    
    public static string GetConfigFileName(Guid id)
    {
        return Path.Combine(GetConfigsPath(), $"config-{id}.json");
    }
    
    public static string GetGameFileName(Guid id)
    {
        return Path.Combine(GetGamesPath(), $"game-{id}.json");
    }
    
    public static void WriteAtomic (string path, string content)
    {
        var temp = path + ".tmp";
        File.WriteAllText(temp, content);
        File.Move(temp, path);
        
        
    }
}
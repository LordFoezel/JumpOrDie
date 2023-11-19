using System.Collections.Generic;

public static class MapperLevel
{
    static Dictionary<int, string> levels = new Dictionary<int, string>();

    static MapperLevel()
    {
        levels.Add(0, "MainMenu");
        levels.Add(1, "Level01");
        levels.Add(2, "Level02");
    }

    public static string GetLevelName(int id)
    {
        if (levels.TryGetValue(id, out string levelName))
        {
            return levelName;
        }
        else
        {
            return "MainMenu";
        }
    }
}

using System;
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
        foreach (KeyValuePair<int, string> levelPair in levels)
        {
            if (levelPair.Key == id) return levelPair.Value;
        }
        return "MainMenu";
    }

    public static int GetLevelId(string name)
    {
        foreach (KeyValuePair<int, string> levelPair in levels)
        {
            if (levelPair.Value == name) return levelPair.Key;
        }
        return 0;
    }
}

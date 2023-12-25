using UnityEngine;
using System;
using System.Collections.Generic;

public static class MapperLevel
{
    static Dictionary<int, string> levelsbase = new Dictionary<int, string>();

    static MapperLevel()
    {
        levelsbase.Add(0, "MainMenu");
        levelsbase.Add(1, "Level01");
        levelsbase.Add(2, "Level02");
        levelsbase.Add(3, "Level03");
        levelsbase.Add(4, "Level04");
    }

    public static string GetLevelName(int id)
    {
        foreach (KeyValuePair<int, string> levelPair in levelsbase)
        {
            if (levelPair.Key == id) return levelPair.Value;
        }
        return "MainMenu";
    }

    public static int GetLevelId(string name)
    {
        foreach (KeyValuePair<int, string> levelPair in levelsbase)
        {
            UtilDebug.Log(levelPair.Value + " " + name + " " + levelPair.Key);
            if (levelPair.Value == name) return levelPair.Key;
        }
        return 0;
    }
}

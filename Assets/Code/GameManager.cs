using System.Collections.Generic;

public static class GameManager
{
    private static UtilEnum.GameState actualGameState;
    private static int actualGameLevel = 0;
    private static Dictionary<int, TrapBase> traps = new Dictionary<int, TrapBase>();


    public static UtilEnum.GameState ActualGameState { get => actualGameState; set => actualGameState = value; }
    public static int ActualGameLevel { get => actualGameLevel; set => actualGameLevel = value; }
    public static Dictionary<int, TrapBase> Traps { get => traps; set => traps = value; }
}

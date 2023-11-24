using System.Collections.Generic;

public static class GameManager
{
    private static UtilEnum.GameState actualGameState;
    private static int actualGameLevel = 0;
    private static PlayerBase player;
    private static Dictionary<int, TrapBase> traps = new Dictionary<int, TrapBase>();
    private static Dictionary<int, PotionBase> potions = new Dictionary<int, PotionBase>();
    private static Dictionary<int, CoinBase> coins = new Dictionary<int, CoinBase>();


    public static UtilEnum.GameState ActualGameState { get => actualGameState; set => actualGameState = value; }
    public static int ActualGameLevel { get => actualGameLevel; set => actualGameLevel = value; }
    public static PlayerBase Player { get => player; set => player = value; }
    public static Dictionary<int, TrapBase> Traps { get => traps; set => traps = value; }
    public static Dictionary<int, PotionBase> Potions { get => potions; set => potions = value; }
    public static Dictionary<int, CoinBase> Coins { get => coins; set => coins = value; }

    public static void ClearAll()
    {
        Player = null;
        Traps.Clear();
        Coins.Clear();
        Potions.Clear();
    }
}
using System.Collections.Generic;

// Test
public static class GameManager
{
    public static UtilEnum.GameState ActualGameState { get; set; }
    public static int ActualGameLevel { get; set; } = 0;
    public static PlayerBase Player { get; set; } = null;
    public static Dictionary<int, TrapBase> Traps { get; set; } = new Dictionary<int, TrapBase>();
    public static Dictionary<int, PotionBase> Potions { get; set; } = new Dictionary<int, PotionBase>();
    public static Dictionary<int, CoinBase> Coins { get; set; } = new Dictionary<int, CoinBase>();

    public static void ClearAll()
    {
        if (Player != null)
        {
            Player.Remove();
            Player = null;
        }
        foreach (KeyValuePair<int, TrapBase> trap in Traps)
        {
            trap.Value.Remove();
        }
        Traps.Clear();
        foreach (KeyValuePair<int, CoinBase> coin in Coins)
        {
            coin.Value.Remove();
        }
        Coins.Clear();
        foreach (KeyValuePair<int, PotionBase> potion in Potions)
        {
            potion.Value.Remove();
        }
        Potions.Clear();
    }
}
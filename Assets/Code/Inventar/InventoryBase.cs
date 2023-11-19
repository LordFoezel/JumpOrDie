public class InventoryBase
{
    int coins;
    public bool canCollectCoins;

    public InventoryBase(int coins, bool canCollectCoins)
    {
        SetCoins(coins);
        this.canCollectCoins = canCollectCoins;
    }

    public void SetCoins(int count = 0)
    {
        coins = count;
    }

    public void AddCoins(int count = 1)
    {
        coins += count;
    }

    public int GetCoins()
    {
        return coins;
    }

}

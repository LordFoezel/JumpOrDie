public class InventoryBase
{
    int coins;
    public bool canCollectCoins;

    public InventoryBase()
    {
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

public class InventoryBase
{
    int coins;
    public bool canCollectCoins;
    PlayerBase self;

    public InventoryBase(PlayerBase self)
    {
        this.self = self;
    }

    public void SetCoins(int count = 0)
    {
        coins = count;
        self.RefreshDisplayCoins();
    }

    public void AddCoins(int count = 1)
    {
        coins += count;
        self.RefreshDisplayCoins();
    }

    public int GetCoins()
    {
        return coins;
    }

}

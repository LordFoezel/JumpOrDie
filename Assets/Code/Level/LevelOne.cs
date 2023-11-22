public class LevelOne : LevelBaseLoader
{
    public override void InitLevelData()
    {
        GameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level01.ToString());
    }

    public override void InitLevel()
    {

        PlayerManager = new PlayerManager();
        TrapManager = new TrapManager();
        PotionManager = new PotionManager();
        CoinManager = new CoinManager();
        TrapManager.SetTickEvent(this);
        PotionManager.SetTickEvent(this);
        CoinManager.SetTickEvent(this);
        base.InitLevel();
    }
}

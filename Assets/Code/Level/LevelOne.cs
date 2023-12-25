public class LevelOne : LevelsBase
{
    public override void InitLevel()
    { 
        GameManager.ActualGameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level01.ToString());
        PlayerManager = new PlayerManager();
        TrapManager = new TrapManager();
        PotionManager = new PotionManager();
        CoinManager = new CoinManager();
        PlayerManager.AddPlayer();
        CoinManager.LoadCoins();
        PotionManager.LoadPotions();
        TrapManager.LoadTraps();
        base.InitLevel();
        SaveLevel();
    }
}

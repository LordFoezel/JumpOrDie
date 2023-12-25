public class LevelFour : LevelsBase
{
    public override void InitLevel()
    {
        GameManager.ActualGameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level04.ToString());
        PlayerManager = new PlayerManager();
        TrapManager = new TrapManager();
        PotionManager = new PotionManager();
        CoinManager = new CoinManager();
        PotionManager.LoadPotions();
        PlayerManager.AddPlayer();
        CoinManager.LoadCoins();
        TrapManager.LoadTraps();
        base.InitLevel();
        SaveLevel();
    }
}

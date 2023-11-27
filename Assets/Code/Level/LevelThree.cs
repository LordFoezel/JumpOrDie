public class LevelThree : LevelsBase
{
    public override void InitLevel()
    {
        GameManager.ActualGameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level03.ToString());
        PlayerManager = new PlayerManager();
        TrapManager = new TrapManager();
        PotionManager = new PotionManager();
        CoinManager = new CoinManager();
        PlayerManager.AddPlayer();
        CoinManager.LoadCoins();
        base.InitLevel();
        SaveLevel();
    }
}

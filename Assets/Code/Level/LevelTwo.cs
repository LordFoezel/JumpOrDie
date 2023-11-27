public class LevelTwo : LevelsBase
{
    public override void InitLevel()
    {
        GameManager.ActualGameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level02.ToString());
        PlayerManager = new PlayerManager();
        TrapManager = new TrapManager();
        PotionManager = new PotionManager();
        CoinManager = new CoinManager();
        PlayerManager.AddPlayer();
        base.InitLevel();

        SaveLevel();
    }
}

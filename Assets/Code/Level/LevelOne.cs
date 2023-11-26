using UnityEngine;

public class LevelOne : LevelsBase
{
    public override void InitLevel()
    {
        GameManager.ActualGameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level01.ToString());
        PlayerManager = new PlayerManager();
        TrapManager = new TrapManager();
        PotionManager = new PotionManager();
        CoinManager = new CoinManager();
        PotionManager.SetTickEvent(this);
        CoinManager.SetTickEvent(this);
        PlayerManager.AddPlayer();
        CoinManager.LoadCoins();
        base.InitLevel();

        SaveLevel();
    }
}

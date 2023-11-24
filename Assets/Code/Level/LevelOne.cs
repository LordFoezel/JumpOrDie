using UnityEngine;

public class LevelOne : LevelsBase
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
        PotionManager.SetTickEvent(this);
        CoinManager.SetTickEvent(this);
        PlayerManager.AddPlayer();
        base.InitLevel();

        SaveLevel();
    }
}

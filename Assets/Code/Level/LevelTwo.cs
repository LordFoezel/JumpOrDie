public class LevelTwo : LevelsBase
{
    public override void InitLevelData()
    {
        GameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level02.ToString());
    }
}

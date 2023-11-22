public class LevelTwo : LevelBaseLoader
{
    public override void InitLevelData()
    {
        GameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level02.ToString());
    }
}

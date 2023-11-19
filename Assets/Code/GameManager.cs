public static class GameManager
{
    private static UtilEnum.GameState actualGameState;

    private static int actualGameLevel = 0;

    public static UtilEnum.GameState GetActualGameState()
    {
        return actualGameState;
    }

    public static void SetActualGameState(UtilEnum.GameState newGameState)
    {
        actualGameState = newGameState;
    }

     public static int GetActualGameLevel()
    {
        return actualGameLevel;
    }

    public static void SetActualGameLevel(int newGameLevel)
    {
        actualGameLevel = newGameLevel;
    }
}

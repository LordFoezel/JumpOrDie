public static class GameManager
{
    public enum GameState { Menu, Running, Pause, Loading };
    private static GameState actualGameState;

    private static int actualGameLevel = 0;

    public static GameState GetActualGameState()
    {
        return actualGameState;
    }

    public static void SetActualGameState(GameState newGameState)
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

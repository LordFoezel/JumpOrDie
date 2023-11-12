using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        GameManager.SetActualGameState(GameManager.GameState.Running);
    }

    public void Save()
    {
        UtilSaveManager.SaveCurrentLevel();
    }

    public void Load()
    {
        // UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        Debug.Log("Hm... kommt noch");
    }

    public void Exit()
    {
        UtilLevelLoader levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        levelLoader.LoadLevel("MainMenu");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button loadGameButton;

    void Awake()
    {
        // TODO: Wiso findet er das nicht????
        // loadGameButton = gameObject.transform.Find("LevelMenuPanel").gameObject.GetComponent<Button>();
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        if (savedData.isIngame == 0) loadGameButton.interactable = false;
    }

    public void Continue()
    {
        GameManager.SetActualGameState(UtilEnum.GameState.Running);
    }

    public void Save()
    {
        LevelBaseLoader levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelBaseLoader>();
        PlayerBase player = levelLoader.playerManager.players[1];
        int coins = player.GetInventory().GetCoins();
        int health = player.GetHealth();
        UtilSaveManager.SaveCurrentGame(health, coins);
        loadGameButton.interactable = true;
    }

    public void Load()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
    }

    public void Exit()
    {
        UtilLevelLoader levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        levelLoader.LoadLevel("MainMenu");
    }
}

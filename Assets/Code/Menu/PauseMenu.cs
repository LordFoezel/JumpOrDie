using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button loadGameButton;
    UtilLevelLoader levelLoader;

    void Awake()
    {
        levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        // TODO: Wiso findet er das nicht????
        // loadGameButton = gameObject.transform.Find("LevelMenuPanel").gameObject.GetComponent<Button>();
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        if (savedData.isIngame == 0) loadGameButton.interactable = false;
        else loadGameButton.interactable = true;
    }

    public void Continue()
    {
        GameManager.ActualGameState = UtilEnum.GameState.Running;
    }

    public void Save()
    {
        LevelBaseLoader levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelBaseLoader>();
        PlayerBase player = levelLoader.playerManager.player;
        int coins = player.GetInventory().GetCoins();
        int health = player.GetHitPoints();
        Vector2 position = player.GetPosition();
        Debug.Log("Save: "+health+" Coins: "+coins+ " Position: " + position.x+"/"+position.y);
        UtilSaveManager.SaveCurrentGame(health, coins, position);
        loadGameButton.interactable = true;
    }

    public void Load()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        levelLoader.LoadLevel(MapperLevel.GetLevelName(savedData.actualLevel));
    }

    public void Exit()
    {
        levelLoader.LoadLevel("MainMenu");
    }
}

using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Button loadGameButton;
    private Text responseText;
    UtilLevelLoader levelLoader;
    float lastTextDisplayd = 0f;

    void Awake()
    {
        levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        loadGameButton = gameObject.transform.Find("LevelMenuPanel").gameObject.transform.Find("Panel").gameObject.transform.Find("Load").gameObject.GetComponent<Button>();
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        if (savedData.isIngame == 0) loadGameButton.interactable = false;
        else loadGameButton.interactable = true;
        responseText = gameObject.transform.Find("LevelMenuPanel").gameObject.transform.Find("Panel").gameObject.transform.Find("Response").GetComponent<Text>();
    }

    void Update()
    {
        if (lastTextDisplayd + 2f < Time.time) ClearResponseText();
    }

    public void SetResponseText(string text)
    {
        responseText.text = text;
        lastTextDisplayd = Time.time;
    }

    private void ClearResponseText()
    {
        responseText.text = "";
    }

    public void Continue()
    {
        GameManager.SetActualGameState(UtilEnum.GameState.Running);
        Cursor.visible = false;
    }

    public void Save()
    {
        PlayerBase player = GameManager.Player;
        int coins = player.Inventory.GetCoins();
        int health = player.HitPoints;
        Vector2 position = player.GetPosition();
        UtilSaveManager.SaveCurrentGame(health, coins, position);
        loadGameButton.interactable = true;
        SetResponseText("Game saved!");
    }

    public void Load()
    {
        GameManager.ClearAll();
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        levelLoader.LoadLevel(MapperLevel.GetLevelName(savedData.actualLevel));
    }

    public void Exit()
    {
        GameManager.ClearAll();
        levelLoader.LoadLevel(UtilEnum.GameLevel.MainMenu.ToString());
    }
}

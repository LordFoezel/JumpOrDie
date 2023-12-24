using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    UtilLevelLoader levelLoader;
    GameObject mainMenuPanel;
    GameObject levelMenuPanel;
    Button loadGameButton;
    int maxLevel = 1;
    public Dictionary<int, Button> buttons;

    void Awake()
    {
        levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        mainMenuPanel = GameObject.Find("MainMenuPanel").gameObject;
        levelMenuPanel = GameObject.Find("LevelMenuPanel").gameObject;
        UtilSaveManager.LevelData levelsBaseaves = UtilSaveManager.LoadLevelData();
        maxLevel = levelsBaseaves.maxLevel;
        InitButtons();
        mainMenuPanel.SetActive(true);
        levelMenuPanel.SetActive(false);
        loadGameButton = mainMenuPanel.transform.Find("LoadGameButton").gameObject.GetComponent<Button>();
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        if (savedData.isIngame == 0) loadGameButton.interactable = false;
        else loadGameButton.interactable = true;
    }

    private void InitButtons()
    {
        buttons = new Dictionary<int, Button>();
        Transform panel = levelMenuPanel.transform.Find("Panel").transform;
        buttons.Add(1, panel.Find(UtilEnum.GameLevel.Level01.ToString()).gameObject.GetComponent<Button>());
        buttons.Add(2, panel.Find(UtilEnum.GameLevel.Level02.ToString()).gameObject.GetComponent<Button>());
        buttons.Add(3, panel.Find(UtilEnum.GameLevel.Level03.ToString()).gameObject.GetComponent<Button>());
        buttons.Add(4, panel.Find(UtilEnum.GameLevel.Level04.ToString()).gameObject.GetComponent<Button>());
        foreach (KeyValuePair<int, Button> button in buttons)
        {
            if (button.Key <= maxLevel) button.Value.interactable = true;
            else button.Value.interactable = false;
        }
    }

    public void NewGame()
    {
        UtilSaveManager.LevelData saveData = new()
        {
            maxLevel = 1,
            actualLevel = 0,
            health = 0,
            totalCoins = 0,
            levelCoins = 0,
            isIngame = 0,
        };
        UtilSaveManager.SaveLevelData(saveData);
        levelLoader.LoadLevel(UtilEnum.GameLevel.Level01.ToString());
    }

    public void LoadGame()
    {
        GameManager.ClearAll();
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        levelLoader.LoadLevel(MapperLevel.GetLevelName(savedData.actualLevel));
    }

    public void Exit()
    {
        GameManager.ClearAll();
        Application.Quit();
    }

    public void ActivateLevelChosePanel()
    {
        UtilSaveManager.LevelData levelsBaseaves = UtilSaveManager.LoadLevelData();
        maxLevel = levelsBaseaves.maxLevel;
        levelMenuPanel.SetActive(true);
    }

    public void DeactivateLevelChosePanel()
    {
        levelMenuPanel.SetActive(false);
    }

    public void StartLevel1()
    {
        levelLoader.LoadLevel("Level01");
    }

    public void StartLevel2()
    {
        levelLoader.LoadLevel("Level02");
    }

     public void StartLevel3()
    {
        levelLoader.LoadLevel("Level03");
    }

     public void StartLevel4()
    {
        levelLoader.LoadLevel("Level04");
    }
}

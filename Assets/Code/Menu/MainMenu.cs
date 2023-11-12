using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    UtilLevelLoader levelLoader;
    GameObject mainMenuPanel;
    GameObject levelMenuPanel;
    int maxLevel = 1;
    public Dictionary<int, Button> buttons;

    void Awake()
    {
        GameManager.SetActualGameLevel(0);
        levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        mainMenuPanel = GameObject.Find("MainMenuPanel").gameObject;
        levelMenuPanel = GameObject.Find("LevelMenuPanel").gameObject;
        UtilSaveManager.LevelData levelSaves = UtilSaveManager.LoadLevelData();
        maxLevel = levelSaves.maxLevel;
        InitButtons();
        mainMenuPanel.SetActive(true);
        levelMenuPanel.SetActive(false);
    }

    private void InitButtons()
    {
        buttons = new Dictionary<int, Button>();
        Transform panel = levelMenuPanel.transform.Find("Panel").transform;
        buttons.Add(1, panel.Find("Level01").gameObject.GetComponent<Button>());
        buttons.Add(2, panel.Find("Level02").gameObject.GetComponent<Button>());
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
            actualLevel = 1,
        };
        UtilSaveManager.SaveLevelData(saveData);
        levelLoader.LoadLevel("Level01");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ActivateLevelChosePanel()
    {
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
}

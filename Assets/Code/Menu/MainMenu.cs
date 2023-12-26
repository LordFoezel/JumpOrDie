using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    UtilLevelLoader levelLoader;
    GameObject mainMenuPanel;
    GameObject levelMenuPanel;
    GameObject congratulationPanel;
    Button loadGameButton;
    int maxLevel = 1;
    public Dictionary<int, Button> buttons;

    void Awake()
    {
        levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        mainMenuPanel = GameObject.Find("MainMenuPanel").gameObject;
        levelMenuPanel = GameObject.Find("LevelMenuPanel").gameObject;
        congratulationPanel = GameObject.Find("CongratulationPanel").gameObject;
        SaveGameData saveData = UtilSaveManager.LoadSaveData();
        maxLevel = saveData.maxLevel;
        InitButtons();
        RefreshButtons();
        mainMenuPanel.SetActive(true);
        levelMenuPanel.SetActive(false);
        if(GameManager.GameIsEnded) congratulationPanel.SetActive(true);
        else congratulationPanel.SetActive(false);
        loadGameButton = mainMenuPanel.transform.Find("LoadGameButton").gameObject.GetComponent<Button>();
        if (saveData.isIngame == 0) loadGameButton.interactable = false;
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
    }

    public void NewGame()
    {
        UtilSaveManager.ClearLevelSave();
        UtilSaveManager.ClearMaxLevel();
        UtilSaveManager.SaveIsIngame(0);
        levelLoader.LoadLevel(UtilEnum.GameLevel.Level01.ToString());
    }

    public void LoadGame()
    {
        GameManager.ClearAll();
        SaveGameData savedData = UtilSaveManager.LoadSaveData();
        levelLoader.LoadLevel(MapperLevel.GetLevelName(savedData.actualLevel));
    }

    public void Exit()
    {
        GameManager.ClearAll();
        Application.Quit();
    }

      public void DeactivateCongratulationPanel()
    {       
        congratulationPanel.SetActive(false);
    }

    public void ActivateLevelChosePanel()
    {
       RefreshButtons();
        levelMenuPanel.SetActive(true);
    }

    private void RefreshButtons(){
        SaveGameData gameData = UtilSaveManager.LoadSaveData();
        maxLevel = gameData.maxLevel;
        foreach (KeyValuePair<int, Button> button in buttons)
        {
            if (button.Key <= maxLevel) button.Value.interactable = true;
            else button.Value.interactable = false;
        }
    }

    public void DeactivateLevelChosePanel()
    {
        levelMenuPanel.SetActive(false);
    }

    public void StartLevel1()
    {   
        UtilSaveManager.ClearLevelSave();
        UtilSaveManager.SaveIsIngame(0);
        levelLoader.LoadLevel("Level01");
    }

    public void StartLevel2()
    {
        UtilSaveManager.ClearLevelSave();
        UtilSaveManager.SaveIsIngame(0);
        levelLoader.LoadLevel("Level02");
    }

     public void StartLevel3()
    {        
        UtilSaveManager.ClearLevelSave();
        UtilSaveManager.SaveIsIngame(0);
        levelLoader.LoadLevel("Level03");
    }

     public void StartLevel4()
    {        
        UtilSaveManager.ClearLevelSave();
        UtilSaveManager.SaveIsIngame(0);
        levelLoader.LoadLevel("Level04");
    }

    public void AvtivateCongratulationPanel() {
        congratulationPanel.SetActive(true);
    }
}

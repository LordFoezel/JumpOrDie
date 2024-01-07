using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    UtilLevelLoader levelLoader;
    GameObject mainMenuPanel;
    GameObject levelMenuPanel;
    GameObject congratulationPanel;
    GameObject optionsPanel;
    Button loadGameButton;
    Slider difficultSlider;
    int maxLevel = 1;
    public Dictionary<int, Button> buttons;
    public Dictionary<string, Text> keys;

    void Awake()
    {
        levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        mainMenuPanel = GameObject.Find("MainMenuPanel").gameObject;
        levelMenuPanel = GameObject.Find("LevelMenuPanel").gameObject;
        congratulationPanel = GameObject.Find("CongratulationPanel").gameObject;
        optionsPanel = GameObject.Find("OptionsPanel").gameObject;
        SaveGameData saveData = UtilSaveManager.LoadData();
        maxLevel = saveData.maxLevel;
        InitButtons();
        RefreshButtons();
        mainMenuPanel.SetActive(true);
        levelMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        if(GameManager.GameIsEnded) congratulationPanel.SetActive(true);
        else congratulationPanel.SetActive(false);
        loadGameButton = mainMenuPanel.transform.Find("LoadGameButton").gameObject.GetComponent<Button>();
        if (saveData.isIngame == 0) loadGameButton.interactable = false;
        else loadGameButton.interactable = true;
        InitOptions();
        RefreshOptions();
    }

    private void InitOptions(){
        GameObject keyObject = optionsPanel.transform.Find("Panel").gameObject.transform.Find("Keys").gameObject;
        keys = new Dictionary<string, Text>();
        keys.Add("left", keyObject.transform.Find("KeyLeft").gameObject.GetComponent<Text>());
        keys.Add("right", keyObject.transform.Find("KeyRight").gameObject.GetComponent<Text>());
        keys.Add("jump", keyObject.transform.Find("KeyJump").gameObject.GetComponent<Text>());
        keys.Add("interact", keyObject.transform.Find("KeyInteract").gameObject.GetComponent<Text>());
        keys.Add("pause", keyObject.transform.Find("KeyPause").gameObject.GetComponent<Text>());
        difficultSlider = optionsPanel.transform.Find("Panel").gameObject.transform.Find("Panel").gameObject.transform.Find("Slider").gameObject.GetComponent<Slider>();
        PersistGameData persistData = UtilSaveManager.LoadPersistData();
        if(persistData.difficultLevel == 0) persistData = UtilSaveManager.clearPersistData();
    }

    private void RefreshOptions(){
        PersistGameData persistData = UtilSaveManager.LoadPersistData();
        difficultSlider.value = persistData.difficultLevel;
        foreach (KeyValuePair<string, Text> keyItem in keys)
        {
            if(keyItem.Key == "left") keyItem.Value.text = persistData.left;
            if(keyItem.Key == "right") keyItem.Value.text = persistData.right;
            if(keyItem.Key == "jump") keyItem.Value.text = persistData.jump;
            if(keyItem.Key == "interact") keyItem.Value.text = persistData.interact;
            if(keyItem.Key == "pause") keyItem.Value.text = persistData.pause;
        }
        GameManager.Difficult = persistData.difficultLevel;
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

    public void SetDefault()
    {
        UtilSaveManager.clearPersistData();  
        RefreshOptions();
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
        SaveGameData savedData = UtilSaveManager.LoadData();
        levelLoader.LoadLevel(MapperLevel.GetLevelName(savedData.actualLevel));
    }

    public void Exit()
    {
        GameManager.ClearAll();
        Application.Quit();
    }

    private void RefreshButtons(){
        SaveGameData gameData = UtilSaveManager.LoadData();
        maxLevel = gameData.maxLevel;
        foreach (KeyValuePair<int, Button> button in buttons)
        {
            if (button.Key <= maxLevel) button.Value.interactable = true;
            else button.Value.interactable = false;
        }
    }

    public void DifficultSlider(){
        if(!difficultSlider) return; 
        UtilSaveManager.SavePersitDataDifficult((int)difficultSlider.value);
        GameManager.Difficult = (int)difficultSlider.value;
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

    
    public void DeactivateLevelChosePanel()
    {
        levelMenuPanel.SetActive(false);
    }

    public void DeactivateOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

     public void ActivateOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    public void DeactivateCongratulationPanel()
    {       
        GameManager.GameIsEnded = false;
        congratulationPanel.SetActive(false);
    }

    public void ActivateLevelChosePanel()
    {
        RefreshButtons();
        levelMenuPanel.SetActive(true);
    }
}

using UnityEngine;
using System;

public class LevelsBase : LevelLoaderBase
{
    private GameObject pauseObject;
    public event Action OnUpdateEvent;
    private PauseMenu pauseMenu;
    public PlayerManager PlayerManager { get; set; }
    public TrapManager TrapManager { get; set; }
    public PotionManager PotionManager { get; set; }
    public CoinManager CoinManager { get; set; }



    public override void InitLevel()
    {
        base.InitLevel();
        LoadPauseObject();
        GameManager.SetActualGameState(UtilEnum.GameState.Running);
        Cursor.visible = false;
    }

    public override void Tick()
    {
        SetTickEvent();
        if (GameManager.ActualGameState == UtilEnum.GameState.Running)
        {
            if (IsPaused)
            {
                pauseObject.SetActive(false);
                IsPaused = false;
            }
        }
        else if (GameManager.ActualGameState == UtilEnum.GameState.Pause)
        {
            if (!IsPaused)
            {
                pauseMenu.UpdateButtons();
                pauseObject.SetActive(true);
                IsPaused = true;
            }
        }
    }

    public void LoadPlayerData()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        GameManager.Player.SetPlayerData(savedData.health, savedData.levelCoins, new Vector2(savedData.positionX, savedData.positionY));

    }

    private void SetTickEvent()
    {
        OnUpdateEvent.Invoke();
    }

    public void SaveLevel()
    {
        UtilSaveManager.SaveLevelStart();
    }

    public void PauseGame()
    {
        GameManager.SetActualGameState(UtilEnum.GameState.Pause);
        Cursor.visible = true;
    }

    private void LoadPauseObject()
    {
        GameManager.SetActualGameState(UtilEnum.GameState.Menu);
        GameObject pausePrefab = Resources.Load<GameObject>("Prefaps/Menu/Pause");
        pauseObject = Instantiate(pausePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        pauseObject.SetActive(false);
        pauseMenu = pauseObject.GetComponent<PauseMenu>();
        Camera camera = GameObject.Find("Camera").gameObject.GetComponent<Camera>();
        Canvas canvas = pauseObject.GetComponent<Canvas>();
        canvas.worldCamera = camera;
    }
}

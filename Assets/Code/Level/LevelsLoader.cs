using UnityEngine;
using System;

public class LevelsBase : LevelLoaderBase
{
    private PlayerManager playerManager;
    private TrapManager trapManager;
    private PotionManager potionManager;
    private CoinManager coinManager;
    private GameObject pauseObject;
    public event Action OnUpdateEvent;

    public PlayerManager PlayerManager { get => playerManager; set => playerManager = value; }
    public TrapManager TrapManager { get => trapManager; set => trapManager = value; }
    public PotionManager PotionManager { get => potionManager; set => potionManager = value; }
    public CoinManager CoinManager { get => coinManager; set => coinManager = value; }

    public override void InitLevelData()
    {
        GameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level01.ToString());
    }

    public override void InitLevel()
    {
        base.InitLevel();
        LoadPauseObject();
        GameManager.ActualGameState = UtilEnum.GameState.Running;
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
        UtilSaveManager.SaveCurrentLevel();
    }

    public void PauseGame()
    {
        GameManager.ActualGameState = UtilEnum.GameState.Pause;
    }

    private void LoadPauseObject()
    {
        GameManager.ActualGameState = UtilEnum.GameState.Menu;
        GameObject pausePrefab = Resources.Load<GameObject>("Prefaps/Menu/Pause");
        pauseObject = Instantiate(pausePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        pauseObject.SetActive(false);
        Camera camera = GameObject.Find("Camera").gameObject.GetComponent<Camera>();
        Canvas canvas = pauseObject.GetComponent<Canvas>();
        canvas.worldCamera = camera;
    }
}

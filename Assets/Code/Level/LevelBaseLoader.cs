using UnityEngine;
using System;

public class LevelBaseLoader : MonoBehaviour
{
    public PlayerManager playerManager;
    public TrapManager trapManager;
    public PotionManager potionManager;
    public CoinManager coinManager;
    private bool isPaused = false;
    private GameObject pauseObject;
    public int gameLevel;
    public event Action OnUpdateEvent;

    void Awake()
    {
        InitLevelData();
        InitLevel();
    }

    public virtual void InitLevelData()
    {
        gameLevel = 0;
    }

    public virtual void InitLevel()
    {
        GameManager.ActualGameLevel = gameLevel;
        playerManager = new PlayerManager();
        trapManager = new TrapManager();
        potionManager = new PotionManager();
        coinManager = new CoinManager();
        playerManager.SetTickEvent(this);
        trapManager.SetTickEvent(this);
        potionManager.SetTickEvent(this);
        coinManager.SetTickEvent(this);
        LoadPauseObject();
        SaveLevel();
        InitPlayerData();
        GameManager.ActualGameState = UtilEnum.GameState.Running;
    }

    void InitPlayerData()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        playerManager.player.SetPlayerData(savedData.health, savedData.levelCoins, new Vector2(savedData.positionX, savedData.positionY));

    }
    private void SaveLevel()
    {
        UtilSaveManager.SaveCurrentLevel();
    }

    void FixedUpdate()
    {
        if (GameManager.ActualGameState == UtilEnum.GameState.Running)
        {
            if (isPaused)
            {
                pauseObject.SetActive(false);
                isPaused = false;
            }
            OnUpdateEvent.Invoke();
        }
        else if (GameManager.ActualGameState == UtilEnum.GameState.Pause)
        {
            if (!isPaused)
            {
                pauseObject.SetActive(true);
                isPaused = true;
            }
        }
    }

    public void PauseGame()
    {
        GameManager.ActualGameState = UtilEnum.GameState.Pause;
    }

    public GameObject PrefabInstantiate(GameObject prefab)
    {
        return Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void GameObjectDestroy(GameObject gameObject, int delay = 0)
    {
        Destroy(gameObject, delay);
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

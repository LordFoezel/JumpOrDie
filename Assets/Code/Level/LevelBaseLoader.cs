using UnityEngine;
using System;

public class LevelBaseLoader : MonoBehaviour
{
    private PlayerManager playerManager;
    private TrapManager trapManager;
    private PotionManager potionManager;
    private CoinManager coinManager;
    private bool isPaused = false;
    private GameObject pauseObject;
    private int gameLevel;
    public event Action OnUpdateEvent;
    
    public int GameLevel { get => gameLevel; set => gameLevel = value; }
    public PlayerManager PlayerManager { get => playerManager; set => playerManager = value; }
    public TrapManager TrapManager { get => trapManager; set => trapManager = value; }
    public PotionManager PotionManager { get => potionManager; set => potionManager = value; }
    public CoinManager CoinManager { get => coinManager; set => coinManager = value; }

    void Awake()
    {
        InitLevel();
    }

    public virtual void InitLevelData()
    {
        GameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.Level01.ToString());
    }

    public virtual void InitLevel()
    {
       
        GameManager.ActualGameLevel = GameLevel;
        LoadPauseObject();
        SaveLevel();
        InitPlayerData();
        GameManager.ActualGameState = UtilEnum.GameState.Running;
    }

    void InitPlayerData()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        GameManager.Player.SetPlayerData(savedData.health, savedData.levelCoins, new Vector2(savedData.positionX, savedData.positionY));

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

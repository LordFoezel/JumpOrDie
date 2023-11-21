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
        GameManager.SetActualGameLevel(gameLevel);
        SaveLevel();
        playerManager = new PlayerManager();
        trapManager = new TrapManager();
        potionManager = new PotionManager();
        coinManager = new CoinManager();
        playerManager.SetTickEvent(this);
        trapManager.SetTickEvent(this);
        potionManager.SetTickEvent(this);
        coinManager.SetTickEvent(this);
        LoadPauseObject();
        GameManager.SetActualGameState(UtilEnum.GameState.Running);
    }

    private void SaveLevel()
    {
        UtilSaveManager.SaveCurrentLevel();
    }

    void FixedUpdate()
    {
        if (GameManager.GetActualGameState() == UtilEnum.GameState.Running)
        {
            if (isPaused)
            {
                pauseObject.SetActive(false);
                isPaused = false;
            }
            OnUpdateEvent.Invoke();
        }
        else if (GameManager.GetActualGameState() == UtilEnum.GameState.Pause)
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
        GameManager.SetActualGameState(UtilEnum.GameState.Pause);
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
        GameManager.SetActualGameState(UtilEnum.GameState.Menu);
        GameObject pausePrefab = Resources.Load<GameObject>("Prefaps/Menu/Pause");
        pauseObject = Instantiate(pausePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        pauseObject.SetActive(false);
        Camera camera = GameObject.Find("Camera").gameObject.GetComponent<Camera>();
        Canvas canvas = pauseObject.GetComponent<Canvas>();
        canvas.worldCamera = camera;
    }
}

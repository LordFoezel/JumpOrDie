using UnityEngine;

public class LevelBaseLoader : MonoBehaviour
{
    public PlayerManager playerManager;
    public TrapManager trapManager;
    public PotionManager potionManager;
    private bool isPaused = false;
    private GameObject pauseObject;
    public int gameLevel;

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
        LoadPauseObject();
        GameManager.SetActualGameState(GameManager.GameState.Running);
    }

    private void SaveLevel()
    {
        UtilSaveManager.SaveCurrentLevel();
    }

    void FixedUpdate()
    {
        if (GameManager.GetActualGameState() == GameManager.GameState.Running)
        {
            if (isPaused)
            {
                pauseObject.SetActive(false);
                isPaused = false;
            }
            playerManager.Tick();
            trapManager.Tick();
            potionManager.Tick();
        }
        else if (GameManager.GetActualGameState() == GameManager.GameState.Pause)
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
        GameManager.SetActualGameState(GameManager.GameState.Pause);
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
        GameManager.SetActualGameState(GameManager.GameState.Menu);
        GameObject pausePrefab = Resources.Load<GameObject>("Prefaps/Menu/Pause");
        pauseObject = Instantiate(pausePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        pauseObject.SetActive(false);
        Camera camera = GameObject.Find("Camera").gameObject.GetComponent<Camera>();
        Canvas canvas = pauseObject.GetComponent<Canvas>();
        canvas.worldCamera = camera;
    }
}

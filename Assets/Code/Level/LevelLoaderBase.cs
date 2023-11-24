using UnityEngine;

public class LevelLoaderBase : MonoBehaviour
{

    private bool isPaused = false;
    private int gameLevel;

    public int GameLevel { get => gameLevel; set => gameLevel = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    public virtual void InitLevelData()
    {
        GameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.MainMenu.ToString());
    }

    public virtual void InitLevel()
    {
        GameManager.ActualGameLevel = GameLevel;
        InitLevelData();
    }

    public virtual void Tick()
    {
    }

    void Awake()
    {
        InitLevel();
    }

    void FixedUpdate()
    {
        Tick();
    }

    public GameObject PrefabInstantiate(GameObject prefab)
    {
        return Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

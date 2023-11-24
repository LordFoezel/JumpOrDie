using UnityEngine;

public class LevelLoaderBase : MonoBehaviour
{

    private bool isPaused = false;
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    public virtual void InitLevel()
    {
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

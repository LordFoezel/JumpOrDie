using UnityEngine;

public class LevelLoaderBase : MonoBehaviour
{
    public bool IsPaused { get ; set; }

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

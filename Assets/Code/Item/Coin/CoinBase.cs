using UnityEngine;

public class CoinBase
{
    public int id;
    public string filename;
    public LevelLoaderBase levelLoader;
    public GameObject coinObject;
    public Transform spawnPoint;
    public bool used = false;

    public CoinBase(int id, Transform spawnPoint)
    {
        this.id = id;
        this.spawnPoint = spawnPoint;
        filename = "Prefaps/Coin/Coin";
        InitCoin();
    }

    void InitCoin()
    {
        LoadCoin();
        LoadColliders();
    }

    void LoadCoin()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoaderBase>();
        GameObject prefab = Resources.Load<GameObject>(filename);
        coinObject = levelLoader.PrefabInstantiate(prefab);
        coinObject.transform.position = spawnPoint.position;
    }

    public void Remove()
    {
        GameObject.Destroy(coinObject, 10);
    }

    private void LoadColliders()
    {
        BoxCollider2D boxCollider = coinObject.GetComponent<BoxCollider2D>();
        UtilColliderEvents triggerCoinCollider = coinObject.AddComponent<UtilColliderEvents>();
        triggerCoinCollider.OnColliderEnterEvent += HandleColliderTriggerEnter;
    }

    private void HandleColliderTriggerEnter(GameObject gameObject, Collider2D collider)
    {
        Collect(collider);
        used = true;
    }

    void Collect(Collider2D collider)
    {
        if (used) return;
        UtilSoundManager.PlaySoundCoin(coinObject);
        GameObject.Destroy(coinObject);
        GameManager.Coins.Remove(id);
        InventoryBase inventory = GameManager.Player.Inventory;
        inventory.AddCoins(1);

    }
}

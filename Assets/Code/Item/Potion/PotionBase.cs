using UnityEngine;

public class PotionBase
{
    public int id;
    public string filename;
    public GameObject potionObject;
    public Transform potionPosition;
    public bool used = false;
    public LevelLoaderBase levelLoader;

    public virtual void UsePotion(Collider2D collider){

    }


    public void InitPotion()
    {
        LoadPotion();
        LoadColliders();
    }

    public void LoadPotion()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoaderBase>();
        GameObject prefab = Resources.Load<GameObject>(filename);
        potionObject = levelLoader.PrefabInstantiate(prefab);
        potionObject.transform.position = potionPosition.position;
    }

    private void LoadColliders()
    {
        BoxCollider2D boxCollider = potionObject.GetComponent<BoxCollider2D>();
        UtilColliderEvents triggerPotionCollider = potionObject.AddComponent<UtilColliderEvents>();
        triggerPotionCollider.OnColliderEnterEvent += HandleColliderTriggerEnter;
    }

    private void HandleColliderTriggerEnter(GameObject gameObject, Collider2D collider)
    {
        UsePotion(collider);
        used = true;
    }
}

using UnityEngine;

public class PotionBase
{
    public int id;
    public string filename;
    public GameObject potionObject;
    public Transform potionPosition;
    public bool used = false;
    public LevelBaseLoader levelLoader;

    public virtual void UsePotion(Collider2D collider){

    }


    public void InitPotion()
    {
        LoadPotion();
        LoadColliders();
    }

    public void LoadPotion()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelBaseLoader>();
        GameObject prefab = Resources.Load<GameObject>(filename);
        potionObject = levelLoader.PrefabInstantiate(prefab);
        potionObject.transform.position = potionPosition.position;
    }

    private void LoadColliders()
    {
        GameObject triggerCollider = AddBoxCollider("TriggerCollider", new Vector2(0f, 0f), new Vector2(1f, 1f));
        UtilColliderEvents triggerPotionCollider = triggerCollider.AddComponent<UtilColliderEvents>();
        triggerPotionCollider.OnColliderEnterEvent += HandleColliderTriggerEnter;
    }

    private void HandleColliderTriggerEnter(GameObject gameObject, Collider2D collider)
    {
        UsePotion(collider);
        used = true;
    }

    GameObject AddBoxCollider(string name, Vector2 offset, Vector2 size)
    {
        GameObject colliderObject = new GameObject(name);
        colliderObject.transform.parent = potionObject.transform;
        colliderObject.transform.localPosition = offset;
        BoxCollider2D boxCollider = colliderObject.AddComponent<BoxCollider2D>();
        boxCollider.size = size;
        boxCollider.isTrigger = true;
        return colliderObject;
    }
}

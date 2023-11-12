using UnityEngine;

public class PotionBase
{
    public int id;
    public string filename;
    public GameObject potionObject;
    public Transform potionPosition;
    public bool used = false;

    public virtual void UsePotion(Collider2D collider){

    }


    public void InitPotion()
    {
        LoadPotion();
        LoadSprite();
        LoadColliders();
    }

    public void LoadPotion()
    {
        potionObject = new GameObject("Potion" + id);
        potionObject.transform.position = potionPosition.position;
    }

    private void LoadSprite()
    {
        SpriteRenderer spriteRenderer = potionObject.AddComponent<SpriteRenderer>();
        var rawData = System.IO.File.ReadAllBytes(filename);
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(rawData);
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;
        spriteRenderer.sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
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

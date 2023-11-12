using UnityEngine;

public class PotionHealing : PotionBase
{
    readonly int healing = 100;
    
    public PotionHealing(int id, Transform potionPosition)
    {
        this.id = id;
        this.potionPosition = potionPosition;
        filename = "Assets/Sprites/Health.png";
        InitPotion();
    }

    public override void UsePotion(Collider2D collider)
    {
        if (used) return;
        GameObject.Destroy(potionObject);
        LevelBaseLoader levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelBaseLoader>();
        levelLoader.potionManager.RemovePotion(id);
        int targetId = collider.gameObject.GetComponentInParent<PlayerState>().id;
        levelLoader.playerManager.players[targetId].getHealing(healing);

    }
}

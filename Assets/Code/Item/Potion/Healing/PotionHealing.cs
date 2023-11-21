using UnityEngine;

public class PotionHealing : PotionBase
{
    readonly int healing = 1;
    
    public PotionHealing(int id, Transform potionPosition)
    {
        this.id = id;
        this.potionPosition = potionPosition;
        filename = "Prefaps/Potion/PotionHealth";
        InitPotion();
    }

    public override void UsePotion(Collider2D collider)
    {
        if (used) return;
        GameObject.Destroy(potionObject);
        levelLoader.potionManager.RemovePotion(id);
        levelLoader.playerManager.player.TakeHealing(healing);

    }
}

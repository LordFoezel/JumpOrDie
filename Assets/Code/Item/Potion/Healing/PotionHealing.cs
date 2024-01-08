using UnityEngine;

public class PotionHealing : PotionBase
{
    private int healing = 1;
    
    public PotionHealing(int id, Transform potionPosition)
    {
        this.id = id;
        this.potionPosition = potionPosition;
        filename = "Prefaps/Potion/PotionHealth";
        InitPotion();
        SetHealing();
    }

     public void SetHealing()
    {
        switch(GameManager.Difficult){
            case 1:
            healing = 3;
            return;
            case 2:
            healing = 2;
            return;
            case 3:
            healing = 1;
            return;
        }
    }

    public override void UsePotion(Collider2D collider)
    {
        if (used) return;
        UtilSoundManager.PlaySoundHealing(potionObject);
        GameObject.Destroy(potionObject);
        GameManager.Potions.Remove(id);
        GameManager.Player.TakeHealing(healing);

    }
}

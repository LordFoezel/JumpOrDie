using UnityEngine;

#nullable enable
public class SpikeTrap : TrapBase
{
    public SpikeTrap(int id, GameObject trapPosition, GameObject? switchPosition, bool isReady = true)
        #nullable disable
    {
        this.id = id;
        filename = "Prefaps/Trap/SpikeTrap";
        name = "SpikeTrap";
        this.trapPosition = trapPosition;
        this.switchPosition = switchPosition;
        this.IsReady = isReady;
        InitTrap();
    }

    public override void SetDamage()
    {
        switch(GameManager.Difficult){
            case 1:
            damage = 1;
            return;
            case 2:
            damage = 2;
            return;
            case 3:
            damage = 3;
            return;
        }
    }
}

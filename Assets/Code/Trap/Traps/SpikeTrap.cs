using UnityEngine;
#nullable enable
public class SpikeTrap : TrapBase
{
    public SpikeTrap(int id, GameObject trapPosition, GameObject? switchPosition, bool isReady = true)
        #nullable disable
    {
        this.id = id;
        damage = 1;
        filename = "Prefaps/Trap/SpikeTrap";
        name = "SpikeTrap";
        this.trapPosition = trapPosition;
        this.switchPosition = switchPosition;
        this.isReady = isReady;
        InitTrap();
    }
}

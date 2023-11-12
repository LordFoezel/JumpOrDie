using UnityEngine;
using System;

public class TrapSwitch : MonoBehaviour
{
    bool isActive;
    TrapBase trap;
    float lastTrigger = 0;

    public void Switch()
    {
        if(lastTrigger + 1 >= Time.time) return;
        lastTrigger = Time.time;
        isActive = !isActive;
        trap.SetIsReady(isActive);
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public void SetTrap(TrapBase trap)
    {
        this.trap = trap;
    }
}

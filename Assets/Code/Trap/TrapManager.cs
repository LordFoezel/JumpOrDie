using System.Collections.Generic;
using UnityEngine;

public class TrapManager : ITickable
{

    public TrapManager()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpikeTrap");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            GameObject trapPositio = GameObject.Find("SpikeTrap" + index);
            if(!trapPositio) continue;
            GameObject switchPositio = GameObject.Find("SpikeSwitch" + index);
            TrapBase newTrap = new SpikeTrap(index, trapPositio, switchPositio);
            GameManager.Traps.Add(index, newTrap);
            index += 1;
        }
    }

    public override void Tick()
    {
        foreach (KeyValuePair<int, TrapBase> trap in GameManager.Traps)
        {
            trap.Value.Tick();
        }
    }
}

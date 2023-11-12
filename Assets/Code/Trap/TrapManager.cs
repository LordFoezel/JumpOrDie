using System.Collections.Generic;
using UnityEngine;

public class TrapManager
{
    public Dictionary<int, TrapBase> traps;

    public TrapManager()
    {
        traps = new Dictionary<int, TrapBase>();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpikeTrap");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            GameObject trapPositio = GameObject.Find("SpikeTrap" + index);
            if(!trapPositio) continue;
            GameObject switchPositio = GameObject.Find("SpikeSwitch" + index);
            TrapBase newTrap = new SpikeTrap(index, trapPositio, switchPositio);
            traps.Add(index, newTrap);
            index += 1;
        }
    }

    public void Tick()
    {
        foreach (KeyValuePair<int, TrapBase> trap in traps)
        {
            trap.Value.Tick();
        }
    }
}

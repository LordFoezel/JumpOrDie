using UnityEngine;

public class TrapManager
{

    public TrapManager()
    {
        AddTraps();
    }

    public void AddTraps()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpikeTrap");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            AddTrap(index);
            index += 1;
        }
    }

    public void AddTrap(int id)
    {
        GameObject trapPositio = GameObject.Find("SpikeTrap" + id);
        if (!trapPositio) return;
        GameObject switchPositio = GameObject.Find("SpikeSwitch" + id);
        TrapBase newTrap = new SpikeTrap(id, trapPositio, switchPositio);
        GameManager.Traps.Add(id, newTrap);
    }
}

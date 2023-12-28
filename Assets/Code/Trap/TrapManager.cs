using UnityEngine;
using System.Collections.Generic;

public class TrapManager
{
    public void LoadTraps()
    {
        SaveGameData savedData = UtilSaveManager.LoadData();
        if(UtilBool.IntToBool(savedData.isIngame)) LoadOldTraps(savedData);
        else LoadNewTraps();
    }

    public void LoadOldTraps(SaveGameData savedData)
    {
        List<TrapData> trapData = savedData.traps;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpikeTrap");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            bool isReady = true;
            if (trapData.Count != 0) {
                isReady = UtilBool.IntToBool(trapData[index].isReady);
            }
            AddTrap(index, isReady);
            index += 1;
        }
    }

      public void LoadNewTraps()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpikeTrap");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            bool isReady = true;
            AddTrap(index, isReady);
            index += 1;
        }
    }

    public void AddTrap(int id, bool isReady)
    {
        GameObject trapPositio = GameObject.Find("SpikeTrap" + id);
        if (!trapPositio) return;
        GameObject switchPositio = GameObject.Find("SpikeSwitch" + id);
        TrapBase newTrap = new SpikeTrap(id, trapPositio, switchPositio, isReady);
        GameManager.Traps.Add(id, newTrap);
    }
}

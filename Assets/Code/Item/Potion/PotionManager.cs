using System.Collections.Generic;
using UnityEngine;

public class PotionManager : ITickable
{
    public PotionManager()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PotionHealth");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            PotionBase newPotion = new PotionHealing(index, spawnPoint.transform);
            GameManager.Potions.Add(index, newPotion);
            index += 1;
        }
    }

    public override void Tick()
    {
    }
}

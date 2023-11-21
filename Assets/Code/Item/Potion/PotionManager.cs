using System.Collections.Generic;
using UnityEngine;

public class PotionManager : ITickable
{
    public Dictionary<int, PotionBase> potions;

    public PotionManager()
    {
        potions = new Dictionary<int, PotionBase>();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PotionHealth");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            PotionBase newPotion = new PotionHealing(index, spawnPoint.transform);
            potions.Add(index, newPotion);
            index += 1;
        }
    }

    public void RemovePotion(int id)
    {
        potions.Remove(id);
    }

    public override void Tick()
    {
    }
}

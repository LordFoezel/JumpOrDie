using UnityEngine;
using System.Collections.Generic;

public class PotionManager
{
    public PotionManager()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        List<int> potions = savedData.potions;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PotionHealth");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (potions.Count != 0)
            {
                if (potions.Contains(index))
                {
                    PotionBase newPotion = new PotionHealing(index, spawnPoint.transform);
                    GameManager.Potions.Add(index, newPotion);
                }
            }
            else
            {
                PotionBase newPotion = new PotionHealing(index, spawnPoint.transform);
                GameManager.Potions.Add(index, newPotion);
            }
            index += 1;
        }
    }
}

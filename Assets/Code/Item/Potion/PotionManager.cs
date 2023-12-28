using UnityEngine;
using System.Collections.Generic;

public class PotionManager
{
    public void LoadPotions()
    {
        SaveGameData savedData = UtilSaveManager.LoadData();
        if(UtilBool.IntToBool(savedData.isIngame)) LoadOldPotions(savedData);
        else LoadNewPotions();
    }

      public void LoadOldPotions(SaveGameData savedData)
    {
        List<int> potions = savedData.potions;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PotionHealth");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (potions.Contains(index))
            {
                PotionBase newPotion = new PotionHealing(index, spawnPoint.transform);
                GameManager.Potions.Add(index, newPotion);
            }
            index += 1;
        }
    }

    public void LoadNewPotions()
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
}

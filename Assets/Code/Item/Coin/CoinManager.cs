using System.Collections.Generic;
using UnityEngine;

public class CoinManager
{
    public void LoadCoins()
    {        
        SaveGameData savedData = UtilSaveManager.LoadData();
        if(UtilBool.IntToBool(savedData.isIngame)) LoadOldCoins(savedData);
        else LoadNewCoins();
    }

      public void LoadOldCoins(SaveGameData savedData)
    {
        List<int> coins = savedData.coins;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Coin");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (coins.Contains(index))
            {
                CoinBase newCoin = new CoinBase(index, spawnPoint.transform);
                GameManager.Coins.Add(index, newCoin);
            }
            index += 1;
        }
    }

    public void LoadNewCoins()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Coin");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            CoinBase newCoin = new CoinBase(index, spawnPoint.transform);
            GameManager.Coins.Add(index, newCoin);
            index += 1;
        }
    }
}

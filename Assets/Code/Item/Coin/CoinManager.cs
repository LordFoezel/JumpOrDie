using System.Collections.Generic;
using UnityEngine;

public class CoinManager
{
    public void LoadCoins()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        List<int> coins = savedData.coins;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Coin");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (coins.Count != 0)
            {
                if (coins.Contains(index))
                {
                    CoinBase newCoin = new CoinBase(index, spawnPoint.transform);
                    GameManager.Coins.Add(index, newCoin);
                }
            }
            else
            {
                CoinBase newCoin = new CoinBase(index, spawnPoint.transform);
                GameManager.Coins.Add(index, newCoin);
            }
            index += 1;
        }
    }
}

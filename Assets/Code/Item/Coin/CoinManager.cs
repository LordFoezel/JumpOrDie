using System.Collections.Generic;
using UnityEngine;

public class CoinManager : ITickable
{
    public CoinManager()
    {
    }

    public void LoadCoins()
    {
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        List<int> coins = savedData.coins;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Coin");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if(!coins.Contains(index) && coins.Count != 0) return;
            CoinBase newCoin = new CoinBase(index, spawnPoint.transform);
            GameManager.Coins.Add(index, newCoin);
            index += 1;
        }
    }

    public override void Tick()
    {
    }
}

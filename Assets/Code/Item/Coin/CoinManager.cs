using System.Collections.Generic;
using UnityEngine;

public class CoinManager
{
    public Dictionary<int, CoinBase> coins;

    public CoinManager()
    {
        coins = new Dictionary<int, CoinBase>();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Coin");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            CoinBase newCoin = new CoinBase(index, spawnPoint.transform);
            coins.Add(index, newCoin);
            index += 1;
        }
    }

    public void RemoveCoin(int id)
    {
        coins.Remove(id);
    }

    public void Tick()
    {
    }
}

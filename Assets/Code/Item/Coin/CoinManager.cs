using System.Collections.Generic;
using UnityEngine;

public class CoinManager : ITickable
{
    public CoinManager()
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

    public override void Tick()
    {
    }
}

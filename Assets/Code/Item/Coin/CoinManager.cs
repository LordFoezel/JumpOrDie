using System.Collections.Generic;
using UnityEngine;

public class CoinManager : ITickable
{
    private Dictionary<int, CoinBase> coins;
    public Dictionary<int, CoinBase> Coins { get => coins; set => coins = value; }

    public CoinManager()
    {
        Coins = new Dictionary<int, CoinBase>();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Coin");
        int index = 0;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            CoinBase newCoin = new CoinBase(index, spawnPoint.transform);
            Coins.Add(index, newCoin);
            index += 1;
        }        
    }


    public void RemoveCoin(int id)
    {
        Coins.Remove(id);
    }

    public override void Tick()
    {
    }
}

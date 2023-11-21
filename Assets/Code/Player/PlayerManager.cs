using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ITickable
{
    public Dictionary<int, PlayerBase> players;

    readonly int playerCount = 1; // Erstmal singel player

    public PlayerManager()
    {
        players = new Dictionary<int, PlayerBase>();

        // Eventuel nicht dynamisch, damit man verschidene Player verwenden kann...
        for (int i = 1; i <= playerCount; i += 1)
        {
            PlayerBase newPlayer = new Player(i);
            players.Add(i, newPlayer);
        }

    }

    public override void Tick()
    {
        foreach (KeyValuePair<int, PlayerBase> player in players)
        {
            player.Value.Tick();
        }
    }
}

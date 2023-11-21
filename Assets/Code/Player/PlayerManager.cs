using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ITickable
{
    public PlayerBase player;

    readonly int playerCount = 1;

    public PlayerManager()
    {
        player = new Player(1);
    }

    public override void Tick()
    {
            player.Tick();
    }

    public PlayerBase GetPlayer()
    {
        return player;
    }
}

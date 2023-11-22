using UnityEngine;

public class PlayerManager
{
    public PlayerManager()
    {
        AddPlayer();
    }

    public void AddPlayer(int id = 0)
    {
        GameManager.Player = new Player(id);
        UtilSaveManager.LevelData savedData = UtilSaveManager.LoadLevelData();
        if(savedData.positionX == 0 && savedData.positionY == 0) return;
        GameManager.Player.SetPlayerData(savedData.health, savedData.levelCoins, new Vector2(savedData.positionX, savedData.positionY));

    }

    public void RemovePlayer(int id)
    {
        GameManager.Player.RemovePlayer();
        GameManager.Player = null;
    }
}

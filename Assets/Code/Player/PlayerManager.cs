using UnityEngine;

public class PlayerManager
{
    public void AddPlayer(int id = 0)
    {
        GameManager.Player = new Player(id);
        UtilSaveManager.SaveGameData savedData = UtilSaveManager.LoadSaveData();
        UtilDebug.Log(savedData.positionX + "/" + savedData.positionY);
        if(savedData.positionX == 0 && savedData.positionY == 0) return;
        GameManager.Player.SetPlayerData(savedData.health, savedData.levelCoins, new Vector2(savedData.positionX, savedData.positionY));

    }
}

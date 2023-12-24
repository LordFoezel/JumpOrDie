using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextLevelName = "MainMenu";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player") return;
        PlayerBase player = GameManager.Player;
        UtilSaveManager.SaveTotalCoins(player.Inventory.GetCoins());
        UtilSaveManager.SaveIsIngame(0);
        UtilSaveManager.ClearLevelSave();
        GameManager.ClearAll();
        UtilLevelLoader utilLevelLoader = gameObject.AddComponent<UtilLevelLoader>();
        utilLevelLoader.LoadLevel(nextLevelName);
    }
}

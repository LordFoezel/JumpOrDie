using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextLevelName = "MainMenu";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player") return;
        PlayerBase player = GameManager.Player;
        // UtilSaveManager.SaveTotalCoins();
        // UtilSaveManager.SaveExitGame(player.HitPoints,  , new Vector2());
        UtilSaveManager.SaveLevelExit(player.Inventory.GetCoins());
        GameManager.ClearAll();
        UtilLevelLoader utilLevelLoader = gameObject.AddComponent<UtilLevelLoader>();
        utilLevelLoader.LoadLevel(nextLevelName);
    }
}

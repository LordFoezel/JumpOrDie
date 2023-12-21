using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextLevelName = "MainMenu";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player") return;
        PlayerBase player = GameManager.Player;

        UtilSaveManager.SaveCurrentGame(player.HitPoints,  player.Inventory.GetCoins(), new Vector2());
        UtilSaveManager.SaveTotalCoins();

        UtilLevelLoader utilLevelLoader = gameObject.AddComponent<UtilLevelLoader>();
        utilLevelLoader.LoadLevel(nextLevelName);
        GameManager.ClearAll();
    }
}

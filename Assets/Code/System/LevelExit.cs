using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextLevelName = "MainMenu";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player") return;
        LevelBaseLoader levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelBaseLoader>();
        PlayerBase player = levelLoader.playerManager.player;

        UtilSaveManager.SaveCurrentGame(player.GetHitPoints(),  player.GetInventory().GetCoins(), new Vector2());
        UtilSaveManager.SaveTotalCoins();

        UtilLevelLoader utilLevelLoader = gameObject.AddComponent<UtilLevelLoader>();
        utilLevelLoader.LoadLevel(nextLevelName);
    }
}

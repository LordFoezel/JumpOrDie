using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextLevelName = "MainMenu";
    public bool isEnd = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player") return;
        if(isEnd) GameManager.GameIsEnded = true;
        PlayerBase player = GameManager.Player;
        UtilSaveManager.SaveIsIngame(0);
        UtilSaveManager.ClearLevelSave();
        GameManager.ClearAll();
        UtilLevelLoader utilLevelLoader = gameObject.AddComponent<UtilLevelLoader>();
        utilLevelLoader.LoadLevel(nextLevelName);
    }
}

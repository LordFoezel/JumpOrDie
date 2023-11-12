using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextLevelName = "MainMenu";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player") return;
        UtilLevelLoader levelLoader = gameObject.AddComponent<UtilLevelLoader>();
        levelLoader.LoadLevel(nextLevelName);
    }
}

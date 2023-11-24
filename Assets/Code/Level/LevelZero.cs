using UnityEngine;

public class LevelZero : LevelLoaderBase
{
    public override void InitLevel()
    {
        GameManager.ActualGameLevel = MapperLevel.GetLevelId(UtilEnum.GameLevel.MainMenu.ToString());
        GameManager.ActualGameState = UtilEnum.GameState.Menu;
        GameObject mainMenuPrefab = Resources.Load<GameObject>("Prefaps/Menu/MainMenu");
        GameObject mainMenu = Instantiate(mainMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mainMenu.SetActive(true);
        Canvas canvas = mainMenu.GetComponent<Canvas>();
        GameObject cameraObject = UtilCamera.CreateCamera();
        Camera camera = cameraObject.GetComponent<Camera>();
        canvas.worldCamera = camera;
    }
}

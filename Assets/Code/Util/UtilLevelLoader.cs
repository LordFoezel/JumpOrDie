using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UtilLevelLoader : MonoBehaviour
{
    private GameObject loadingScreen;
    private Slider loadingBar;
    private bool isLoading = false;
    void Awake()
    {
        GameObject LoadingScreenPrefab = Resources.Load<GameObject>("Prefaps/Menu/Loading");
        loadingScreen = Instantiate(LoadingScreenPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        loadingBar = GameObject.Find("LoadingBar").gameObject.GetComponent<Slider>();
        loadingScreen.SetActive(false);
    }

    public void LoadLevel(string level)
    {
        GameManager.SetActualGameState(UtilEnum.GameState.Loading);
        loadingScreen.SetActive(true);
        if (isLoading) return;
        isLoading = true;
        StartCoroutine(LoadLevelAsync(level));
    }

    IEnumerator LoadLevelAsync(string level)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
        if (asyncOperation.isDone) isLoading = false;
    }
}

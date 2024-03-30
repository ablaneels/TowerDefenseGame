using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentMap;
    public string currentMode;
    private void Awake() {
        if (PlayerPrefs.HasKey("Map") && PlayerPrefs.HasKey("Mode"))
        {
            currentMap = PlayerPrefs.GetString("Map");
            currentMode = PlayerPrefs.GetString("Mode");
        }
        else
        {
            GoToMenuScene();
        }
    }

    public void GoToMenuScene()
    {
        StartCoroutine(LoadAsyncchronously("MenuScene"));
    }

    IEnumerator LoadAsyncchronously(string sceneStr)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneStr);

        while (operation.isDone == false)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}

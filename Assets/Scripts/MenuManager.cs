using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mapUI;
    public GameObject modeUI;

    private string selectedMap;
    private string selectedMode;

    // Start is called before the first frame update
    void Start()
    {
        ResetMenu();
    }

    public void ChooseMap()
    {
        selectedMap = EventSystem.current.currentSelectedGameObject.name;
        modeUI.SetActive(true);
    }

    public void ChooseMode()
    {
        selectedMode = EventSystem.current.currentSelectedGameObject.name;

        PlayerPrefs.SetString("Map", selectedMap);
        PlayerPrefs.SetString("Mode", selectedMode);
        PlayerPrefs.Save();

        GoToGameScene();
    }

    public void ResetMenu()
    {
        selectedMap = null;
        selectedMode = null;

        mapUI.SetActive(true);
        modeUI.SetActive(false);
    }

    public void GoToGameScene()
    {
        StartCoroutine(LoadAsyncchronously("GameScene"));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] float timeToLoadScene = 0.1f;

    int pastSceneId = 0;

    IEnumerator LoadScene(int index)
    {
        pastSceneId = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(pastSceneId);
        yield return new WaitForSecondsRealtime(timeToLoadScene);
        SceneManager.LoadScene(index);
    }

    IEnumerator LoadSceneName(string name)
    {
        pastSceneId = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(pastSceneId);
        yield return new WaitForSecondsRealtime(timeToLoadScene);
        SceneManager.LoadScene(name);
    }



    public void LoadPreviousScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(currentIndex - 1));
    }

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(currentIndex + 1));     
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadSceneName(sceneName));
    }

    public float GetTimeToLoad()
    {
        return timeToLoadScene;
    }

    public void LoadPastScene()
    {
        StartCoroutine(LoadScene(pastSceneId));
    }
    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

}

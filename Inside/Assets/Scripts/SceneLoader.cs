using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] float timeToLoadScene = 0.1f;

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSecondsRealtime(timeToLoadScene);
        SceneManager.LoadScene(index);
    }

    IEnumerator LoadSceneName(string name)
    {
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

}

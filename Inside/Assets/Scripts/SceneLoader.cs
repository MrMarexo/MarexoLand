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

    public void LoadPreviousScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(currentIndex - 1));
    }

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        //load next scene
        StartCoroutine(LoadScene(currentIndex + 1));     
    }

    public float GetTimeToLoad()
    {
        return timeToLoadScene;
    }

}

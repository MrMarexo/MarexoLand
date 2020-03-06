using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoader : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

    [SerializeField] float waitTime = 1.2f;

    private void Start()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }

    public void LoadWinCanvas()
    {
        StartCoroutine(LoadCanvasAfterDelay(winCanvas));
    }

    public void LoadLoseCanvas()
    {
        StartCoroutine(LoadCanvasAfterDelay(loseCanvas));
    }

    IEnumerator LoadCanvasAfterDelay(GameObject canvas)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        canvas.SetActive(true);
    }


}

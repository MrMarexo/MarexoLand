using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagement : MonoBehaviour
{
    [SerializeField] GameObject canvasIntro;
    [SerializeField] GameObject canvasCalendar7;
    [SerializeField] GameObject canvasCalendar2;


    [SerializeField] float timeToSwitchCanvas = 0.2f;

    void Start()
    {
        TurnOffCanvases();
    }

    IEnumerator LoadCanvasInTime(GameObject canvas, bool enableDisable)
    {
        yield return new WaitForSecondsRealTime(timeToSwitchCanvas);
        canvas.setActive(enableDisable);
    }

    void TurnOffCanvases() 
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].setActive(false);
        }
    }

    public void StartGame()
    {
        string[] names = GetComponent<ValueManagement>().GetNames();
        if (names[0] == "" || names[1] == "") 
        {
            EnableIntro();
        }
    }

    public void EnableIntro()
    {
        StartCoroutine(LoadCanvasInTime(canvasIntro, true));
    }

    public void DisableIntro()
    {
        StartCoroutine(LoadCanvasInTime(canvasIntro, false));
    }

    public void EnableCalendar7()
    {
        StartCoroutine(LoadCanvasInTime(canvasCalendar7, true));
    }

    public void DisableCalendar7()
    {
        StartCoroutine(LoadCanvasInTime(canvasCalendar7, false));
    }

    public void EnableCalendar2()
    {
        StartCoroutine(LoadCanvasInTime(canvasCalendar2, true));
    }

    public void DisableCalendar2()
    {
        StartCoroutine(LoadCanvasInTime(canvasCalendar2, false));
    }

}

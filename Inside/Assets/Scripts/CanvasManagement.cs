using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagement : MonoBehaviour
{
    [SerializeField] List<GameObject> canvases;

    [SerializeField] float timeToSwitchCanvas = 0.2f;

    private void Start()
    {
        DisableCanvases();
    }

    IEnumerator LoadCanvas(GameObject canvas, bool enableDisable)
    {
        yield return new WaitForSecondsRealtime(timeToSwitchCanvas);
        canvas.SetActive(enableDisable);
    }
    
    public void StartGame()
    {
        //checking if the name values have been saved ---if the intro has been already completed
        string[] names = GetComponent<ValueManagement>().GetNames();
        if (names[0] == "" || names[1] == "" || names[2] == "") //badHabitName and firstCheckName
        {
            EnableIntro();
        }
    }

    public void EnableIntro()
    {
        StartCoroutine(LoadCanvas(canvases[0], true));
    }

    public int GetActiveCanvasIndex()
    {
        int activeCanvasIndex = 0;
        for (int i = 0; i < canvases.Count; i++)
        {
            if (canvases[i].active == true)
            {
                activeCanvasIndex = i;
            }
        }
        return activeCanvasIndex;
    }

    public void NextCanvas()
    {
        if (canvases[GetActiveCanvasIndex() + 1].tag == "Calendar")
        {
            ShowCalendar();
        }
        else
        {
            int activeIndex = GetActiveCanvasIndex();
            StartCoroutine(LoadCanvas(canvases[activeIndex], false));
            StartCoroutine(LoadCanvas(canvases[activeIndex + 1], true));
        } 
    }

    public void DisableCanvases()
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].SetActive(false);
        }
    }

    public void ShowCalendar()
    {
        int activeIndex = GetActiveCanvasIndex();

        int currentCalendarIndex = GetComponent<DateManagement>().GetCalendarIndex();

        GameObject currentCalendar = canvases[canvases.Count - 1].transform.GetChild(currentCalendarIndex).gameObject;
        GameObject otherCalendar = canvases[canvases.Count - 1].transform.GetChild(Mathf.Abs(currentCalendarIndex - 1)).gameObject;
        Debug.Log(currentCalendar.name);
        Debug.Log(GetComponent<DateManagement>().GetCalendarIndex());
        GetComponent<DateManagement>().LoadWeek();
        StartCoroutine(LoadCanvas(canvases[activeIndex], false)); //disable current active canvas


        StartCoroutine(LoadCanvas(canvases[canvases.Count - 1], true));
        StartCoroutine(LoadCanvas(otherCalendar, false)); //turns off the other canvas
        StartCoroutine(LoadCanvas(currentCalendar, true));
        Debug.Log(currentCalendar.name);
    }

    public void PreviousWeek()
    {
        GetComponent<DateManagement>().DecreaseWeekIndex();
        ShowCalendar();
    }

    public void NextWeek()
    {
        GetComponent<DateManagement>().IncreaseWeekIndex();
        ShowCalendar();
    }
    
}

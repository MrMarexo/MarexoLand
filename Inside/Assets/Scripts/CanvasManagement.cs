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
        string[] names = GetComponent<ValueManagement>().GetNames(); //badHabitName, firstCheckName, weeklyCheckName, secondCheckName (in that order)
        if (names[0] == "" || names[1] == "") 
        {
            FromTheBeginning();
        }
        else
        {
            //GameObject canvasToLoad = canvases[FindCanvasIndex("Daily_Check")];
            //StartCoroutine(LoadCanvas(canvasToLoad, true)); 
        }
    }

    //shows either yesterday or today daily check
    void ShowCorrectDailyCheck()
    {
        //finds the daily check parent game object
        GameObject dailyCheckObject = canvases[FindCanvasIndex("Daily_Check")];

        //check if daily check has been completed for yesterday and current day
        int correctCheckIndex = GetComponent<ValueManagement>().CheckValuesForDailyCheck(); //returns 0 if today should be loaded, 1 if yesterday

        //gets the correct daily check canvas
        GameObject correctDailyCheck = dailyCheckObject.transform.GetChild(correctCheckIndex).gameObject;
        GameObject otherDailyCheck = dailyCheckObject.transform.GetChild(Mathf.Abs(correctCheckIndex - 1)).gameObject;

        StartCoroutine(LoadDailyCheck(dailyCheckObject, correctDailyCheck, otherDailyCheck));
    }

    IEnumerator LoadDailyCheck(GameObject parent, GameObject correct, GameObject other)
    {
        int activeCanvasIndex = GetActiveCanvasIndex();
        yield return new WaitForSecondsRealtime(timeToSwitchCanvas);
        canvases[activeCanvasIndex].SetActive(false); //disables the current active canvas
        parent.SetActive(true); //enables the parent
        other.SetActive(false); //disables the other canvas
        correct.SetActive(true); //enables the correct canvas
    }

    //finds the canvas index according to its name
    int FindCanvasIndex(string name)
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            if (canvases[i].tag == name)
            {
                return i;
            }
        }
        Debug.LogWarning("Wrong name");
        return 999;
    }

    //load the first canvas
    public void FromTheBeginning()
    {
        StartCoroutine(LoadCanvas(canvases[0], true));
    }

    
    // finds the one active canvas (make sure there is only one active) and returns its index
    public int GetActiveCanvasIndex()
    {
        int activeCanvasIndex = 0;
        for (int i = 0; i < canvases.Count; i++)
        {
            if (canvases[i].activeSelf == true)
            {
                activeCanvasIndex = i;
            }
        }
        return activeCanvasIndex;
    }

    //loads next canvas if it is a regular one and disables the current one
    public void NextCanvas()
    {
        if (canvases[GetActiveCanvasIndex() + 1].tag == "Calendar")
        {
            ShowCalendar();
        }
        else if (canvases[GetActiveCanvasIndex() + 1].tag == "Daily_Check")
        {
            ShowCorrectDailyCheck();
        }
        else
        {
            int activeIndex = GetActiveCanvasIndex();
            StartCoroutine(LoadCanvas(canvases[activeIndex], false));
            StartCoroutine(LoadCanvas(canvases[activeIndex + 1], true));
        } 
    }

    //disables all canvases
    public void DisableCanvases()
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].SetActive(false);
        }
    }

    //loads correct calendar, either 7 or 2 days, disables the current canvas
    public void ShowCalendar()
    {
        //gets index of the calendar that needs to be shown
        int currentCalendarIndex = GetComponent<DateManagement>().GetCalendarIndex();

        //finds the correct calendar and identifies the other one
        GameObject currentCalendar = canvases[FindCanvasIndex("Calendar")].transform.GetChild(currentCalendarIndex).gameObject;
        GameObject otherCalendar = canvases[FindCanvasIndex("Calendar")].transform.GetChild(Mathf.Abs(currentCalendarIndex - 1)).gameObject;

        StartCoroutine(LoadCalendar(currentCalendar, otherCalendar));
    }

    //special one with LoadWeek ----put here to get rid of time discrepancy when loading other calendar type
    IEnumerator LoadCalendar(GameObject current, GameObject other)
    {
        int activeIndex = GetActiveCanvasIndex();
        yield return new WaitForSecondsRealtime(timeToSwitchCanvas);
        canvases[activeIndex].SetActive(false); //disables current active canvas
        canvases[FindCanvasIndex("Calendar")].SetActive(true); //enables the parent canvas
        GetComponent<DateManagement>().LoadWeek();  //creates a calendar
        other.SetActive(false);     //disables off the other canvas
        current.SetActive(true);    //enables the correct canvas
    }

    //methods to browse through the calendar
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

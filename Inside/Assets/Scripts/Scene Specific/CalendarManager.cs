using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    //2 calendar gameObjects
    [SerializeField] GameObject[] pages = new GameObject[2];

    //2 buttons to browse calendar
    [SerializeField] GameObject nextWeekButton;
    [SerializeField] GameObject previousWeekButton;

    //week number
    [SerializeField] TextMeshProUGUI weekNumberText;

    //array of 7 calendar gameobjects 
    TextMeshProUGUI[] days7 = new TextMeshProUGUI[7];

    //array of 2 calendar gameobjexts
    TextMeshProUGUI[] days2 = new TextMeshProUGUI[2];

    //array of 7 and 2 day arrays
    TextMeshProUGUI[][] calendars = new TextMeshProUGUI[2][]; 

    int currentWeekIndex;
    int currentDayIndex;
    int curDateNumber;

    int weekIndex;

    float timeToWait;

    ValueManagement vM;
    DateManagement dM;

    void Start()
    {
        dM = FindObjectOfType<DateManagement>();
        vM = FindObjectOfType<ValueManagement>();

        FillUpCalendars();
        timeToWait = FindObjectOfType<SceneLoader>().GetTimeToLoad();
        currentDayIndex = dM.GetCurrentDayIndex();
        curDateNumber = currentDayIndex + 1;
        currentWeekIndex = dM.GetCurrentWeek();
        weekIndex = currentWeekIndex;
        InstantlyCreateCalendar();
    }

    void UpdateWeekNumberText()
    {
        weekNumberText.text = "Week " + (weekIndex + 1).ToString();
    }

    //enables and disables the browse calendar buttons
    void ButtonsEnableDisable()
    {
        //get the buttons of the proper calendar and enable them
        previousWeekButton.SetActive(true);
        nextWeekButton.SetActive(true);

        //if current is 0 disable the previous button
        if (weekIndex == 0)
        {
            previousWeekButton.SetActive(false);
        }

        //if current is four disable the next button and dont run further
        if (weekIndex == 4) //will later tweak when more weeks will be possible
        {
            nextWeekButton.SetActive(false);
        }
        else if (weekIndex == currentWeekIndex)
        {
            nextWeekButton.SetActive(false);
        }
    }

    //fills up calendars array with two days arrays
    void FillUpCalendars()
    {
        //finds the 7 day game objects and puts them into the array
        Button[] gO7 = pages[0].GetComponentsInChildren<Button>();
        for (int i = 0; i < gO7.Length; i++)
        {
            var tM = gO7[i].gameObject.GetComponent<TextMeshProUGUI>();
            days7[i] = tM;
        }

        ////finds the 2 day game objects and puts them into the array
        Button[] gO2 = pages[1].GetComponentsInChildren<Button>();
        for (int i = 0; i < gO2.Length; i++)
        {
            var tM = gO2[i].gameObject.GetComponent<TextMeshProUGUI>();
            days2[i] = tM;
        }

        //adds the two arrays to the calendar array
        calendars[0] = days7;
        calendars[1] = days2;
    }

    //returns index of calendar[][] .... also for CanvasManagement.calendars[]
    int GetCalendarIndex()
    {
        if (weekIndex % 4 == 0 && weekIndex != 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //creates calendar without waiting
    void InstantlyCreateCalendar()
    {
        EnableDisableWeeks();
        CalculateCalendar();
        ButtonsEnableDisable();
        UpdateWeekNumberText();
    }

    //creates calendar with waiting to simulate loading a new scene
    IEnumerator CreateCalendar()
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        EnableDisableWeeks();
        CalculateCalendar();
        ButtonsEnableDisable();
        UpdateWeekNumberText();
    }

    void EnableDisableWeeks()
    {
        int correctPageIndex = GetCalendarIndex();
        //enable the correct page
        pages[correctPageIndex].SetActive(true);
        //disable the other one
        pages[Mathf.Abs(correctPageIndex - 1)].SetActive(false);
    }

    public void ShowNextWeek()
    {
        weekIndex++;
        StartCoroutine(CreateCalendar());
    }

    public void ShowPreviousWeek()
    {
        weekIndex--;
        StartCoroutine(CreateCalendar());
    }

    void CalculateCalendar()
    {
        var weekToShow = calendars[GetCalendarIndex()];

        for (int i = 0; i < weekToShow.Length; i++)
        {
            //colors
            Color32 incompleteColor = Colors.incompleteColor;
            Color32 invisibleColor = Colors.invisibleColor;
            Color32 completeColor = Colors.completeColor;
            Color32 failedColor = Colors.failedColor;

            var cur = weekToShow[i];

            int curDateNumber = currentDayIndex + 1;

            //0 is day text, 1 is the first check, 2 is the second, 3 is the underline
            TextMeshProUGUI[] arrayOfTMs = cur.GetComponentsInChildren<TextMeshProUGUI>();
            
            int dayInArr = (weekIndex * 7) + i; //current day in loop - array number == zero based
            int dayGlobal = dayInArr + 1; //current day in loop = 1 based number
            cur.text = dayGlobal.ToString();

            //set up regular properties
            arrayOfTMs[3].color = invisibleColor; //colors the underline color -- its invisible
            arrayOfTMs[0].color = invisibleColor; //this only makes it invisible --need to figure out how to disable it
            //-- only the present and past days will be active (visible, clickable), thanks to the fragment right below

            //set for today and all days before today
            if (dayGlobal <= curDateNumber)
            {
                arrayOfTMs[0].color = incompleteColor;
            }

            //set for today
            if (dayGlobal == curDateNumber)
            {
                //indicate this is the current day
                arrayOfTMs[3].color = incompleteColor;                                                          //!!!
            }

            //gets values for the current day in loop
            string[] stringValues = vM.GetValuesOfIndex(dayInArr); //0 is firstcheck, 1 is secondcheck and 2 is habitcheck 
            

            //sets the default - if the value is "" then the dot won't be visible
            arrayOfTMs[1].text = stringValues[0]; //sets the first daily check
            arrayOfTMs[2].text = stringValues[1]; //sets the second daily check

            //colors first daily checks
            if (stringValues[0] == "S") //checks first daily check value
            {
                //the index after getcomponents is +1 because the first TextMeshPro component is the day number(current one)
                arrayOfTMs[1].text = ".";
                arrayOfTMs[1].color = completeColor;
            }
            else if (stringValues[0] == "F")
            {
                arrayOfTMs[1].text = ".";
                arrayOfTMs[1].color = failedColor;
            }

            //colors second daily checks
            if (stringValues[1] == "S") //checks second daily check value
            {
                //the index after getcomponents is +1 because the first TextMeshPro component is the day number(current one)
                arrayOfTMs[2].text = ".";
                arrayOfTMs[2].color = completeColor;
            }
            else if (stringValues[1] == "F")
            {
                arrayOfTMs[2].text = ".";
                arrayOfTMs[2].color = failedColor;
            }

            //colors days and underline
            if (stringValues[2] == "S")
            {
                cur.color = completeColor;
                //colors underline only if its the current date
                if (dayGlobal == curDateNumber)
                {
                    arrayOfTMs[3].color = completeColor;
                }

            }
            if (stringValues[2] == "F")
            {
                cur.color = failedColor;
                //colors underline only if its the current date
                if (dayGlobal == curDateNumber)
                {
                    arrayOfTMs[3].color = failedColor;
                }
            }
        }
    }
}

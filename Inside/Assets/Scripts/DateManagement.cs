using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DateManagement : MonoBehaviour
{
    //colors
    [SerializeField] Color32 completeColor = new Color32(255, 255, 255, 255);
    [SerializeField] Color32 failedColor = new Color32(0, 0, 0, 255);
    [SerializeField] Color32 incompleteColor = new Color32(96, 87, 87, 255);
    [SerializeField] Color32 invisibleColor = new Color32(0, 0, 0, 0);

    //current date
    DateTime curDate;

    //launch date retrieved from PlayerPrefs and transformed back to DateTime
    DateTime savedLaunchDate;

    //how many days from launch date, launch date being 1
    int curDateNumber;

    //how many days from launch date, launch date being 0 ---zero based
    int curDateIndex;
    
    //index of the current week, can change to browse the calendar
    int currentWeekIndex;

    //insex of current week, stays the same
    int originalCurrentWeekIndex;

    //array of 7 calendar gameobjects 
    [SerializeField] TextMeshProUGUI[] daysArray7 = new TextMeshProUGUI[7];

    //array of 2 calendar gameobjects
    [SerializeField] TextMeshProUGUI[] daysArray2 = new TextMeshProUGUI[2];

    TextMeshProUGUI[][] calendars = new TextMeshProUGUI[2][];

    private void Awake()
    {
        Date();
    }

    void Start()
    {
        FillUpCalendars();
    }
    
    //fills up calendars array with two days arrays
    void FillUpCalendars()
    {
        calendars[0] = daysArray7;
        calendars[1] = daysArray2;
    }

    //used to define the launch date and current date and save/get from PlayerPrefs
    void Date()
    {
        curDate = DateTime.Now.Date;     

        // try to get the launch date saved as a string:
        string savedLaunchDateString = PlayerPrefs.GetString("savedLaunchDate", "");
        if (savedLaunchDateString == "")
        { // if not saved yet...
          // convert current date to string...
            savedLaunchDateString = curDate.ToString();
            // and save it in PlayerPrefs as LaunchDate:
            PlayerPrefs.SetString("savedLaunchDate", savedLaunchDateString);
        }
        // at this point, the string savedDate contains the launch date
        // let's convert it to DateTime:
        DateTime savedLaunchDate;
        DateTime.TryParse(savedLaunchDateString, out savedLaunchDate);

        //for testing (zero based)
        savedLaunchDate = savedLaunchDate.AddDays(-8);

        curDateIndex = (curDate - savedLaunchDate).Days;
        curDateNumber = curDateIndex + 1;

        currentWeekIndex = curDateIndex / 7;       //floored number after division
        originalCurrentWeekIndex = curDateIndex / 7;
    }

    public int GetCurrentDayIndex()
    {
        return curDateIndex;
    }
    
    //returns index of calendar[][] .... also for CanvasManagement.calendars[]
    public int GetCalendarIndex()
    {
        if (currentWeekIndex % 4 == 0 && currentWeekIndex != 0) 
        {
            return 1;
        }
        else
        {
            return 0;
        } 
    }

    void CreateCalendar(int index)
    {
        for (int i = 0; i < calendars[index].Length; i++)
        {
            var cur = calendars[index][i];

            //0 is day text, 1 is the first check, 2 is the second, 3 is the underline
            TextMeshProUGUI[] arrayOfTMs = cur.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log("length of arrayTMs: " + arrayOfTMs.Length);
            Debug.Log("name of the first GO: " + arrayOfTMs[0].gameObject.name);
            Debug.Log("name of the second GO: " + arrayOfTMs[1].gameObject.name);
            Debug.Log("name of the third GO: " + arrayOfTMs[2].gameObject.name);
            Debug.Log("name of the fourth GO: " + arrayOfTMs[3].gameObject.name);

            int dayInArr = (currentWeekIndex * 7) + i; //current day in loop - array number == zero based
            int dayGlobal = dayInArr + 1; //current day in loop = 1 based number
            cur.text = dayGlobal.ToString();

            //set up regular properties
            cur.color = incompleteColor;
            arrayOfTMs[3].color = invisibleColor; //colors the underline bkg color -- its invisible
            arrayOfTMs[0].color = invisibleColor; //disables the day object itself -------this only makes it invisible --need to figure out how to disable it
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
                arrayOfTMs[3].color = incompleteColor;
            }

            //gets values for the current day in loop
            string[] stringValues = GetComponent<ValueManagement>().GetValuesOfIndex(dayInArr);

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

    //these are for canvasmanagement
    
    public void LoadWeek()
    {
        CreateCalendar(GetCalendarIndex());
    }

    public void IncreaseWeekIndex()
    {
        currentWeekIndex++;
    }

    public void DecreaseWeekIndex()
    {
        currentWeekIndex--;
    }

    public int[] GetCurrentWeek()
    {
        int[] indexes = { currentWeekIndex, originalCurrentWeekIndex };
        return indexes;
    }
    
}

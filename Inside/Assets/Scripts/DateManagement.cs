using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DateManagement : MonoBehaviour
{
    //colors
    [SerializeField] Color32 completeColor = new Color32(255, 255, 255, 255);
    [SerializeField] Color32 incompleteColor = new Color32(0, 0, 0, 255);
    [SerializeField] Color32 failedColor = new Color32(140, 48, 48, 255);

    //current date
    DateTime curDate;

    //launch date retrieved from PlayerPrefs and transformed back to DateTime
    DateTime savedLaunchDate;

    //how many days from launch date, launch date being 1
    int curDateNumber;

    //how many days from launch date, launch date being 0 ---zero based
    int curDateIndex;
    
    //index of the current week
    int currentWeekIndex;

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
        savedLaunchDate = savedLaunchDate.AddDays(-5);

        curDateIndex = (curDate - savedLaunchDate).Days;
        curDateNumber = curDateIndex + 1;

        currentWeekIndex = curDateIndex / 7;       //floored number after division
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

            //set up regular properties
            cur.fontStyle = FontStyles.Normal;              
            cur.color = incompleteColor;

            
            //Debug.Log((currentWeekIndex * 7) + i + 1);
            int dayInArr = (currentWeekIndex * 7) + i; //current day in loop - array number == zero based
            int dayGlobal = dayInArr + 1; //current day in loop - 1 based number
            cur.text = dayGlobal.ToString();

            string[] stringValues = GetComponent<ValueManagement>().GetValuesOfIndex(dayInArr); //gets values for the current day in loop

            //the index after getcomponents is +1 because the first TextMeshPro component is the day number(current one)
            cur.GetComponentsInChildren<TextMeshProUGUI>()[1].text = stringValues[0]; //sets the first daily check
            cur.GetComponentsInChildren<TextMeshProUGUI>()[2].text = stringValues[1]; //sets the second daily check

            //colors days
            if (stringValues[2] == "S") 
            {
                cur.color = completeColor;
            }
            if (stringValues[2] == "F") 
            {
                cur.color = failedColor;
            }

            //colors first daily checks
            if (stringValues[0] == "S")
            {
                cur.GetComponentsInChildren<TextMeshProUGUI>()[1].color = completeColor;
            }
            if (stringValues[0] == "F")
            {
                cur.GetComponentsInChildren<TextMeshProUGUI>()[1].color = failedColor;
            }

            //colors first daily checks
            if (stringValues[1] == "S")
            {
                cur.GetComponentsInChildren<TextMeshProUGUI>()[2].color = completeColor;
            }
            if (stringValues[1] == "F")
            {
                cur.GetComponentsInChildren<TextMeshProUGUI>()[2].color = failedColor;
            }


            if (dayGlobal == curDateNumber)
            {
                //indicate this is the current day
                cur.fontStyle = FontStyles.Underline;
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
        if (currentWeekIndex != 4)
        {
            currentWeekIndex++;
        }
    }

    public void DecreaseWeekIndex()
    {
        if (currentWeekIndex != 0)
        {
            currentWeekIndex--;
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DateManagement : MonoBehaviour
{
    DateTime curDate;

    //launch date retrieved from PlayerPrefs and transformed back to DateTime
    DateTime savedLaunchDate;

    //how many days from launch date, launch date being 1
    int numberOfCurrentDay;

    //how many days from launch date, launch date being 0 ---for arrays
    int indexOfCurrentDay;
    
    //index of the current week
    int currentWeekIndex;

    //array of 7 calendar gameobjects 
    [SerializeField] TextMeshProUGUI[] daysArray7 = new TextMeshProUGUI[7];

    //array of 2 calendar gameobjects
    [SerializeField] TextMeshProUGUI[] daysArray2 = new TextMeshProUGUI[2];


    void Start()
    {
        Date();
        CurrentWeek(); //later this will be put on a button ---now its here for testing
    }
    
    //used to define the launch date and current date and save/get from PlayerPrefs
    void Date()
    {
        curDate = DateTime.Now.Date;

        //for now ---for testing (array index number - add 1)
        savedLaunchDate = curDate.AddDays(-28);


        //savedDate is got from PlayerPrefs ---to be used instead of savedLaunchDate

        //// try to get the launch date saved as a string:
        //string savedDateString = PlayerPrefs.GetString("savedLaunchDate", "");
        //if (savedDateString == "")
        //{ // if not saved yet...
        //  // convert current date to string...
        //    savedDateString = curDate.ToString();
        //    // and save it in PlayerPrefs as LaunchDate:
        //    PlayerPrefs.SetString("savedLaunchDate", savedDateString);
        //}
        //// at this point, the string savedDate contains the launch date
        //// let's convert it to DateTime:
        //DateTime savedDate;
        //DateTime.TryParse(savedDateString, out savedDate);
        
        indexOfCurrentDay = (curDate - savedLaunchDate).Days;
        numberOfCurrentDay = indexOfCurrentDay + 1;
        
    }
    
    public void CurrentWeek()
    {
        currentWeekIndex = indexOfCurrentDay / 7;       //floored number after division
        SetupBeforeCreateCalendar();
    }


    void SetupBeforeCreateCalendar()
    {
        if (currentWeekIndex % 4 == 0 && currentWeekIndex != 0) 
        {
            CreateCalendar(daysArray2);
        }
        else
        {
            CreateCalendar(daysArray7);
        } 
    }

    void CreateCalendar(TextMeshProUGUI[] arrayOfCalendarObjects)
    {
        for (int i = 0; i < arrayOfCalendarObjects.Length; i++)
        {
            var cur = arrayOfCalendarObjects[i];
            //Debug.Log((currentWeekIndex * 7) + i + 1);
            int dayInArr = (currentWeekIndex * 7) + i; //current day in loop - array number
            int dayGlobal = dayInArr + 1; //current day in loop - global number
            cur.text = dayGlobal.ToString();

            string[] stringValues = GetComponent<ValueManagement>().GetValuesOfIndex(dayInArr); //gets values for the current day in loop
            cur.GetComponentsInChildren<TextMeshProUGUI>()[1].text = stringValues[0]; //sets the first daily check
            cur.GetComponentsInChildren<TextMeshProUGUI>()[2].text = stringValues[1]; //sets the second daily check

            //if (stringValues[2] != "") //colors all survived days
            //{
            //    cur.color = new Color32(50, 50, 50, 255);
            //}

            if (dayGlobal == numberOfCurrentDay)
            {
                //indicate this is the current day
                cur.fontStyle = FontStyles.Underline;
            }
        }
    }

    //put these on buttons
    public void NextWeek()
    {
        currentWeekIndex++;
        SetupBeforeCreateCalendar();
    }

    public void LastWeek()
    {
        currentWeekIndex--;
        SetupBeforeCreateCalendar();
    }
}

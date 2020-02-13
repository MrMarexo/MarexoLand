using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateManagement : MonoBehaviour
{
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

    int curRunIndex = 0;


    //used to define the launch date and current date and save/get from PlayerPrefs
    void LoadDateOrSetDate(int index)
    {
        curRunIndex = index;

        curDate = DateTime.Now.Date;     

        // try to get the launch date saved as a string:
        string savedLaunchDateString = PlayerPrefs.GetString("savedLaunchDate" + curRunIndex.ToString(), "");
        if (savedLaunchDateString == "")
        { // if not saved yet...
          // convert current date to string...
            savedLaunchDateString = curDate.ToString();
            // and save it in PlayerPrefs as LaunchDate:
            PlayerPrefs.SetString("savedLaunchDate" + curRunIndex.ToString(), savedLaunchDateString);
        }
        // at this point, the string savedDate contains the launch date
        // let's convert it to DateTime:
        DateTime savedLaunchDate;
        DateTime.TryParse(savedLaunchDateString, out savedLaunchDate);

        //for testing (zero based)
        savedLaunchDate = savedLaunchDate.AddDays(-14);

        curDateIndex = (curDate - savedLaunchDate).Days;
        curDateNumber = curDateIndex + 1;

        currentWeekIndex = curDateIndex / 7;       //floored number after division
        originalCurrentWeekIndex = curDateIndex / 7;
    }

    public int GetCurrentDayIndex()
    {
        return curDateIndex;
    }
    
    public int GetCurrentWeek()
    {
        return currentWeekIndex;
    }

    
}

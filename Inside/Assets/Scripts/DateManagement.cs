using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateManagement : MonoBehaviour
{
    SceneLoader sL;
    ValueManagement vM;

    //current date
    DateTime curDate;

    //launch date retrieved from PlayerPrefs and transformed back to DateTime
    DateTime savedLaunchDate;

    string savedLaunchDateString;
    string savedLaunchDateStringForExport;

    //how many days from launch date, launch date being 0 ---zero based
    int curDateIndex;

    //index of the current week, can change to browse the calendar
    int currentWeekIndex;

    int curRunIndex = 0;

    float timeElapsed = 0;

    private void Start()
    {
        sL = GetComponent<SceneLoader>();
        vM = GetComponent<ValueManagement>();
    }

    private void Update()
    {
        Timer();
    }

    void Timer()
    {
        timeElapsed += Time.deltaTime;
        if (Convert.ToInt32(timeElapsed) == 30f)
        {
            UpdateCurDay();
        }
    }

    void UpdateCurDay()
    {
        int curI = sL.GetCurrentSceneIndex();
        string curN = sL.GetCurrentSceneName();
        if (curN == "Calendar" || curN == "Menu")
        {
            Debug.Log("curDay updated");
            LoadDateOrSetDate(vM.GetCurrentRunIndex());
            timeElapsed = 0;
        }
    }

    //used to define the launch date and current date and save/get from PlayerPrefs
    public void LoadDateOrSetDate(int index)
    {
        curRunIndex = index;

        curDate = DateTime.Now.Date;     

        // try to get the launch date saved as a string:
        savedLaunchDateString = PlayerPrefs.GetString("savedLaunchDate" + curRunIndex.ToString(), "");
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

        savedLaunchDateStringForExport = savedLaunchDate.ToShortDateString();


        //for testing (zero based)
        //savedLaunchDate = savedLaunchDate.AddDays(-14);

        curDateIndex = (curDate - savedLaunchDate).Days;

        currentWeekIndex = curDateIndex / 7;       //floored number after division
    }

    //set by the run manager for the Intro scenes --then after the Intro section finalizes the LoadDateOrSet will be run
    public void SetIndexes(int index)
    {
        curRunIndex = index;
        curDateIndex = 0;
        currentWeekIndex = 0;
    }

    //triggered by the last Intro scene
    public void SaveLaunchDateIntro()
    {
        LoadDateOrSetDate(curRunIndex);
        FindObjectOfType<ValueManagement>().SetRunDate(savedLaunchDateStringForExport, curRunIndex);
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

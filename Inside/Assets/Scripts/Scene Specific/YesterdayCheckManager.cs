﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesterdayCheckManager : MonoBehaviour
{
    ValueManagement vM;
    PopupManagement pM;
    SceneLoader sL;

    int dayIndex;

    string firstDailyCheck = "";
    string habitDailyCheck = "";

    //determines after how many weeks the second check will be added --will be later adjusted in the settings
    int daysToStartWithSecondCheck = 14;

    //counter to make sure popup only enables once
    int counter = 0;

    [SerializeField] GameObject popupCheck;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        pM = FindObjectOfType<PopupManagement>();
        dayIndex = FindObjectOfType<DateManagement>().GetCurrentDayIndex() - 1;
    }

    //methods on buttons to set a check reply to vMs arrays
    public void FirstCheckYes()
    {
        firstDailyCheck = OptionCodes.options[0];
    }

    public void FirstCheckNo()
    {
        firstDailyCheck = OptionCodes.options[1];
    }

    public void HabitCheckNo()
    {
        habitDailyCheck = OptionCodes.options[1];
    }

    public void HabitCheckYes()
    {
        habitDailyCheck = OptionCodes.options[0];
    }


    //if the answer is no on habit, opens the popup the first time to check if the answer was correctly input, then loads the next scene
    public void CheckAndLoadScene()
    {
        if (counter == 0)
        {
            if (habitDailyCheck == OptionCodes.options[1] || firstDailyCheck == OptionCodes.options[1])
            {
                pM.EnablePopup(popupCheck);
                counter++;
                return;
            }
        }
        //saves values and loads the next scene
        SaveValuesAndLoad();
    }

    public void ClosePopup()
    {
        pM.DisablePopup();
    }

    //called by the button in the popup
    public void LoadNextScene()
    {
        SaveValuesAndLoad();
    }

    void SaveValuesAndLoad()
    {
        //sets the default (for today it's empty string)
        if (habitDailyCheck == "")
        {
            habitDailyCheck = OptionCodes.options[0];
        }
        if (firstDailyCheck == "")
        {
            firstDailyCheck = OptionCodes.options[0];
        }

        //sets the results to playerprefs
        vM.SaveHabitAndFirstCheck(habitDailyCheck, firstDailyCheck, dayIndex);

        // + 1 for transfering idex into a number, since dayIndex is -1 this checks yesterday
        if (dayIndex + 1 > daysToStartWithSecondCheck && vM.GetValuesOfIndex(dayIndex)[1] == "")
        {
            FindObjectOfType<SceneLoader>().LoadSceneByName("Second Check Yesterday");
        }
        else
        {
            //loads todays check
            FindObjectOfType<SceneLoader>().LoadSceneByName("Today Check");
        }
        
    }
}
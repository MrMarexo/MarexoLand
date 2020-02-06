﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCheckYesterdayManager : MonoBehaviour
{
    ValueManagement vM;
    PopupManagement pM;
    SceneLoader sL;

    int dayIndex;

    string secondDailyCheck = "";

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
    public void SecondCheckYes()
    {
        secondDailyCheck = OptionCodes.options[0];
    }

    public void SecondCheckNo()
    {
        secondDailyCheck = OptionCodes.options[1];
    }

    //if the answer is no on habit, opens the popup the first time to check if the answer was correctly input, then loads the next scene
    public void CheckAndLoadScene()
    {
        if (counter == 0)
        {
            if (secondDailyCheck == OptionCodes.options[1])
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
        if (secondDailyCheck == "")
        {
            secondDailyCheck = OptionCodes.options[0];
        }
        //sets the results to playerprefs
        vM.SaveSecondCheck(secondDailyCheck, dayIndex);

        //loads todays check
        FindObjectOfType<SceneLoader>().LoadSceneByName("Today Check");
    }
}
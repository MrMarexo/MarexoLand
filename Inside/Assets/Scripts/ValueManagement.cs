using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ValueManagement : MonoBehaviour
{
    string[] firstDailyCheck = new string[30];
    string[] secondDailyCheck = new string[30];
    string[] habitDailyCheck = new string[30];

    
    string[] weeklyCheck = new string[5];

    //in case you'll wanna change the name of the task later --not configured yet
    string[] firstDailyName = new string[30]; 
    string[] secondDailyName = new string[30]; 

    string badHabitName;
    string firstCheckName;
    string secondCheckName;
    string weeklyCheckName;

    int currentDayIndex;

    private void Start()
    {
        //for testing purposes ---deletes all prefs for all scripts
        PlayerPrefs.DeleteAll(); 

        //getting all the prefs at the berginning of the game
        badHabitName = PlayerPrefs.GetString("badHabitName", "");
        firstCheckName = PlayerPrefs.GetString("firstCheckName", "");
        secondCheckName = PlayerPrefs.GetString("secondCheckName", "");
        weeklyCheckName = PlayerPrefs.GetString("weeklyCheckName", "");

        firstDailyCheck = PlayerPrefsX.GetStringArray("firstDailyCheck", "", 30);
        secondDailyCheck = PlayerPrefsX.GetStringArray("secondDailyCheck", "", 30);
        habitDailyCheck = PlayerPrefsX.GetStringArray("habitDailyCheck", "", 30);

        weeklyCheck = PlayerPrefsX.GetStringArray("weeklyCheck", "", 5);

        firstDailyName = PlayerPrefsX.GetStringArray("firstDailyName", "", 30);
        secondDailyName = PlayerPrefsX.GetStringArray("secondDailyName", "", 30);

        //get current day - zero based
        currentDayIndex = GetComponent<DateManagement>().GetCurrentDayIndex();

        //just for testing ---- fills up the calendar with successful previous days
        //////////////////////////////////////////////
        currentDayIndex = 7;
        for (int i = 0; i < currentDayIndex; i++)
        {
            firstDailyCheck[i] = OptionCodes.options[0];
            habitDailyCheck[i] = OptionCodes.options[0];
        }
        //weeklyCheck[0] = OptionCodes.options[0];
        //////////////////////////////////////////////////////
    }

    //called by the checkup scene script to set the chosen values to prefs
    public void SaveHabitAndFirstCheck(string habitResult, string firstResult, int index)
    {
        firstDailyCheck[index] = firstResult;
        habitDailyCheck[index] = habitResult;

        PlayerPrefsX.SetStringArray("firstDailyCheck", firstDailyCheck);
        PlayerPrefsX.SetStringArray("habitDailyCheck", habitDailyCheck);

        firstDailyCheck = PlayerPrefsX.GetStringArray("firstDailyCheck");
        habitDailyCheck = PlayerPrefsX.GetStringArray("habitDailyCheck");

        //foreach (string check in habitDailyCheck)
        //{
        //    Debug.Log(check);
        //}
    }

    //called by the checkup scene script to set the chosen values to prefs
    public void SaveWeeklyCheck(string result, int weekIndex)
    {
        weeklyCheck[weekIndex] = result;

        PlayerPrefsX.SetStringArray("weeklyCheck", weeklyCheck);
        weeklyCheck = PlayerPrefsX.GetStringArray("weeklyCheck");

        foreach (string check in weeklyCheck)
        {
            Debug.Log(check);
        }
    }

    //methods to save to playerprefs
    public void SaveBadHabit(string input)
    {
        PlayerPrefs.SetString("badHabitName", input);
        badHabitName = PlayerPrefs.GetString("badHabitName", "");
    }

    public void SaveFirstName(string input)
    {
        PlayerPrefs.SetString("firstCheckName", input);
        firstCheckName = PlayerPrefs.GetString("firstCheckName", "");
    }

    public void SaveSecondName(string input)
    {
        PlayerPrefs.SetString("secondCheckName", input);
        secondCheckName = PlayerPrefs.GetString("secondCheckName", "");
    }

    public void SaveWeeklyName(string input)
    {
        PlayerPrefs.SetString("weeklyCheckName", input);
        weeklyCheckName = PlayerPrefs.GetString("weeklyCheckName", "");
    }

    public string[] GetValuesOfIndex(int index)
    {
        //foreach (string habit in habitDailyCheck)
        //{
        //    Debug.Log(habit);
        //}
        string[] arrays = {firstDailyCheck[index], secondDailyCheck[index], habitDailyCheck[index]};
        return arrays;
    }

    public string[] GetWeeklyValues()
    {
        return weeklyCheck;
    }

    public string[] GetNames()
    {
        string[] names = {badHabitName, firstCheckName, weeklyCheckName, secondCheckName };
        return names;
    }

}

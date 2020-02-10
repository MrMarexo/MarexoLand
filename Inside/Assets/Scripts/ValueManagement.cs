using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueManagement : MonoBehaviour
{
    string[] firstDailyCheck = new string[30];
    string[] secondDailyCheck = new string[30];
    string[] habitDailyCheck = new string[30];



    string[] weeklyCheck = new string[5];

    string[] dayJournal = new string[30];
    string[] weekJournal = new string[5];

    //in case you'll wanna change the name of the task later --not configured yet
    string[] firstDailyName = new string[30]; 
    string[] secondDailyName = new string[30];
    string[] habitDailyName = new string[30];

    string[] weeklyName = new string[5];

    string playerName;
    string badHabitName;
    string firstCheckName;
    string secondCheckName;
    string weeklyCheckName;

    int currentDayIndex;
    int currentWeekIndex;

    private void Start()
    {
        //for testing purposes ---deletes all prefs for all scripts
        PlayerPrefs.DeleteAll(); 

        //getting all the prefs at the berginning of the game
        playerName = PlayerPrefs.GetString("playerName", "");
        badHabitName = PlayerPrefs.GetString("badHabitName", "");
        firstCheckName = PlayerPrefs.GetString("firstCheckName", "");
        secondCheckName = PlayerPrefs.GetString("secondCheckName", "");
        weeklyCheckName = PlayerPrefs.GetString("weeklyCheckName", "");

        firstDailyCheck = PlayerPrefsX.GetStringArray("firstDailyCheck", "", 30);
        secondDailyCheck = PlayerPrefsX.GetStringArray("secondDailyCheck", "", 30);
        habitDailyCheck = PlayerPrefsX.GetStringArray("habitDailyCheck", "", 30);

        dayJournal = PlayerPrefsX.GetStringArray("dayJournal", "", 30);
        weekJournal = PlayerPrefsX.GetStringArray("weekJournal", "", 5);

        weeklyCheck = PlayerPrefsX.GetStringArray("weeklyCheck", "", 5);

        habitDailyName = PlayerPrefsX.GetStringArray("habitDailyName", "", 30);
        firstDailyName = PlayerPrefsX.GetStringArray("firstDailyName", "", 30);
        secondDailyName = PlayerPrefsX.GetStringArray("secondDailyName", "", 30);

        weeklyName = PlayerPrefsX.GetStringArray("weeklyName", "", 5);

        //get current day - zero based
        currentDayIndex = GetComponent<DateManagement>().GetCurrentDayIndex();

        currentWeekIndex = GetComponent<DateManagement>().GetCurrentWeek();

        //////////////////////////////////////////////////////
        //just for testing ---- fills up the calendar with successful previous days

        for (int i = 0; i < currentDayIndex -1; i++)
        {
            firstDailyCheck[i] = OptionCodes.options[0];
            habitDailyCheck[i] = OptionCodes.options[0];
            habitDailyName[i] = "lala";
            firstDailyName[i] = "lala";
        }
        weeklyCheck[0] = OptionCodes.options[0];
        //weeklyCheck[1] = OptionCodes.options[1];

        weeklyName[0] = "lala";
        //weeklyName[1] = "lala";

        playerName = "lala";
        badHabitName = "lala";
        firstCheckName = "lala";
        weeklyCheckName = "lala";
        //secondCheckName = "lala";

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

        

        //saving the names as well
        habitDailyName[index] = badHabitName;
        firstDailyName[index] = firstCheckName;

        PlayerPrefsX.SetStringArray("habitDailyName", habitDailyName);
        PlayerPrefsX.SetStringArray("firstDailyName", firstDailyName);

        habitDailyName = PlayerPrefsX.GetStringArray("habitDailyName");
        firstDailyName = PlayerPrefsX.GetStringArray("firstDailyName"); 
    }

    //called by the checkup scene script to set the chosen values to prefs
    public void SaveWeeklyCheck(string result, int weekIndex)
    {
        weeklyCheck[weekIndex] = result;

        PlayerPrefsX.SetStringArray("weeklyCheck", weeklyCheck);
        weeklyCheck = PlayerPrefsX.GetStringArray("weeklyCheck");

    }

    //called by secondcheckmanager to save the check results
    public void SaveSecondCheck(string result, int index)
    {
        secondDailyCheck[index] = result;

        PlayerPrefsX.SetStringArray("secondDailyCheck", secondDailyCheck);
        secondDailyCheck = PlayerPrefsX.GetStringArray("secondDailyCheck");

        //saving the name of the check as well
        secondDailyName[index] = secondCheckName;

        PlayerPrefsX.SetStringArray("secondDailyName", secondDailyName);
        secondDailyName = PlayerPrefsX.GetStringArray("secondDailyName");
    }

    public void SaveDayJournal(string result, int dayIndex)
    {
        dayJournal[dayIndex] = result;

        PlayerPrefsX.SetStringArray("dayJournal", dayJournal);
        dayJournal = PlayerPrefsX.GetStringArray("dayJournal");
    }

    public void SaveWeekJournal(string result, int weekIndex)
    {
        weekJournal[weekIndex] = result;

        PlayerPrefsX.SetStringArray("weekJournal", weekJournal);
        weekJournal = PlayerPrefsX.GetStringArray("weekJournal");
    }

    //methods to save to playerprefs
    public void SaveBadHabit(string input)
    {
        PlayerPrefs.SetString("badHabitName", input);
        badHabitName = PlayerPrefs.GetString("badHabitName", "");
    }

    public void SavePlayerName(string name)
    {
        PlayerPrefs.SetString("playerName", name);
        playerName = PlayerPrefs.GetString("playerName", "");
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

        weeklyName[currentWeekIndex] = weeklyCheckName;

        PlayerPrefsX.SetStringArray("weeklyName", weeklyName);
        weeklyName = PlayerPrefsX.GetStringArray("weeklyName");
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

    public string GetDayJournalOfIndex(int dayIndex)
    {
        return dayJournal[dayIndex];
    }

    public string GetWeekJournalOfIndex(int weekIndex)
    {
        return weekJournal[weekIndex];
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

    public string GetWeeklyName(int weekIndex)
    {
        return weeklyName[weekIndex];
    }

    public string[] GetNamesOfDayIndex(int index)
    {
        string[] names = { firstDailyName[index], secondDailyName[index], habitDailyName[index] };
        return names;
    }
}

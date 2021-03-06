using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueManagement : MonoBehaviour
{
    //5 is the limit for runs on one device
    string[] runDates = new string[5];
    int curRunIndex = 0;
    bool[] areRunsFinished = new bool[5];

    int dayLength = 30;
    int weekLength = 5;

    //common for all runs once set
    string playerName;
    string gender;


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

    string badHabitName;
    string firstCheckName;
    string secondCheckName;
    string weeklyCheckName;

    string goodFuture;
    string badFuture;

    private void Awake()
    {
        ///////////////////////for testing purposes ---deletes all prefs for all scripts
        PlayerPrefs.DeleteAll();
        ///////////////////////

        DeleteGamePrefs();

        //get Run data from prefs
        runDates = PlayerPrefsX.GetStringArray("runDates", "", 5);
        areRunsFinished = PlayerPrefsX.GetBoolArray("areRunsFinished", false, 5);
    }

    //run by the RunManager
    public void SetRunIndexForIntro(int index)
    {
        curRunIndex = index;
    }

    public string[] GetRunDates()
    {
        return runDates;
    }

    public bool[] GetRunsFinishedState()
    {
        return areRunsFinished;
    }

    public string GetBadHabitNameFromRun(int runIndex)
    {
        return PlayerPrefs.GetString("badHabitName" + runIndex.ToString(), "");
    }

    public void LoadCurrentValuesFromPrefs(int index)
    {
        curRunIndex = index;

        //getting all the prefs at the berginning of the game
        playerName = PlayerPrefs.GetString("playerName", "");
        gender = PlayerPrefs.GetString("gender", "");
        goodFuture = PlayerPrefs.GetString("goodFuture", "");
        badFuture = PlayerPrefs.GetString("badFuture", "");

        badHabitName = PlayerPrefs.GetString("badHabitName" + curRunIndex.ToString(), "");
        firstCheckName = PlayerPrefs.GetString("firstCheckName" + curRunIndex.ToString(), "");
        secondCheckName = PlayerPrefs.GetString("secondCheckName" + curRunIndex.ToString(), "");
        weeklyCheckName = PlayerPrefs.GetString("weeklyCheckName" + curRunIndex.ToString(), "");

        firstDailyCheck = PlayerPrefsX.GetStringArray("firstDailyCheck" + curRunIndex.ToString(), "", 30);
        secondDailyCheck = PlayerPrefsX.GetStringArray("secondDailyCheck" + curRunIndex.ToString(), "", 30);
        habitDailyCheck = PlayerPrefsX.GetStringArray("habitDailyCheck" + curRunIndex.ToString(), "", 30);

        dayJournal = PlayerPrefsX.GetStringArray("dayJournal" + curRunIndex.ToString(), "", 30);
        weekJournal = PlayerPrefsX.GetStringArray("weekJournal" + curRunIndex.ToString(), "", 5);

        weeklyCheck = PlayerPrefsX.GetStringArray("weeklyCheck" + curRunIndex.ToString(), "", 5);

        habitDailyName = PlayerPrefsX.GetStringArray("habitDailyName" + curRunIndex.ToString(), "", 30);
        firstDailyName = PlayerPrefsX.GetStringArray("firstDailyName" + curRunIndex.ToString(), "", 30);
        secondDailyName = PlayerPrefsX.GetStringArray("secondDailyName" + curRunIndex.ToString(), "", 30);

        weeklyName = PlayerPrefsX.GetStringArray("weeklyName" + curRunIndex.ToString(), "", 5);

        //////////////////////////////////////////////////////
        //just for testing ---- fills up the calendar with successful previous days
        //int currentDayIndex = GetComponent<DateManagement>().GetCurrentDayIndex();

        //int currentWeekIndex = GetComponent<DateManagement>().GetCurrentWeek();

        //for (int i = 0; i < currentDayIndex - 1; i++)
        //{
        //    firstDailyCheck[i] = OptionCodes.options[0];
        //    habitDailyCheck[i] = OptionCodes.options[0];
        //    habitDailyName[i] = "lala";
        //    firstDailyName[i] = "lala";
        //}
        //weeklyCheck[0] = OptionCodes.options[0];
        ////weeklyCheck[1] = OptionCodes.options[1];

        //weeklyName[0] = "lala";
        ////weeklyName[1] = "lala";

        //playerName = "lala";
        ////badHabitName = "lala";
        //firstCheckName = "lala";
        //weeklyCheckName = "lala";
        //secondCheckName = "lala";

        //////////////////////////////////////////////////////

    }

    //called by the checkup scene script to set the chosen values to prefs
    public void SaveHabitCheck(string habitResult, int index)
    {
        habitDailyCheck[index] = habitResult;
        PlayerPrefsX.SetStringArray("habitDailyCheck" + curRunIndex.ToString(), habitDailyCheck);
    }

    public void SaveFirstCheck(string firstResult, int index)
    {
        firstDailyCheck[index] = firstResult;
        PlayerPrefsX.SetStringArray("firstDailyCheck" + curRunIndex.ToString(), firstDailyCheck);
    }

    public void SaveSecondCheck(string result, int index)
    {
        secondDailyCheck[index] = result;
        PlayerPrefsX.SetStringArray("secondDailyCheck" + curRunIndex.ToString(), secondDailyCheck);
    }

    public void SaveDailyNamesForTomorrow(int todayIndex)
    {
        //saving the names as well
        habitDailyName[todayIndex + 1] = badHabitName;
        firstDailyName[todayIndex + 1] = firstCheckName;
        secondDailyName[todayIndex + 1] = secondCheckName;

        PlayerPrefsX.SetStringArray("habitDailyName" + curRunIndex.ToString(), habitDailyName);
        PlayerPrefsX.SetStringArray("firstDailyName" + curRunIndex.ToString(), firstDailyName);
        PlayerPrefsX.SetStringArray("secondDailyName" + curRunIndex.ToString(), secondDailyName);
    }

    //called by the checkup scene script to set the chosen values to prefs
    public void SaveWeeklyCheck(string result, int weekIndex)
    {
        weeklyCheck[weekIndex] = result;

        PlayerPrefsX.SetStringArray("weeklyCheck" + curRunIndex.ToString(), weeklyCheck);
    }
    

    public void SaveDayJournal(string result, int dayIndex)
    {
        dayJournal[dayIndex] = result;

        PlayerPrefsX.SetStringArray("dayJournal" + curRunIndex.ToString(), dayJournal);
    }

    public void SaveWeekJournal(string result, int weekIndex)
    {
        weekJournal[weekIndex] = result;

        PlayerPrefsX.SetStringArray("weekJournal" + curRunIndex.ToString(), weekJournal);
    }

    //methods from Intro Scenes
    public void SaveBadHabit(string input)
    {
        badHabitName = input;
    }

    public void SavePlayerName(string name)
    {
        playerName = name;
    }

    public void SaveGernder(string input)
    {
        gender = input;
    }

    public void SaveFirstName(string input)
    {
        firstCheckName = input;
    }

    public void SaveGoodFuture(string input)
    {
        goodFuture = input;
    }

    public void SaveBadFuture(string input)
    {
        badFuture = input;
    }


    //triggered in the last Intro scene to set up the normal game process
    public void SaveIntroValues()
    {
        string genderCache = gender;
        string badHabitCache = badHabitName;
        string playerNameCache = playerName;
        string firstCheckCache = firstCheckName;
        string goodFutureCache = goodFuture;
        string badFutureCache = badFuture;

        //introduce and fill up all the arrays and other variables with empty values
        LoadCurrentValuesFromPrefs(curRunIndex);

        gender = genderCache;
        badHabitName = badHabitCache;
        playerName = playerNameCache;
        firstCheckName = firstCheckCache;
        goodFuture = goodFutureCache;
        badFuture = badFutureCache;

        //save intro values to playerprefs

        PlayerPrefs.SetString("badHabitName" + curRunIndex.ToString(), badHabitName);
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetString("gender", gender);
        PlayerPrefs.SetString("firstCheckName" + curRunIndex.ToString(), firstCheckName);
        PlayerPrefs.SetString("goodFuture", goodFuture);
        PlayerPrefs.SetString("badFuture", badFuture);
    

        //save names into name arrays - 0 index
        habitDailyName[0] = badHabitName;
        firstDailyName[0] = firstCheckName;
        secondDailyName[0] = secondCheckName;
        weeklyName[0] = weeklyCheckName;

        PlayerPrefsX.SetStringArray("habitDailyName" + curRunIndex.ToString(), habitDailyName);
        PlayerPrefsX.SetStringArray("firstDailyName" + curRunIndex.ToString(), firstDailyName);
        PlayerPrefsX.SetStringArray("secondDailyName" + curRunIndex.ToString(), secondDailyName);
        PlayerPrefsX.SetStringArray("weeklyName" + curRunIndex.ToString(), weeklyName);

    }

    //by dM to save the date at the end of Intro scenes
    public void SetRunDate(string runDateValue, int runIndex)
    {
        runDates[runIndex] = runDateValue;
        PlayerPrefsX.SetStringArray("runDates", runDates);
    }

    //this is a special intro method because its also used outside of intro
    public void SaveWeeklyName(string input, int weekIndex, bool isIntro)
    {
        weeklyCheckName = input;
        PlayerPrefs.SetString("weeklyCheckName" + curRunIndex.ToString(), input);

        if (!isIntro)
        {
            weeklyName[weekIndex] = weeklyCheckName;
            PlayerPrefsX.SetStringArray("weeklyName" + curRunIndex.ToString(), weeklyName);
        }
    }

    

    public void SaveSecondName(string input)
    {
        secondCheckName = input;
        PlayerPrefs.SetString("secondCheckName" + curRunIndex.ToString(), secondCheckName);

        secondDailyName[14] = secondCheckName;
        PlayerPrefsX.GetStringArray("secondDailyName" + curRunIndex.ToString(), "", 30);

    }

    public string[] GetValuesOfIndex(int index)
    {
        //foreach (string habit in habitDailyCheck)
        //{
        //    Debug.Log(habit);
        //}
        string[] arrays = { firstDailyCheck[index], secondDailyCheck[index], habitDailyCheck[index] };
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
        string[] names = { badHabitName, firstCheckName, weeklyCheckName, secondCheckName };
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

    public int GetDayLength()
    {
        return dayLength;
    }

    public int GetWeekLength()
    {
        return weekLength;
    }

    public int GetCurrentRunIndex()
    {
        return curRunIndex;
    }

    //for game 
    void DeleteGamePrefs() 
    {
        PlayerPrefs.DeleteKey("gotKeyCheckpoint");

        PlayerPrefs.DeleteKey("mpName");

        PlayerPrefs.DeleteKey("deathPosX");
        PlayerPrefs.DeleteKey("deathPosY");
        PlayerPrefs.DeleteKey("deathPosZ");

        PlayerPrefs.DeleteKey("checkpointPosX");
        PlayerPrefs.DeleteKey("checkpointPosY");
        PlayerPrefs.DeleteKey("checkpointPosZ");
    }

    //cache for the level
    public void SaveBoughtCheckpointsForLevel(int number)
    {
        PlayerPrefs.SetInt("checkpointsBought", number);
    }

    public int GetBoughtCheckpoints()
    {
        return PlayerPrefs.GetInt("checkpointsBought");
    }

    public void SaveBoughtInsteadsForLevel(int number)
    {
        PlayerPrefs.SetInt("insteadsBought", number);
    }

    public int GetBoughtInsteads()
    {
        return PlayerPrefs.GetInt("insteadsBought");
    }

    public void SaveBoughtSlowdownsForLevel(int number)
    {
        PlayerPrefs.SetInt("slowdownsBought", number);
    }

    public int GetBoughtSlowdowns()
    {
        return PlayerPrefs.GetInt("slowdownsBought");
    }

}

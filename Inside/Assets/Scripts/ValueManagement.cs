using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ValueManagement : MonoBehaviour
{
    //colors
    [SerializeField] Color32 completeColor = new Color32(255, 255, 255, 255);
    [SerializeField] Color32 incompleteColor = new Color32(0, 0, 0, 255);

    string[] firstDailyCheck = new string[30];
    string[] secondDailyCheck = new string[30];
    string[] habitDailyCheck = new string[30];

    string[] firstDailyName = new string[30];
    string[] secondDailyName = new string[30];

    string badHabitName;
    string firstCheckName;
    string secondCheckName;
    string weeklyCheckName;

    int currentDayIndex;


    string[] options = { "S", "F", "" };

    [SerializeField] TMP_InputField inputHabit;
    [SerializeField] TMP_InputField inputFirst;
    [SerializeField] TMP_InputField inputSecond;
    [SerializeField] TMP_InputField inputWeekly;
    
    private void Awake()
    {
        PlayerPrefs.DeleteAll(); //for testing purposes ---deletes all prefs for all scripts

        //getting all the prefs at the berginning of the game
        badHabitName = PlayerPrefs.GetString("badHabitName", "");
        firstCheckName = PlayerPrefs.GetString("firstCheckName", "");
        secondCheckName = PlayerPrefs.GetString("secondCheckName", "");
        weeklyCheckName = PlayerPrefs.GetString("weeklyCheckName", "");

        firstDailyCheck = PlayerPrefsX.GetStringArray("firstDailyCheck", "", 30);
        secondDailyCheck = PlayerPrefsX.GetStringArray("secondDailyCheck", "", 30);
        habitDailyCheck = PlayerPrefsX.GetStringArray("habitDailyCheck", "", 30);
        firstDailyName = PlayerPrefsX.GetStringArray("firstDailyName", "", 30);
        secondDailyName = PlayerPrefsX.GetStringArray("secondDailyName", "", 30);
    }

    private void Start()
    {
        //get current day - zero based
        currentDayIndex = GetComponent<DateManagement>().GetCurrentDayIndex();

        //just for testing ---- fills up the calendar with successful previous days
        for (int i = 0; i < currentDayIndex; i++)
        {
            firstDailyCheck[i] = options[0];
            habitDailyCheck[i] = options[0];
        }

    }

    //toggles between two buttons
    void ToggleTwo(int activeIndex, GameObject parentButton)
    {
        int inactiveIndex = Mathf.Abs(activeIndex - 1);
        parentButton.GetComponentsInChildren<TextMeshProUGUI>()[inactiveIndex].color = incompleteColor;
        parentButton.GetComponentsInChildren<TextMeshProUGUI>()[activeIndex].color = completeColor;
    }

    public void FirstButton()
    {
        GameObject parentOfButton = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        ToggleTwo(0, parentOfButton);
    }

    public void SecondButton()
    {
        GameObject parentOfButton = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        ToggleTwo(1, parentOfButton);
    }

    //methods on buttons to set a check reply
    public void FirstCheckYes()
    {
        firstDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[0]; //checks if it should write to yesterdays or tomorrows values
    }

    public void FirstCheckNo()
    {
        firstDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[1];
    }

    public void FirstCheckMaybe()
    {
        firstDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[2];
    }

    public void HabitCheckYes()
    {
        habitDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[0];
    }

    public void HabitCheckNo()
    {
        habitDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[1];
    }

    public void HabitCheckMaybe()
    {
        habitDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[2];
    }

    public void SecondCheckYes()
    {
        secondDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[0];
    }

    public void SecondCheckNo()
    {
        secondDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[1];
    }

    public void SecondCheckMaybe()
    {
        secondDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[2];
    }

    public void SubmitCheck()
    {
        //sets default values --- will add second check later
        if (CheckValuesForDailyCheck() == 0) //in todays check default values are empty strings = not yet options
        {
            if (firstDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] == "")
            {
                firstDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[2];
            }
            if (habitDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] == "")
            {
                habitDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[2];
            }
        }
        else //in yesterdays check default values are success
        {
            if (firstDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] == "")
            {
                firstDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[0];
            }
            if (habitDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] == "")
            {
                habitDailyCheck[currentDayIndex - CheckValuesForDailyCheck()] = options[0];
            }
        }

        PlayerPrefsX.SetStringArray("firstDailyCheck", firstDailyCheck);
        PlayerPrefsX.SetStringArray("secondDailyCheck", secondDailyCheck);
        PlayerPrefsX.SetStringArray("habitDailyCheck", habitDailyCheck);

        firstDailyCheck = PlayerPrefsX.GetStringArray("firstDailyCheck");
        secondDailyCheck = PlayerPrefsX.GetStringArray("secondDailyCheck");
        habitDailyCheck = PlayerPrefsX.GetStringArray("habitDailyCheck");

    }


    //methods to save to playerprefs
    public void SaveBadHabit()
    {
        string inputBadHabit = inputHabit.text;
        PlayerPrefs.SetString("badHabitName", inputBadHabit);
        badHabitName = PlayerPrefs.GetString("badHabitName", "");
    }

    public void SaveFirstName()
    {
        string inputFirstName = inputFirst.text;
        PlayerPrefs.SetString("firstCheckName", inputFirstName);
        firstCheckName = PlayerPrefs.GetString("firstCheckName", "");
    }

    public void SaveSecondName()
    {
        string inputSecondName = inputSecond.text;
        PlayerPrefs.SetString("secondCheckName", inputSecondName);
        secondCheckName = PlayerPrefs.GetString("secondCheckName", "");
    }

    public void SaveWeeklyName()
    {
        string inputWeeklyName = inputWeekly.text;
        PlayerPrefs.SetString("weeklyCheckName", inputWeeklyName);
        weeklyCheckName = PlayerPrefs.GetString("weeklyCheckName", "");
    }

    public string[] GetValuesOfIndex(int index)
    {
        string[] arrays = {firstDailyCheck[index], secondDailyCheck[index], habitDailyCheck[index]};
        return arrays;
    }


    public string[] GetNames()
    {
        string[] names = {badHabitName, firstCheckName, weeklyCheckName, secondCheckName };
        return names;
    }
    
    //checks if yesterdays all todays values will be adjusted in the current daily check
    public int CheckValuesForDailyCheck()
    {
        //if (all yesterdays values have not been set)
        // load yesterdays check  --- if (no yesterday)
        //load todays check
        // else if (all todays values have nott been set)
        //load todays check
        int yesterdayIndex = currentDayIndex - 1;
        int indexToReturn;

        if (yesterdayIndex == -1) //if this is the first day
        {
            indexToReturn = 0; //today
        }
        if (firstDailyCheck[yesterdayIndex] == "" || habitDailyCheck[yesterdayIndex] == "") //add second check later
        {
            indexToReturn = 1; //yesterday
        }
        else
        {
            indexToReturn = 0; //today
        }
        return indexToReturn;
    }
}

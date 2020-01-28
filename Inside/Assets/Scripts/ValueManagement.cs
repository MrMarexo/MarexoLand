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

    string[] firstdailyName = new string[30];
    string[] secondDailyName = new string[30];

    string badHabitName;
    string firstCheckName;
    string secondCheckName;
    string weeklyCheckName;

    [SerializeField] TMP_InputField inputHabit;
    [SerializeField] TMP_InputField inputFirst;
    [SerializeField] TMP_InputField inputSecond;
    [SerializeField] TMP_InputField inputWeekly;

    private void Awake()
    {
        PlayerPrefs.DeleteAll(); //for testing purposes
        badHabitName = PlayerPrefs.GetString("badHabitName", "");
        firstCheckName = PlayerPrefs.GetString("firstCheckName", "");
        secondCheckName = PlayerPrefs.GetString("secondCheckName", "");
        weeklyCheckName = PlayerPrefs.GetString("weeklyCheckName", "");
    }

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
    
    
}

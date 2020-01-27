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

    string badHabit;
    string firstCheck;
    string secondCheck;

    [SerializeField] TMP_InputField inputHabit;

    public void SetBadHabit()
    {
        badHabit = inputHabit.text;
    }

    public string[] GetValuesOfIndex(int index)
    {
        string[] arrays = {firstDailyCheck[index], secondDailyCheck[index], habitDailyCheck[index]};
        return arrays;
    }

}

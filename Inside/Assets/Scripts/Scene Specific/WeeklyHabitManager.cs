using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeeklyHabitManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject writePopup;

    DateManagement dM;
    ValueManagement vM;

    int curWeekIndex;

    private void Start()
    {
        dM = FindObjectOfType<DateManagement>();
        vM = FindObjectOfType<ValueManagement>();

        curWeekIndex = dM.GetCurrentWeek();
    }

    public void SaveAndLoadNextIf()
    {
        string input = inputField.text;
        //check if text is not ""
        if (string.IsNullOrEmpty(input))
        {
            //enable a pop-up canvas that prompts the user to input sth and return
            FindObjectOfType<PopupManagement>().EnablePopup(writePopup);
            return;
        }
        else
        {
            vM.SaveWeeklyName(input, curWeekIndex);
            FindObjectOfType<SceneLoader>().LoadNextScene();
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }
    
}

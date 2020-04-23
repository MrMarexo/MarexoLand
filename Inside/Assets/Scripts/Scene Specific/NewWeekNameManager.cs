using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewWeekNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject writePopup;

    DateManagement dM;

    int curWeekIndex;

    private void Start()
    {
        dM = FindObjectOfType<DateManagement>();
        curWeekIndex = dM.GetCurrentWeek();
    }

    public void SaveAndLoadNextIf()
    {
        string input = inputField.text;
        //check if text is not ""
        if (input == "")
        {
            //enable a pop-up canvas that prompts the user to input sth and return
            FindObjectOfType<PopupManagement>().EnablePopup(writePopup);
            return;
        }
        else
        {
            FindObjectOfType<ValueManagement>().SaveWeeklyName(input, curWeekIndex, true);
            FindObjectOfType<SceneLoader>().LoadSceneByName("Check");
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }
}

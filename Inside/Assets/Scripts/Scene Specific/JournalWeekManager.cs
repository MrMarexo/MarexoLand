using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalWeekManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject journalPopup;

    int curWeekIndex;

    ValueManagement vM;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        curWeekIndex = FindObjectOfType<DateManagement>().GetCurrentWeek();
        if (vM.GetDayJournalOfIndex(curWeekIndex - 1) != "")
        {
            inputField.text = vM.GetWeekJournalOfIndex(curWeekIndex);
        }

    }

    public void SaveAndLoadNextIf()
    {
        string input = inputField.text;
        FindObjectOfType<ValueManagement>().SaveWeekJournal(input, curWeekIndex);

        //check if text is not ""
        if (input != "")
        {
            //enable a pop-up canvas that prompts the user to input sth and return
            FindObjectOfType<PopupManagement>().EnablePopup(journalPopup);
            return;
        }
        else
        {
            NextScene();
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }

    public void NextScene()
    {
        FindObjectOfType<SceneLoader>().LoadSceneByName("Calendar");
    }
}

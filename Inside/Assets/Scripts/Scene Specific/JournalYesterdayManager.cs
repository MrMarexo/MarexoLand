using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class JournalYesterdayManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject journalPopup;

    int curDay;

    ValueManagement vM;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        curDay = FindObjectOfType<DateManagement>().GetCurrentDayIndex();
        if (vM.GetDayJournalOfIndex(curDay - 1) != "")
        {
            inputField.text = vM.GetDayJournalOfIndex(curDay - 1);
        }   
    }

    public void SaveAndLoadNextIf()
    {
        string input = inputField.text;
        FindObjectOfType<ValueManagement>().SaveDayJournal(input, curDay - 1);

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
        FindObjectOfType<SceneLoader>().LoadSceneByName("Today Check");
    }
}

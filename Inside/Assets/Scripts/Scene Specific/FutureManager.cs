using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FutureManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject writePopup;

    DateManagement dM;
    ValueManagement vM;
    SceneLoader sL;

    int curWeekIndex;

    private void Start()
    {
        dM = FindObjectOfType<DateManagement>();
        vM = FindObjectOfType<ValueManagement>();
        sL = FindObjectOfType<SceneLoader>();

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
            if (sL.GetCurrentSceneName() == "Good Future")
            {
                vM.SaveGoodFuture(input);
                sL.LoadNextScene();
            }
            else
            {
                vM.SaveBadFuture(input);
                sL.LoadSceneByName("Check");
            }
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }
    
}

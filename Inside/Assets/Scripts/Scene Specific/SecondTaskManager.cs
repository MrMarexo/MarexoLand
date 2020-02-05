using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondTaskManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject writePopup;

    string secondDailyCheck = "";

    //methods on buttons to set a check reply to vMs arrays
    public void SecondCheckYes()
    {
        secondDailyCheck = OptionCodes.options[0];
    }

    public void SecondCheckMaybe()
    {
        secondDailyCheck = OptionCodes.options[2];
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
            var vM = FindObjectOfType<ValueManagement>();
            //sets the results to playerprefs
            vM.SaveSecondName(input);
            vM.SaveSecondCheck(secondDailyCheck, FindObjectOfType<DateManagement>().GetCurrentDayIndex());

            FindObjectOfType<SceneLoader>().LoadSceneByName("After Check");
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }
}

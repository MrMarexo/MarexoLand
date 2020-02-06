using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewWeekNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject writePopup;


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
            FindObjectOfType<ValueManagement>().SaveWeeklyName(input);
            FindObjectOfType<SceneLoader>().LoadSceneByName("Yesterday Check");
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }
}

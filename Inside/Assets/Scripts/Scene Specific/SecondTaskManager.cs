using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondTaskManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    [SerializeField] GameObject writePopup;

    
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
            var vM = FindObjectOfType<ValueManagement>();
            //sets the results to playerprefs
            vM.SaveSecondName(input);
            FindObjectOfType<SceneLoader>().LoadSceneByName("Calendar");
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }
}

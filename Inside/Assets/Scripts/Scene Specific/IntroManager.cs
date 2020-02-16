using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class IntroManager : MonoBehaviour
{
    string gender = "male";
    string playerName;

    int counter = 0;

    [SerializeField] TMP_InputField nameInput;

    [SerializeField] GameObject namePopup;


    public void Male()
    {
        gender = "male";
    }

    public void Female()
    {
        gender = "female";
    }

    public void CheckAndLoadPopup()
    {
        playerName = nameInput.text;
        //check if text is not ""
        if (playerName == "" && counter == 0)
        {
            //enable a pop-up canvas that prompts the user to input sth and return
            FindObjectOfType<PopupManagement>().EnablePopup(namePopup);
            counter++;
            return;
        }
        else
        {
            SaveValuesAndLoadNext();
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }

    //saves values to prefs and loads next scene
    void SaveValuesAndLoadNext()
    {
        var vM = FindObjectOfType<ValueManagement>();
        vM.SaveGernder(gender);
        vM.SavePlayerName(playerName);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }

}      

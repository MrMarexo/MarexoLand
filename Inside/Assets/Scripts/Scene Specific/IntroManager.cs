using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class IntroManager : MonoBehaviour
{
    string habit;
    string playerName;

    int counter = 0;

    [SerializeField] TMP_InputField habitInput;
    [SerializeField] TMP_InputField nameInput;


    [SerializeField] GameObject writePopup;
    [SerializeField] GameObject namePopup;


    public void CheckAndLoadPopup()
    {
        habit = habitInput.text;
        playerName = nameInput.text;
        //check if text is not ""
        if (playerName == "" && counter == 0)
        {
            //enable a pop-up canvas that prompts the user to input sth and return
            FindObjectOfType<PopupManagement>().EnablePopup(namePopup);
            counter++;
            return;
        }
        else if (habit == "")
        {
            //enable a pop-up canvas that prompts the user to input sth and return
            FindObjectOfType<PopupManagement>().EnablePopup(writePopup);
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
        vM.SavePlayerName(playerName);
        vM.SaveBadHabit(habit);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }

}      

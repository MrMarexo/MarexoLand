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

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        sL = FindObjectOfType<SceneLoader>();
        dM = FindObjectOfType<DateManagement>();
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
                vM.SaveIntroValues();
                dM.SaveLaunchDateIntro();
                sL.LoadNextScene();
            }
        }
    }

    public void DisablePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }
    
}

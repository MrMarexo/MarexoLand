using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCheckTodayManager : MonoBehaviour
{
    ValueManagement vM;

    int currentDayIndex;

    string secondDailyCheck = "";

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        currentDayIndex = FindObjectOfType<DateManagement>().GetCurrentDayIndex();
    }

    //methods on buttons to set a check reply to vMs arrays
    public void FirstCheckYes()
    {
        secondDailyCheck = OptionCodes.options[0];
    }

    public void FirstCheckMaybe()
    {
        secondDailyCheck = OptionCodes.options[2];
    }

    //called by the button in the popup
    public void LoadNextScene()
    {
        SaveValuesAndLoad();
    }

    void SaveValuesAndLoad()
    {
        //sets the results to playerprefs
        vM.SaveSecondCheck(secondDailyCheck, currentDayIndex);

        //loads the next scene
        FindObjectOfType<SceneLoader>().LoadSceneByName("Calendar");
    }
}

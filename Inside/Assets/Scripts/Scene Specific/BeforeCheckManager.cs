using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeforeCheckManager : MonoBehaviour
{
    ValueManagement vM;
    PopupManagement pM;
    SceneLoader sL;
    DateManagement dM;

    int currentDayIndex;
    int currentWeekIndex;

    string weeklyCheck = "";

    //counter to make sure popup only enables once
    int counter = 0;

    //determines after how many weeks the second check will be added --will be later adjusted in the settings
    int daysToStartWithSecondCheck = 14;


    [SerializeField] GameObject popupCheck;

    //0 is precheck, 1 is serious check
    [SerializeField] GameObject[] checkObjects = new GameObject[2];
    [SerializeField] TextMeshProUGUI[] weekNames = new TextMeshProUGUI[2];
    
 
    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        pM = FindObjectOfType<PopupManagement>();
        dM = FindObjectOfType<DateManagement>();
        currentDayIndex = dM.GetCurrentDayIndex();
        currentWeekIndex = dM.GetCurrentWeek();
        LoadButtons();
    }

    //methods on buttons to set a check reply to vMs arrays
    public void WeeklyCheckYes()
    {
        weeklyCheck = OptionCodes.options[0];
    }

    public void WeeklyCheckMaybe()
    {
        weeklyCheck = OptionCodes.options[2];
    }

    public void WeeklyCheckNo()
    {
        weeklyCheck = OptionCodes.options[1];
    }

    //returns 1 if this check is for last week and should be saved there 
    int ShouldSaveToPreviousWeek()
    {
        if (currentWeekIndex > 0)
        {
            string lastWeekCheck = vM.GetWeeklyValues()[currentWeekIndex - 1];
            if (string.IsNullOrEmpty(lastWeekCheck) && (currentDayIndex % 7 == 0 || currentDayIndex == 29))
            {
                return 1;
            }
        }
        return 0;
    }

    //2 show nothing, 0 show precheck, 1 show serious check
    int ToCheckOrNotToCheck()
    {
        int currentDay = currentDayIndex + 1;
        int currentWeek = currentWeekIndex + 1;

        //check the date and values and decide
        if (currentDayIndex == 0) return 2;
        else if (ShouldSaveToPreviousWeek() == 1) return 1;
        else if (string.IsNullOrEmpty(vM.GetWeeklyValues()[currentWeekIndex]) && ((currentWeek * 7) - 1 == currentDay 
                 || (currentWeek * 7) - 2 == currentDay || currentWeek * 7 == currentDay)) return 0;
        else return 2;
    }

    void LoadButtons()
    {
        int correct = ToCheckOrNotToCheck();
        if (correct == 2)
        {
            checkObjects[0].SetActive(false);
            checkObjects[1].SetActive(false);
        }
        else
        {
            checkObjects[correct].SetActive(true);
            checkObjects[Mathf.Abs(correct - 1)].SetActive(false);
            string name = vM.GetWeeklyName(currentWeekIndex - correct);
            weekNames[correct].text = name;
        }
    }

    //if the answer is fail, opens the popup the first time to check if the answer was correctly input, then loads the next scene
    public void CheckAndLoadScene()
    {
        if (counter == 0)
        {
            if (weeklyCheck == OptionCodes.options[1])
            {
                pM.EnablePopup(popupCheck);
                counter++;
                return;
            }
        }
        //saves values and loads the next scene
        SaveValuesAndLoad();
    }

    public void ClosePopup()
    {
        pM.DisablePopup();
    }

    //called by the button in the popup
    public void LoadNextScene()
    {
        SaveValuesAndLoad();
    }

    void SaveValuesAndLoad()
    {
        //sets the default
        if (string.IsNullOrEmpty(weeklyCheck))
        {
            //if this is the serious check default is yes..otherwise it can stay as empty string 
            if (ToCheckOrNotToCheck() == 1)
            {
                weeklyCheck = OptionCodes.options[0];
            }
        }

        //sets the results to playerprefs
        vM.SaveWeeklyCheck(weeklyCheck, currentWeekIndex - ShouldSaveToPreviousWeek());

        if (ShouldSaveToPreviousWeek() == 1)
        {
            FindObjectOfType<SceneLoader>().LoadNextScene();
        }
        else
        {
            FindObjectOfType<SceneLoader>().LoadSceneByName("Check");
        }
        
    }
    

}

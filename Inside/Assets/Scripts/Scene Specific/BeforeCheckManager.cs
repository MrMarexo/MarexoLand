using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (lastWeekCheck == "" && (currentDayIndex % 7 == 0 || currentDayIndex == 29))
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
        else if (vM.GetWeeklyValues()[currentWeekIndex] == "" && ((currentWeek * 7) - 1 == currentDay 
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
        if (weeklyCheck == "")
        {
            //if this is the serious check default is yes..otherwise it can stay as empty string 
            if (ToCheckOrNotToCheck() == 1)
            {
                weeklyCheck = OptionCodes.options[0];
            }
        }

        //sets the results to playerprefs
        vM.SaveWeeklyCheck(weeklyCheck, currentWeekIndex - ShouldSaveToPreviousWeek());

        LoadTheCorrectScene();
    }
    
    //chooses the scene to load
    void LoadTheCorrectScene()
    {
        string[] sceneNames = { "Calendar", "Today Check", "Yesterday Check", "Second Check Yesterday", "Second Check Today", "Second Task Manager", "New Week Name" };
        string scenetoLoad = sceneNames[0]; //calendar is default
        string[] todayValues = vM.GetValuesOfIndex(currentDayIndex); //0 is first, 1 is second, 2 is habit

        if (currentDayIndex == 0) //launch day
        {
            if (todayValues[0] == "" || todayValues[2] == "")
            {
                scenetoLoad = sceneNames[1];
            }
            else if (vM.GetNames()[3] == "" && currentDayIndex + 1 > daysToStartWithSecondCheck)
            {
                scenetoLoad = sceneNames[5];
            }
            else if (todayValues[1] == "" && currentDayIndex + 1 > daysToStartWithSecondCheck)
            {
                scenetoLoad = sceneNames[4];
            }
            else
            {
                scenetoLoad = sceneNames[0];
            }
        }
        else if (currentDayIndex > 0)
        {
            Debug.Log(currentDayIndex % 7 + " = first part");
            Debug.Log(vM.GetWeeklyName(currentWeekIndex) + " = second part");
            Debug.Log(currentWeekIndex + " = current week index ");
            string[] yesterdayValues = vM.GetValuesOfIndex(currentDayIndex - 1); //0 is first, 1 is second, 2 is habit
            if (currentDayIndex % 7 == 0 && vM.GetWeeklyName(currentWeekIndex) == "") //if its the first day of the new week and the weekly challenge name is empty
            {
                scenetoLoad = sceneNames[6];
            }
            else if (yesterdayValues[0] == "" || yesterdayValues[2] == "")
            {
                scenetoLoad = sceneNames[2];
            }
            else if (yesterdayValues[1] == "" && currentDayIndex + 1 - 1 > daysToStartWithSecondCheck)
            {
                scenetoLoad = sceneNames[3];
            }
            else if (todayValues[0] == "" || todayValues[2] == "")
            {
                scenetoLoad = sceneNames[1];
            }
            else if (vM.GetNames()[3] == "" && currentDayIndex + 1 > daysToStartWithSecondCheck)
            {
                scenetoLoad = sceneNames[5];
            }
            else if (todayValues[1] == "" && currentDayIndex + 1 > daysToStartWithSecondCheck)
            {
                scenetoLoad = sceneNames[4];
            }
            else
            {
                scenetoLoad = sceneNames[0];
            }

            if (currentDayIndex > 1)
            {
                string[] beforeValues = vM.GetValuesOfIndex(currentDayIndex - 2); //0 is first, 1 is second, 2 is habit
                if (beforeValues[0] == "" || beforeValues[2] == "")
                {
                    ////////////////do a popup saying its been too long and you need to start over again
                    return;
                }
            }
        }
        
        //load chosen scene
        FindObjectOfType<SceneLoader>().LoadSceneByName(scenetoLoad);
    }

}

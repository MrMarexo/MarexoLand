using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CalendarManager : MonoBehaviour
{
    //2 calendar gameObjects
    [SerializeField] GameObject[] pages = new GameObject[2];

    //2 buttons to browse calendar
    [SerializeField] GameObject nextWeekButton;
    [SerializeField] GameObject previousWeekButton;

    //week number
    [SerializeField] TextMeshProUGUI weekNumberText;

    //week task gOs
    [SerializeField] TextMeshProUGUI[] weekTasks = new TextMeshProUGUI[2]; 

    //array of 7 calendar gameobjects 
    TextMeshProUGUI[] days7 = new TextMeshProUGUI[7];

    //array of 2 calendar gameobjexts
    TextMeshProUGUI[] days2 = new TextMeshProUGUI[2];

    //array of 7 and 2 day arrays
    TextMeshProUGUI[][] calendars = new TextMeshProUGUI[2][];

    [SerializeField] GameObject dayPopup;
    [SerializeField] GameObject weekPopup;


    //day popup text elements
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI weekly;
    [SerializeField] TextMeshProUGUI habit;
    [SerializeField] TextMeshProUGUI first;
    [SerializeField] TextMeshProUGUI second;
    [SerializeField] TextMeshProUGUI secondText;

    [SerializeField] TextMeshProUGUI powerNumberDay;

    [SerializeField] TextMeshProUGUI checkpointNumberDay;
    [SerializeField] TextMeshProUGUI plusCheckpointDay;
    [SerializeField] TextMeshProUGUI minusCheckpointDay;

    [SerializeField] TextMeshProUGUI slowdownNumberDay;
    [SerializeField] TextMeshProUGUI plusSlowdownDay;
    [SerializeField] TextMeshProUGUI minusSlowdownDay;

    [SerializeField] TextMeshProUGUI insteadNumberDay;
    [SerializeField] TextMeshProUGUI plusInsteadDay;
    [SerializeField] TextMeshProUGUI minusInsteadDay;

    //week popup elements
    [SerializeField] TextMeshProUGUI weeklyInWeek;
    [SerializeField] TextMeshProUGUI journalWeekText;

    int currentWeekIndex;
    int currentDayIndex;
    int curDateNumber;

    int playLevelNumber = -1;

    int weekIndex;

    float timeToWait;

    int checkpointsBought = 0;
    int slowdownsBought = 0;
    int insteadsBought = 0;

    //prices
    [SerializeField] int checkpointPrice = 5;
    [SerializeField] int slowdownPrice = 5;
    [SerializeField] int insteadPrice = 5;

    //will be taken from value management
    int power;

    ValueManagement vM;
    DateManagement dM;
    Willpower will;

    void Start()
    {
        dM = FindObjectOfType<DateManagement>();
        vM = FindObjectOfType<ValueManagement>();
        will = FindObjectOfType<Willpower>();

        power = will.GetWillpower();
        //points = vM.GetPoints();
        FillUpCalendars();
        timeToWait = FindObjectOfType<SceneLoader>().GetTimeToLoad();
        currentDayIndex = dM.GetCurrentDayIndex();
        curDateNumber = currentDayIndex + 1;
        currentWeekIndex = dM.GetCurrentWeek();
        weekIndex = currentWeekIndex;
        InstantlyCreateCalendar();

        UpdateCheckpoints();
        UpdateSlowdowns();
        UpdateInsteads();
    }

    void UpdateWeekNumberText()
    {
        weekNumberText.text = "Week " + (weekIndex + 1).ToString();
    }

    void UpdatePoints()
    {
        powerNumberDay.text = power.ToString();
    }

    void UpdateCheckpoints()
    {
        if (checkpointsBought == 0)
        {
            minusCheckpointDay.color = Colors.incompleteColor;
            minusCheckpointDay.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            minusCheckpointDay.color = Colors.completeColor;
            minusCheckpointDay.gameObject.GetComponent<Button>().enabled = true;
        }

        if (checkpointsBought == 3 || power - checkpointPrice < 0)
        {
            plusCheckpointDay.color = Colors.incompleteColor;
            plusCheckpointDay.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            plusCheckpointDay.color = Colors.completeColor;
            plusCheckpointDay.gameObject.GetComponent<Button>().enabled = true;
        }
        checkpointNumberDay.text = checkpointsBought.ToString();
        UpdatePoints(); 
    }
    

    public void AddCheckpoint()
    {
        if (power - checkpointPrice >= 0 && checkpointsBought < 3)
        {
            ++checkpointsBought;
            power -= checkpointPrice;
            UpdateCheckpoints();
        }
        
    }

    public void SubtractCheckpoint()
    {
        if (checkpointsBought > 0)
        {
            --checkpointsBought;
            power += checkpointPrice;
            UpdateCheckpoints();
        }
        
    }


    void UpdateInsteads()
    {
        if (insteadsBought == 0)
        {
            minusInsteadDay.color = Colors.incompleteColor;
            minusInsteadDay.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            minusInsteadDay.color = Colors.completeColor;
            minusInsteadDay.gameObject.GetComponent<Button>().enabled = true;
        }

        if (insteadsBought == 3 || power - insteadPrice < 0)
        {
            plusInsteadDay.color = Colors.incompleteColor;
            plusInsteadDay.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            plusInsteadDay.color = Colors.completeColor;
            plusInsteadDay.gameObject.GetComponent<Button>().enabled = true;
        }
        insteadNumberDay.text = insteadsBought.ToString();
        UpdatePoints();
    }


    public void AddInstead()
    {
        if (power - insteadPrice >= 0 && insteadsBought < 3)
        {
            ++insteadsBought;
            power -= insteadPrice;
            UpdateInsteads();
        }

    }

    public void SubtractInstead()
    {
        if (insteadsBought > 0)
        {
            --insteadsBought;
            power += insteadPrice;
            UpdateInsteads();
        }

    }


    void UpdateSlowdowns()
    {
        if (slowdownsBought == 0)
        {
            minusSlowdownDay.color = Colors.incompleteColor;
            minusSlowdownDay.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            minusSlowdownDay.color = Colors.completeColor;
            minusSlowdownDay.gameObject.GetComponent<Button>().enabled = true;
        }

        if (slowdownsBought == 3 || power - slowdownPrice < 0)
        {
            plusSlowdownDay.color = Colors.incompleteColor;
            plusSlowdownDay.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            plusSlowdownDay.color = Colors.completeColor;
            plusSlowdownDay.gameObject.GetComponent<Button>().enabled = true;
        }
        slowdownNumberDay.text = slowdownsBought.ToString();
        UpdatePoints();
    }


    public void AddSlowdown()
    {
        if (power - slowdownPrice >= 0 && slowdownsBought < 3)
        {
            ++slowdownsBought;
            power -= slowdownPrice;
            UpdateSlowdowns();
        }

    }

    public void SubtractSlowdownd()
    {
        if (slowdownsBought > 0)
        {
            --slowdownsBought;
            power += slowdownPrice;
            UpdateSlowdowns();
        }

    }


    //enables and disables the browse calendar buttons
    void ButtonsEnableDisable()
    {
        //get the buttons of the proper calendar and enable them
        previousWeekButton.SetActive(true);
        nextWeekButton.SetActive(true);

        //if current is 0 disable the previous button
        if (weekIndex == 0)
        {
            previousWeekButton.SetActive(false);
        }

        //if current is four disable the next button and dont run further
        if (weekIndex == 4) //will later tweak when more weeks will be possible
        {
            nextWeekButton.SetActive(false);
        }
        else if (weekIndex == currentWeekIndex)
        {
            nextWeekButton.SetActive(false);
        }
    }

    //fills up calendars array with two days arrays
    void FillUpCalendars()
    {
        //finds the 7 day game objects and puts them into the array
        Button[] gO7 = pages[0].GetComponentsInChildren<Button>();
        for (int i = 0; i < 7; i++) //because 7th is the week button
        {
            var tM = gO7[i].gameObject.GetComponent<TextMeshProUGUI>();
            days7[i] = tM;
        }

        ////finds the 2 day game objects and puts them into the array
        Button[] gO2 = pages[1].GetComponentsInChildren<Button>();
        for (int i = 0; i < 2; i++) //because 2nd is the week button
        {
            var tM = gO2[i].gameObject.GetComponent<TextMeshProUGUI>();
            days2[i] = tM;
        }

        //adds the two arrays to the calendar array
        calendars[0] = days7;
        calendars[1] = days2;
    }

    //returns index of calendar[][] .... also for CanvasManagement.calendars[]
    int GetCalendarIndex()
    {
        if (weekIndex % 4 == 0 && weekIndex != 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //creates calendar without waiting
    void InstantlyCreateCalendar()
    {
        EnableDisableWeeks();
        CalculateCalendar();
        ButtonsEnableDisable();
        UpdateWeekNumberText();
    }

    //creates calendar with waiting to simulate loading a new scene
    IEnumerator CreateCalendar()
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        EnableDisableWeeks();
        CalculateCalendar();
        ButtonsEnableDisable();
        UpdateWeekNumberText();
    }

    //shows correct gameobject with days - also woth the week challenge
    void EnableDisableWeeks()
    {
        int correctPageIndex = GetCalendarIndex();
        //enable the correct page
        pages[correctPageIndex].SetActive(true);
        //disable the other one
        pages[Mathf.Abs(correctPageIndex - 1)].SetActive(false);
    }

    void ShowWeeklyTask(int index)
    {
        var correctWeekObject = weekTasks[index];
        string[] values = vM.GetWeeklyValues(); //0 is first week, 1 is second etc
        if (values[weekIndex] == "") 
        {
            correctWeekObject.color = Colors.incompleteColor;
        }
        else if (values[weekIndex] == OptionCodes.options[0]) //if success
        {
            correctWeekObject.color = Colors.completeColor;
        }
        else if (values[weekIndex] == OptionCodes.options[1])
        {
            correctWeekObject.color = Colors.toggleGrayColor;
        }
    }

    //loads whichever scene from where we got to the journal
    public void GoBackToPastScene()
    {
        FindObjectOfType<SceneLoader>().LoadPastScene();
    }

    public void ShowDayInfo()
    {
        string dayString = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].text;
        int dayIndex = int.Parse(dayString) - 1;
        playLevelNumber = dayIndex + 1;

        //fill up texts
        day.text = "Day " + (dayIndex + 1).ToString();
        weekly.text = vM.GetWeeklyName(weekIndex);
        first.text = vM.GetNamesOfDayIndex(dayIndex)[0];
        habit.text = vM.GetNamesOfDayIndex(dayIndex)[2];

        //if there is no name for the second task dont show it in the info at all
        if (vM.GetNamesOfDayIndex(dayIndex)[1] == "")
        {
            second.gameObject.SetActive(false);
            secondText.gameObject.SetActive(false);
        }
        else
        {
            second.gameObject.SetActive(true);
            secondText.gameObject.SetActive(true);
            second.text = vM.GetNamesOfDayIndex(dayIndex)[1];
        }
        FindObjectOfType<PopupManagement>().EnablePopup(dayPopup);
    }

    public void ShowWeekInfo()
    {
        weeklyInWeek.text = vM.GetWeeklyName(weekIndex);
        journalWeekText.text = vM.GetDayJournalOfIndex(weekIndex);

        FindObjectOfType<PopupManagement>().EnablePopup(weekPopup);
    }

    public void StartLevel()
    {
        vM.SaveBoughtCheckpointsForLevel(checkpointsBought);
        vM.SaveBoughtInsteadsForLevel(insteadsBought);
        vM.SaveBoughtSlowdownsForLevel(slowdownsBought);
        will.SetWillpower(power);
        FindObjectOfType<SceneLoader>().LoadSceneByName("Day " + playLevelNumber.ToString());
    }

    public void ClosePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }

    public void ShowNextWeek()
    {
        weekIndex++;
        StartCoroutine(CreateCalendar());
    }

    public void ShowPreviousWeek()
    {
        weekIndex--;
        StartCoroutine(CreateCalendar());
    }

    void CalculateCalendar()
    {
        int calendarIndex = GetCalendarIndex();
        var weekToShow = calendars[calendarIndex];

        //show weekly task gameobject
        ShowWeeklyTask(calendarIndex);

        for (int i = 0; i < weekToShow.Length; i++)
        {
            //colors
            Color32 incompleteColor = Colors.incompleteColor;
            Color32 invisibleColor = Colors.invisibleColor;
            Color32 completeColor = Colors.completeColor;
            Color32 failedColor = Colors.toggleGrayColor;

            var cur = weekToShow[i];
            var curButton = cur.GetComponent<Button>();

            int curDateNumber = currentDayIndex + 1;

            //0 is day text, 1 is the first check, 2 is the second, 3 is the underline
            TextMeshProUGUI[] arrayOfTMs = cur.GetComponentsInChildren<TextMeshProUGUI>();
            
            int dayInArr = (weekIndex * 7) + i; //current day in loop - array number == zero based
            int dayGlobal = dayInArr + 1; //current day in loop = 1 based number
            cur.text = dayGlobal.ToString();

            //set up regular properties
            arrayOfTMs[3].color = invisibleColor; //colors the underline color -- its invisible
            arrayOfTMs[0].color = invisibleColor; //this only makes it invisible --need to figure out how to disable it
            curButton.enabled = false; //this disables the button component for all days(makes them unclickable)
            //-- only the present and past days will be active (visible, clickable), thanks to the fragment right below

            //set for today and all days before today
            if (dayGlobal <= curDateNumber)
            {
                arrayOfTMs[0].color = incompleteColor;
                curButton.enabled = true; //enable the button component for current day and days before
            }

            //set for today
            if (dayGlobal == curDateNumber)
            {
                //indicate this is the current day
                arrayOfTMs[3].color = incompleteColor;                                                         
            }

            //gets values for the current day in loop
            string[] stringValues = vM.GetValuesOfIndex(dayInArr); //0 is firstcheck, 1 is secondcheck and 2 is habitcheck 
            

            //sets the default - if the value is "" then the dot won't be visible
            arrayOfTMs[1].text = stringValues[0]; //sets the first daily check
            arrayOfTMs[2].text = stringValues[1]; //sets the second daily check

            //colors first daily checks
            if (stringValues[0] == "S") //checks first daily check value
            {
                //the index after getcomponents is +1 because the first TextMeshPro component is the day number(current one)
                arrayOfTMs[1].text = ".";
                arrayOfTMs[1].color = completeColor;
            }
            else if (stringValues[0] == "F")
            {
                arrayOfTMs[1].text = ".";
                arrayOfTMs[1].color = failedColor;
            }

            //colors second daily checks
            if (stringValues[1] == "S") //checks second daily check value
            {
                //the index after getcomponents is +1 because the first TextMeshPro component is the day number(current one)
                arrayOfTMs[2].text = ".";
                arrayOfTMs[2].color = completeColor;
            }
            else if (stringValues[1] == "F")
            {
                arrayOfTMs[2].text = ".";
                arrayOfTMs[2].color = failedColor;
            }

            //colors days and underline
            if (stringValues[2] == "S")
            {
                cur.color = completeColor;
                //colors underline only if its the current date
                if (dayGlobal == curDateNumber)
                {
                    arrayOfTMs[3].color = completeColor;
                }

            }
            if (stringValues[2] == "F")
            {
                cur.color = failedColor;
                //colors underline only if its the current date
                if (dayGlobal == curDateNumber)
                {
                    arrayOfTMs[3].color = failedColor;
                }
            }
        }
    }
}

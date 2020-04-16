using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckManager : MonoBehaviour
{
    ValueManagement vM;
    PopupManagement pM;
    SceneLoader sL;

    [SerializeField] GameObject yesterdayBlock;
    [SerializeField] GameObject instructionBlock;


    [SerializeField] GameObject[] todayHabitBlock = new GameObject[3];
    [SerializeField] GameObject[] todayFirstBlock = new GameObject[3];
    [SerializeField] GameObject[] todaySecondBlock = new GameObject[3];
    [SerializeField] TextMeshProUGUI todayTitle;

    [SerializeField] GameObject[] yesterdayHabitBlock = new GameObject[3];
    [SerializeField] GameObject[] yesterdayFirstBlock = new GameObject[3];
    [SerializeField] GameObject[] yesterdaySecondBlock = new GameObject[3];
    [SerializeField] TextMeshProUGUI yesterdayTitle;


    [SerializeField] TextMeshProUGUI todayHabitValueText;
    [SerializeField] TextMeshProUGUI todayFirstValueText;
    [SerializeField] TextMeshProUGUI todaySecondValueText;
    [SerializeField] TextMeshProUGUI yesterdayHabitValueText;
    [SerializeField] TextMeshProUGUI yesterdayFirstValueText;
    [SerializeField] TextMeshProUGUI yesterdaySecondValueText;


    int todayIndex;
    int yesterdayIndex = -1;

    string firstDailyCheckToday = "";
    string secondDailyCheckToday = "";
    string habitDailyCheckToday = "";

    string firstDailyCheckYesterday = "";
    string secondDailyCheckYesterday = "";
    string habitDailyCheckYesterday = "";

    //counter to make sure popup only enables once
    int counter = 0;


    [SerializeField] GameObject popupCheck;

    private void Start()
    {
        
        vM = FindObjectOfType<ValueManagement>();
        pM = FindObjectOfType<PopupManagement>();
        todayIndex = FindObjectOfType<DateManagement>().GetCurrentDayIndex();
        DetermineYesterdayIndex();
        FillInValueTexts();
        ShowYesterdayOrInstructions();
        ShouldShowBlocksForToday();
        ShouldSecondToday();
    }

    void DetermineYesterdayIndex()
    {
        if (todayIndex == 0)
        {
            yesterdayIndex = -1;
        }
        else
        {
            yesterdayIndex = todayIndex - 1;
        }
    }

    void ShowYesterdayOrInstructions()
    {
        if (todayIndex == 0)
        {
            yesterdayBlock.SetActive(false);
            instructionBlock.SetActive(true);
        }
        else
        {
            yesterdayBlock.SetActive(true);
            instructionBlock.SetActive(false);
            ShouldShowBlocksForYesterday();
            ShouldSecondYesterday();
        }
    }

    void ShouldShowBlocksForYesterday()
    {
        var values = vM.GetValuesOfIndex(yesterdayIndex);
        
        if (!string.IsNullOrEmpty(values[0]))
        {
            DiscolorBlock(yesterdayFirstBlock);
        }
        
        if (!string.IsNullOrEmpty(values[1]))
        {
            DiscolorBlock(yesterdaySecondBlock);
        }
        
        if (!string.IsNullOrEmpty(values[2]))
        {
            DiscolorBlock(yesterdayHabitBlock);
        }

        if (!string.IsNullOrEmpty(values[0]) && !string.IsNullOrEmpty(values[1]) && !string.IsNullOrEmpty(values[2]))
        {
            yesterdayTitle.color = Colors.disabledRedText;
        }
    }

    void ShouldSecondToday()
    {
        bool shouldShow = false;
        if (todayIndex > 14)
        {
            shouldShow = true;
        }
        EnableDisableBlock(todaySecondBlock, shouldShow);
    }

    void ShouldSecondYesterday()
    {
        bool shouldShow = false;
        if (yesterdayIndex > 14)
        {
            shouldShow = true;
        }
        EnableDisableBlock(yesterdaySecondBlock, shouldShow);
    }

    void ShouldShowBlocksForToday()
    {
        var values = vM.GetValuesOfIndex(todayIndex);
        if (!string.IsNullOrEmpty(values[0]))
        {
            DiscolorBlock(todayFirstBlock);
        }

        if (!string.IsNullOrEmpty(values[1]))
        {
            DiscolorBlock(todaySecondBlock);
        }
        
        if (!string.IsNullOrEmpty(values[2]))
        {
            DiscolorBlock(todayHabitBlock);
        }

        if (!string.IsNullOrEmpty(values[0]) && !string.IsNullOrEmpty(values[1]) && !string.IsNullOrEmpty(values[2]))
        {
            todayTitle.color = Colors.disabledRedText;
        }
    }

    void DiscolorBlock(GameObject[] block)
    {
        foreach (GameObject go in block)
        {
            var tm = go.GetComponent<TextMeshProUGUI>();
            if (tm)
            {
                tm.color = Colors.disabledRedText;
            }
            else
            {
                var images = go.GetComponentsInChildren<Image>();
                foreach (Image image in images)
                {
                    image.color = Colors.disabledRedText;
                    image.gameObject.GetComponent<Button>().enabled = false;
                }
            }
        }
    }

    void EnableDisableBlock(GameObject[] block, bool enableDisable)
    {
        foreach (GameObject go in block)
        {
            go.SetActive(enableDisable);
        }
    }

    void FillInValueTexts()
    {
        var todayValues = vM.GetNamesOfDayIndex(todayIndex);
        todayFirstValueText.text = todayValues[0];
        todaySecondValueText.text = todayValues[1];
        todayHabitValueText.text = todayValues[2];

        if (yesterdayIndex >= 0)
        {
            var yesterdayValues = vM.GetNamesOfDayIndex(yesterdayIndex);
            yesterdayFirstValueText.text = todayValues[0];
            yesterdaySecondValueText.text = todayValues[1];
            yesterdayHabitValueText.text = todayValues[2];
        }

    }

    //TODAY methods on buttons to set a check reply to vMs arrays
    public void TodayFirstCheckYes()
    {
        firstDailyCheckToday = OptionCodes.options[0];
    }

    public void TodayFirstCheckMaybe()
    {
        firstDailyCheckToday = OptionCodes.options[2];
    }

    public void TodaySecondCheckYes()
    {
        secondDailyCheckToday = OptionCodes.options[0];
    }

    public void TodaySecondCheckMaybe()
    {
        secondDailyCheckToday = OptionCodes.options[2];
    }

    public void TodayHabitCheckNo()
    {
        habitDailyCheckToday = OptionCodes.options[1];
    }

    public void TodayHabitCheckMaybe()
    {
        habitDailyCheckToday = OptionCodes.options[2];
    }


    //YESTERDAY methods on buttons to set a check reply to vMs arrays
    public void YesterdayFirstCheckYes()
    {
        firstDailyCheckYesterday = OptionCodes.options[0];
    }

    public void YesterdayFirstCheckNo()
    {
        firstDailyCheckYesterday = OptionCodes.options[1];
    }

    public void YesterdaySecondCheckYes()
    {
        secondDailyCheckYesterday = OptionCodes.options[0];
    }

    public void YesterdaySecondCheckNo()
    {
        secondDailyCheckYesterday = OptionCodes.options[1];
    }

    public void YesterdayHabitCheckNo()
    {
        habitDailyCheckYesterday = OptionCodes.options[1];
    }

    public void YesterdayHabitCheckYes()
    {
        habitDailyCheckYesterday = OptionCodes.options[0];
    }

    //if the answer is no on habit, opens the popup the first time to check if the answer was correctly input, then loads the next scene
    public void CheckAndLoadScene()
    {
        if (counter == 0)
        {
            if (habitDailyCheckToday == OptionCodes.options[1] || habitDailyCheckYesterday == OptionCodes.options[1] || 
                secondDailyCheckYesterday == OptionCodes.options[1] || firstDailyCheckYesterday == OptionCodes.options[1])
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
        //sets the default (for today it's empty string)
        if (string.IsNullOrEmpty(habitDailyCheckYesterday))
        {
            habitDailyCheckYesterday = OptionCodes.options[0];
        }
        if (string.IsNullOrEmpty(firstDailyCheckYesterday))
        {
            firstDailyCheckYesterday = OptionCodes.options[0];
        }
        if (string.IsNullOrEmpty(secondDailyCheckYesterday))
        {
            secondDailyCheckYesterday = OptionCodes.options[0];
        }

        //sets the results to playerprefs
        vM.SaveHabitAndFirstCheck(habitDailyCheckToday, firstDailyCheckToday, todayIndex);
        vM.SaveSecondCheck(secondDailyCheckToday, todayIndex);
        vM.SaveDailyNamesForTomorrow(todayIndex);

        if (yesterdayIndex >= 0)
        {
            vM.SaveHabitAndFirstCheck(habitDailyCheckYesterday, firstDailyCheckYesterday, yesterdayIndex);
            vM.SaveSecondCheck(secondDailyCheckYesterday, yesterdayIndex);
        }

        //loads the next scene
        FindObjectOfType<SceneLoader>().LoadSceneByName("Calendar");

    }

}

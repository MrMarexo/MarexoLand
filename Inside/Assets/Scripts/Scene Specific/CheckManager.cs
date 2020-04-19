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

    string[] arrayOfChanged = new string[6];

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
            DiscolorBlock(yesterdayFirstBlock, values[0]);
            firstDailyCheckYesterday = values[0];
        }

        if (!string.IsNullOrEmpty(values[1]))
        {
            DiscolorBlock(yesterdaySecondBlock, values[1]);
            secondDailyCheckYesterday = values[1];
        }

        if (!string.IsNullOrEmpty(values[2]))
        {
            DiscolorBlock(yesterdayHabitBlock, values[2]);
            habitDailyCheckYesterday = values[20];
        }

        if (!string.IsNullOrEmpty(values[0]) && !string.IsNullOrEmpty(values[1]) && !string.IsNullOrEmpty(values[2]))
        {
            yesterdayTitle.color = Colors.disabledRedText;
        }
    }

    void ShouldSecondToday()
    {
        bool shouldShow = false;
        string nameSaved = vM.GetNamesOfDayIndex(todayIndex)[1];
        if (string.IsNullOrEmpty(nameSaved))
        {
            shouldShow = true;
        }
        EnableDisableBlock(todaySecondBlock, shouldShow);
    }

    void ShouldSecondYesterday()
    {
        bool shouldShow = false;
        string nameSaved = vM.GetNamesOfDayIndex(yesterdayIndex)[1];
        if (string.IsNullOrEmpty(nameSaved))
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
            DiscolorBlock(todayFirstBlock, values[0]);
            firstDailyCheckToday = values[0];
        }

        if (!string.IsNullOrEmpty(values[1]))
        {
            DiscolorBlock(todaySecondBlock, values[1]);
            secondDailyCheckToday = values[1];
        }

        if (!string.IsNullOrEmpty(values[2]))
        {
            DiscolorBlock(todayHabitBlock, values[2]);
            habitDailyCheckToday = values[2];
        }

        if (!string.IsNullOrEmpty(values[0]) && !string.IsNullOrEmpty(values[1]) && !string.IsNullOrEmpty(values[2]))
        {
            todayTitle.color = Colors.disabledRedText;
        }

    }

    void DiscolorBlock(GameObject[] block, string result)
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
                var chosenName = "";
                if (result == OptionCodes.options[0])
                {
                    chosenName = "Yes";
                }
                else
                {
                    chosenName = "No";
                }
                foreach (Image image in images)
                {
                    image.gameObject.GetComponent<Button>().enabled = false;
                    if (image.gameObject.name == chosenName)
                    {
                        image.color = Colors.completeColor;
                        image.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
                    }
                    else
                    {
                        image.color = Colors.disabledRedText;
                        image.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
                    }

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
        arrayOfChanged[0] = firstDailyCheckToday;
    }

    public void TodayFirstCheckMaybe()
    {
        firstDailyCheckToday = OptionCodes.options[2];
        arrayOfChanged[0] = firstDailyCheckToday;
    }

    public void TodaySecondCheckYes()
    {
        secondDailyCheckToday = OptionCodes.options[0];
        arrayOfChanged[1] = secondDailyCheckToday;
    }

    public void TodaySecondCheckMaybe()
    {
        secondDailyCheckToday = OptionCodes.options[2];
        arrayOfChanged[1] = secondDailyCheckToday;
    }

    public void TodayHabitCheckNo()
    {
        habitDailyCheckToday = OptionCodes.options[1];
        arrayOfChanged[2] = habitDailyCheckToday;
    }

    public void TodayHabitCheckMaybe()
    {
        habitDailyCheckToday = OptionCodes.options[2];
        arrayOfChanged[2] = habitDailyCheckToday;
    }


    //YESTERDAY methods on buttons to set a check reply to vMs arrays
    public void YesterdayFirstCheckYes()
    {
        firstDailyCheckYesterday = OptionCodes.options[0];
        arrayOfChanged[4] = firstDailyCheckYesterday;
    }

    public void YesterdayFirstCheckNo()
    {
        firstDailyCheckYesterday = OptionCodes.options[1];
        arrayOfChanged[4] = firstDailyCheckYesterday;
    }

    public void YesterdaySecondCheckYes()
    {
        secondDailyCheckYesterday = OptionCodes.options[0];
        arrayOfChanged[5] = secondDailyCheckYesterday;
    }

    public void YesterdaySecondCheckNo()
    {
        secondDailyCheckYesterday = OptionCodes.options[1];
        arrayOfChanged[5] = secondDailyCheckYesterday;
    }

    public void YesterdayHabitCheckNo()
    {
        habitDailyCheckYesterday = OptionCodes.options[1];
        arrayOfChanged[6] = habitDailyCheckYesterday;
    }

    public void YesterdayHabitCheckYes()
    {
        habitDailyCheckYesterday = OptionCodes.options[0];
        arrayOfChanged[6] = habitDailyCheckYesterday;
    }

    //if the answer is no on habit, opens the popup the first time to check if the answer was correctly input, then loads the next scene
    public void CheckAndLoadScene()
    {
        if (counter == 0)
        {
            foreach (string el in arrayOfChanged)
            {
                if (el == OptionCodes.options[1])
                {
                    pM.EnablePopup(popupCheck);
                    counter++;
                    return;
                }
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
        vM.SaveHabitCheck(habitDailyCheckToday, todayIndex);
        vM.SaveFirstCheck(firstDailyCheckToday, todayIndex);
        vM.SaveSecondCheck(secondDailyCheckToday, todayIndex);
        vM.SaveDailyNamesForTomorrow(todayIndex);

        if (yesterdayIndex >= 0)
        {
            vM.SaveHabitCheck(habitDailyCheckYesterday, yesterdayIndex);
            vM.SaveFirstCheck(firstDailyCheckYesterday, yesterdayIndex);
            vM.SaveSecondCheck(secondDailyCheckYesterday, yesterdayIndex);
        }

        //loads the next scene
        if (todayIndex >= 13 && string.IsNullOrEmpty(vM.GetNamesOfDayIndex(todayIndex)[1]))
        {
            FindObjectOfType<SceneLoader>().LoadSceneByName("Second Task");
        }
        else
        {
            FindObjectOfType<SceneLoader>().LoadSceneByName("Calendar");
        }
        

    }

}

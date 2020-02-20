using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadingManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI dayText;

    [SerializeField] GameObject prevDay;
    [SerializeField] GameObject prevWeek;
    [SerializeField] GameObject nextDay;
    [SerializeField] GameObject nextWeek;

    [SerializeField] GameObject dayBtn;
    [SerializeField] GameObject weekBtn;

    ValueManagement vM;
    DateManagement dM;

    int curDayIndex;
    int curWeekIndex;

    int showDayIndex;
    int showWeekIndex;

    int dayLength;
    int weekLength;

    string dayString = "Day ";
    string weekString = "Week ";

    bool isShowingDay = true;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        dM = FindObjectOfType<DateManagement>();

        curDayIndex = dM.GetCurrentDayIndex();
        curWeekIndex = dM.GetCurrentWeek();
        dayLength = vM.GetDayLength();
        weekLength = vM.GetWeekLength();
        

        ShowDay();
        showWeekIndex = curWeekIndex;
    }

    void LimitDayBrowseButtons()
    {
        Debug.Log("runs");
        if (showDayIndex == 0)
        {
            prevDay.SetActive(false);
            Debug.Log("deactivate");
        }
        else
        {
            prevDay.SetActive(true);
            Debug.Log("activate");
        }

        if (showDayIndex == dayLength - 1)
        {
            nextDay.SetActive(false);
        }
        else
        {
            nextDay.SetActive(true);
        }
    }

    void LimitWeekBrowseButtons()
    {
        if (showWeekIndex == 0)
        {
            prevWeek.SetActive(false);
        }
        else
        {
            prevWeek.SetActive(true);
        }

        if (showWeekIndex == weekLength - 1)
        {
            nextWeek.SetActive(false);
        }
        else
        {
            nextWeek.SetActive(true);
        }
    }

    void ShowDayJournalOfIndex(int dayIndex)
    {
        string textValue = vM.GetDayJournalOfIndex(dayIndex);
        text.text = textValue;
        dayText.text = dayString + (dayIndex + 1).ToString();
        showDayIndex = dayIndex;
        LimitDayBrowseButtons();

    }

    void ShowWeekJournalOfIndex(int weekIndex)
    {
        string textValue = vM.GetWeekJournalOfIndex(weekIndex);
        text.text = textValue;
        dayText.text = weekString + (weekIndex + 1).ToString();
        showWeekIndex = weekIndex;
        LimitWeekBrowseButtons();
        
    }

    //on buttons
    public void PreviousDay()
    {
        ShowDayJournalOfIndex(showDayIndex - 1);
    }

    public void NextDay()
    {
        ShowDayJournalOfIndex(showDayIndex + 1);
    }

    public void PreviousWeek()
    {
        ShowWeekJournalOfIndex(showWeekIndex - 1);
    }

    public void NextWeek()
    {
        ShowWeekJournalOfIndex(showWeekIndex + 1);
    }


    void ShowWeekButtons(bool shouldShowWeek)
    {
        if (shouldShowWeek)
        {
            isShowingDay = false;
            prevDay.SetActive(false);
            nextDay.SetActive(false);

            prevWeek.SetActive(true);
            nextWeek.SetActive(true);
        }
        else
        {
            isShowingDay = true;
            prevDay.SetActive(true);
            nextDay.SetActive(true);

            prevWeek.SetActive(false);
            nextWeek.SetActive(false);
        }
    }

    public void ShowWeek()
    {
        ShowWeekButtons(true);
        ShowWeekJournalOfIndex(curWeekIndex);
        dayBtn.SetActive(true);
        weekBtn.SetActive(false);
    }

    public void ShowDay()
    {
        ShowWeekButtons(false);
        ShowDayJournalOfIndex(curDayIndex);
        dayBtn.SetActive(false);
        weekBtn.SetActive(true);
    }

    public void ToJournal()
    {
        FindObjectOfType<SceneLoader>().LoadPastScene();
    }


}

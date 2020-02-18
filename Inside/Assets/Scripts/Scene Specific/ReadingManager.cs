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

    ValueManagement vM;
    DateManagement dM;

    int curDayIndex;
    int curWeekIndex;

    int showDayIndex;
    int showWeekIndex;

    string dayString = "Day ";
    string weekString = "Week ";

    bool isShowingDay = true;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        dM = FindObjectOfType<DateManagement>();

        curDayIndex = dM.GetCurrentDayIndex();
        curWeekIndex = dM.GetCurrentWeek();

        ShowDayJournalOfIndex(curDayIndex);
        ShowWeekButtons(false);
        showWeekIndex = curWeekIndex;
    }

    void ShowDayJournalOfIndex(int dayIndex)
    {
        string textValue = vM.GetDayJournalOfIndex(dayIndex);
        text.text = textValue;
        dayText.text = dayString + (dayIndex + 1).ToString();
        showDayIndex = dayIndex;
    }

    void ShowWeekJournalOfIndex(int weekIndex)
    {
        string textValue = vM.GetWeekJournalOfIndex(weekIndex);
        text.text = textValue;
        dayText.text = weekString + (weekIndex + 1).ToString();
        showWeekIndex = weekIndex;
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
    }

    public void ShowDay()
    {
        ShowWeekButtons(false);
        ShowDayJournalOfIndex(curDayIndex);
    }
}

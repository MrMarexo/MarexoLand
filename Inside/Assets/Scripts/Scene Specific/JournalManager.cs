using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class JournalManager : MonoBehaviour
{
    [SerializeField] GameObject lastWeekButton;
    [SerializeField] GameObject yesterdayButton;

    [SerializeField] GameObject popupFun;
    [SerializeField] GameObject popupWarning;

    [SerializeField] TextMeshProUGUI restoreToday;
    [SerializeField] TextMeshProUGUI restoreYesterday;
    [SerializeField] TextMeshProUGUI restoreThisWeek;
    [SerializeField] TextMeshProUGUI restoreLastWeek;

    //panels
    [SerializeField] GameObject generalPanel;
    [SerializeField] GameObject todayPanel;
    [SerializeField] GameObject yesterdayPanel;
    [SerializeField] GameObject thisWeekPanel;
    [SerializeField] GameObject lastWeekPanel;

    //input fields
    [SerializeField] TMP_InputField todayField;
    [SerializeField] TMP_InputField yesterdayField;
    [SerializeField] TMP_InputField thisWeekField;
    [SerializeField] TMP_InputField lastWeekField;

    DateManagement dM;
    ValueManagement vM;

    int curDayIndex;
    int curWeekIndex;
    float timeToLoad;

    string todayValue = "";
    string yesterdayValue = "";
    string thisWeekValue = "";
    string lastWeekValue = "";

    int todayCounter = 0;
    int yesterdayCounter = 0;
    int thisWeekCounter = 0;
    int lastWeekCounter = 0;

    //in tens of percent--what is the chance of popup being enabled
    [SerializeField] int randomnessPercentage = 2;

    private void Start()
    {
        dM = FindObjectOfType<DateManagement>();
        vM = FindObjectOfType<ValueManagement>();

        curDayIndex = dM.GetCurrentDayIndex();
        curWeekIndex = dM.GetCurrentWeek();
        timeToLoad = FindObjectOfType<SceneLoader>().GetTimeToLoad();
        InputFieldInit();
        DisableAllExcept(generalPanel);
        HideOrShowLastWeek();
        HideOrShowYesterday();
    }

    private void Update()
    {
        CheckRestore();
    }

    void HideOrShowYesterday()
    {
        if (curDayIndex == 0)
        {
            yesterdayButton.GetComponent<TextMeshProUGUI>().color = Colors.incompleteColor;
            yesterdayButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            yesterdayButton.GetComponent<TextMeshProUGUI>().color = Colors.completeColor;
            yesterdayButton.GetComponent<Button>().enabled = true;
        }
    }
    void HideOrShowLastWeek()
    {
        if (curWeekIndex > 0 && (curDayIndex % 7 == 0 || curDayIndex == 29))
        {
            lastWeekButton.GetComponent<TextMeshProUGUI>().color = Colors.completeColor;
            lastWeekButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            lastWeekButton.GetComponent<TextMeshProUGUI>().color = Colors.incompleteColor;
            lastWeekButton.GetComponent<Button>().enabled = false;
        }
    }

    IEnumerator DisableAllExceptTimed(GameObject enablePanel)
    {
        yield return new WaitForSecondsRealtime(timeToLoad);
        DisableAllExcept(enablePanel);
    }

    void DisableAllExcept(GameObject enablePanel)
    {
        generalPanel.SetActive(false);
        todayPanel.SetActive(false);
        yesterdayPanel.SetActive(false);
        thisWeekPanel.SetActive(false);
        lastWeekPanel.SetActive(false);

        enablePanel.SetActive(true);
    }

    public void ShowToday()
    {
        StartCoroutine(DisableAllExceptTimed(todayPanel));
    }

    public void ShowYesterday()
    {
        StartCoroutine(DisableAllExceptTimed(yesterdayPanel));
    }

    public void ShowThisWeek()
    {
        StartCoroutine(DisableAllExceptTimed(thisWeekPanel));
    }

    public void ShowLastWeek()
    {
        StartCoroutine(DisableAllExceptTimed(lastWeekPanel));
    }

    public void DisableCurrent()
    {
        GameObject parentOfButton = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        StartCoroutine(EnableOrDisable(parentOfButton, false));
        StartCoroutine(EnableOrDisable(generalPanel, true));
    }

    //loads whichever scene from where we got to the journal
    public void GoBackToPastScene()
    {
        FindObjectOfType<SceneLoader>().LoadPastScene();
    }

    public void ToReadingJournal()
    {
        FindObjectOfType<SceneLoader>().LoadSceneByName("Reading");
    }

    
    void CheckRestore()
    {
        if (todayValue == todayField.text)
        {
            restoreToday.color = Colors.incompleteColor;
            restoreToday.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            restoreToday.color = Colors.completeColor;
            restoreToday.gameObject.GetComponent<Button>().enabled = true;
        }

        if (yesterdayValue == yesterdayField.text)
        {
            restoreYesterday.color = Colors.incompleteColor;
            restoreYesterday.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            restoreYesterday.color = Colors.completeColor;
            restoreYesterday.gameObject.GetComponent<Button>().enabled = true;
        }

        if (thisWeekValue == thisWeekField.text)
        {
            restoreThisWeek.color = Colors.incompleteColor;
            restoreThisWeek.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            restoreThisWeek.color = Colors.completeColor;
            restoreThisWeek.gameObject.GetComponent<Button>().enabled = true;
        }

        if (lastWeekValue == lastWeekField.text)
        {
            restoreLastWeek.color = Colors.incompleteColor;
            restoreLastWeek.gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            restoreLastWeek.color = Colors.completeColor;
            restoreLastWeek.gameObject.GetComponent<Button>().enabled = true;
        }
    }

    //restore to what was written before
    public void RestoreToday()
    {
        todayField.text = todayValue;
    }

    public void RestoreYesterday()
    {
        yesterdayField.text = yesterdayValue;
    }

    public void RestoreThisWeek()
    {
        thisWeekField.text = thisWeekValue;
    }

    public void RestoreLastWeek()
    {
        lastWeekField.text = lastWeekValue;
    }

    //on shut up button in fun popup
    public void ShutUp()
    {
        ClosePopup();
        StartCoroutine(DisableAllExceptTimed(generalPanel));
    }

    public void ClosePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }


    //on submit button
    public void CheckAndSaveToday()
    {
        if (todayField.text != "" && todayField.text != todayValue)
        {
            float random = Random.Range(0, 10);
            if (random < randomnessPercentage)
            {
                SaveToday();
                FindObjectOfType<PopupManagement>().EnablePopup(popupFun);
                return;
            }  
        }

        if (todayField.text == "" && todayValue != "" && todayCounter == 0)
        {
            todayCounter++;
            FindObjectOfType<PopupManagement>().EnablePopup(popupWarning);
            return;
        }

        SaveToday();
        ClosePopup();
        DisableCurrent();
    }

    void SaveToday()
    {
        vM.SaveDayJournal(todayField.text, curDayIndex);
        todayValue = vM.GetDayJournalOfIndex(curDayIndex);
    }

    //on submit button
    public void CheckAndSaveYesterday()
    {
        if (yesterdayField.text != "" && yesterdayField.text != yesterdayValue)
        {
            float random = Random.Range(0, 10);
            if (random < randomnessPercentage)
            {
                SaveYesterday();
                FindObjectOfType<PopupManagement>().EnablePopup(popupFun);
                return;
            }
        }

        if (yesterdayField.text == "" && yesterdayValue != "" && yesterdayCounter == 0)
        {
            yesterdayCounter++;
            FindObjectOfType<PopupManagement>().EnablePopup(popupWarning);
            return;
        }

        SaveYesterday();
        ClosePopup();
        DisableCurrent();
    }

    void SaveYesterday()
    {
        vM.SaveDayJournal(yesterdayField.text, curDayIndex - 1);
        yesterdayValue = vM.GetDayJournalOfIndex(curDayIndex - 1);
    }

    //on submit button
    public void CheckAndSaveThisWeek()
    {
        if (thisWeekField.text != "" && thisWeekField.text != thisWeekValue)
        {
            float random = Random.Range(0, 10);
            if (random < randomnessPercentage)
            {
                SaveThisWeek();
                FindObjectOfType<PopupManagement>().EnablePopup(popupFun);
                return;
            }
        }

        if (thisWeekField.text == "" && thisWeekValue != "" && thisWeekCounter == 0)
        {
            thisWeekCounter++;
            FindObjectOfType<PopupManagement>().EnablePopup(popupWarning);
            return;
        }

        SaveThisWeek();
        ClosePopup();
        DisableCurrent();
    }

    void SaveThisWeek()
    {
        vM.SaveWeekJournal(thisWeekField.text, curWeekIndex);
        thisWeekValue = vM.GetWeekJournalOfIndex(curWeekIndex);
    }

    //on submit button
    public void CheckAndSaveLastWeek()
    {
        if (lastWeekField.text != "" && lastWeekField.text != lastWeekValue)
        {
            float random = Random.Range(0, 10);
            if (random < randomnessPercentage)
            {
                SaveLastWeek();
                FindObjectOfType<PopupManagement>().EnablePopup(popupFun);
                return;
            }
        }

        if (lastWeekField.text == "" && lastWeekValue != "" && lastWeekCounter == 0)
        {
            lastWeekCounter++;
            FindObjectOfType<PopupManagement>().EnablePopup(popupWarning);
            return;
        }

        SaveLastWeek();
        ClosePopup();
        DisableCurrent();
    }

    void SaveLastWeek()
    {
        vM.SaveWeekJournal(lastWeekField.text, curWeekIndex - 1);
        lastWeekValue = vM.GetWeekJournalOfIndex(curWeekIndex - 1);
    }


    IEnumerator EnableOrDisable(GameObject panel, bool enableDisable)
    {
        yield return new WaitForSecondsRealtime(timeToLoad);
        panel.SetActive(enableDisable);
    }

    void InputFieldInit()
    {
        todayValue = vM.GetDayJournalOfIndex(curDayIndex);
        if (todayValue != "")
        {
            todayField.text = todayValue;
        }

        if (curDayIndex > 0)
        {
            yesterdayValue = vM.GetDayJournalOfIndex(curDayIndex - 1);
            if (todayValue != "")
            {
                yesterdayField.text = yesterdayValue;
            }
        }

        thisWeekValue = vM.GetWeekJournalOfIndex(curWeekIndex);
        if (thisWeekValue != "")
        {
            thisWeekField.text = thisWeekValue;
        }

        if (curWeekIndex > 0)
        {
            lastWeekValue = vM.GetWeekJournalOfIndex(curWeekIndex - 1);
            if (lastWeekValue != "")
            {
                lastWeekField.text = lastWeekValue;
            }
        }
    }



}

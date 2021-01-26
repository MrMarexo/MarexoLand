using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunManager : MonoBehaviour
{
    //run name slots
    [SerializeField] TextMeshProUGUI slot0;
    [SerializeField] TextMeshProUGUI slot1;
    [SerializeField] TextMeshProUGUI slot2;
    [SerializeField] TextMeshProUGUI slot3;
    [SerializeField] TextMeshProUGUI slot4;

    [SerializeField] GameObject runTablePopup;

    [SerializeField] GameObject noEmptyPopup;

    [SerializeField] GameObject alreadyActivePopup;

    [SerializeField] TextMeshProUGUI loadButton;

    TextMeshProUGUI[] slots = new TextMeshProUGUI[5];

    ValueManagement vM;
    DateManagement dM;
    SceneLoader sL;
    PopupManagement pM;

    string[] runDates;
    bool[] runsFinishedState;

    int selectedIndex;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        dM = FindObjectOfType<DateManagement>();
        sL = FindObjectOfType<SceneLoader>();
        pM = FindObjectOfType<PopupManagement>();
        runDates = vM.GetRunDates();
        runsFinishedState = vM.GetRunsFinishedState();

        slots[0] = slot0;
        slots[1] = slot1;
        slots[2] = slot2;
        slots[3] = slot3;
        slots[4] = slot4;
    }

    public void ShowRunSlots()
    { 
        for(int i = 0; i < runDates.Length; i++) 
        {
            var cur = runDates[i];
            slots[i].color = Colors.greyColor;
            if(cur == "")
            {
                slots[i].text = "Empty slot";
                slots[i].GetComponent<Button>().enabled = false;
            }
            else
            {
                string finishStatus;
                if (vM.GetRunsFinishedState()[i] == true)
                {
                    finishStatus = " - finished";
                }
                else
                {
                    finishStatus = " - ongoing";
                }
                slots[i].text = cur + " - " + vM.GetBadHabitNameFromRun(i) + finishStatus;
                Debug.Log("index of req run = " + i);
                Debug.Log(PlayerPrefs.GetString("badHabitName0","default"));
            }
        }
        loadButton.color = Colors.greyColor;
        loadButton.GetComponent<Button>().enabled = false;
        pM.EnablePopup(runTablePopup);
    }

    //finds the next empty slot and prepares the dM and vM and runs Intro
    public void StartNewRun()
    {
        var listOfEmpty = GetListOfEmptyRuns();
        var listOfActive = GetListOfActiveRuns();
        if (listOfEmpty.Count == 0)
        {
            FindObjectOfType<PopupManagement>().EnablePopup(noEmptyPopup);
            return;
        }

        //choose the first empty index
        int index = listOfEmpty[0];

        //will only set the curDayIndex and curWeekIndex to 0 for the Intro scenes
        dM.SetIndexes(index);

        //will be empty now
        vM.SetRunIndexForIntro(index);

        if (index == 0)
        {
            sL.LoadSceneByName("Intro");
        }
        else
        {
            sL.LoadSceneByName("Intro Again");
        }
        
        return;
    }

    List<int> GetListOfActiveRuns()
    {
        //finds open runs == runs that have started but not have yet finished
        List<int> listOfActiveRuns = new List<int>();
        
        for (int i = 0; i < runsFinishedState.Length; i++)
        {
            if (runDates[i] != "" && runsFinishedState[i] == false)
            {
                listOfActiveRuns.Add(i);
            }
        }
        return listOfActiveRuns;
    }

    List<int> GetListOfEmptyRuns()
    {
        //finds empty runs == runs that have not yet started
        List<int> listOfEmptyRuns = new List<int>();
        for (int i = 0; i < runDates.Length; i++)
        {
            var cur = runDates[i];
            if (cur == "")
            {
                listOfEmptyRuns.Add(i);
            }
        }
        return listOfEmptyRuns;
    }

    public void ContinueRun()
    {
        var listOfActiveRuns = GetListOfActiveRuns();
        if (listOfActiveRuns.Count == 0)
        {
            Debug.LogWarning("no open runs --- Continue should not have been shown");
        }
        else if (listOfActiveRuns.Count == 1)
        {
            dM.LoadDateOrSetDate(listOfActiveRuns[0]);
            vM.LoadCurrentValuesFromPrefs(listOfActiveRuns[0]);
            sL.LoadSceneByName("Before Check");
            return;
        }
        else
        {
            ShowRunSlots();
        }

    }

    public string StartOrContinue()
    {
        var listOfActive = GetListOfActiveRuns();
        if (listOfActive.Count == 0)
        {
            return "Start";
        }
        else
        {
            return "Continue";
        }
    }

    public void ToggleElement()
    {
        TextMeshProUGUI buttonPressed = EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>();
        Toggle(buttonPressed);
    }

    void Toggle(TextMeshProUGUI button)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var cur = slots[i];
            if (cur == button)
            {
                cur.color = Colors.completeColor;
            }
            else
            {
                cur.color = Colors.greyColor;
            }
        }
    }

    //sits on the load button
    public void ContinueWithSelectedIndex()
    {
        dM.LoadDateOrSetDate(selectedIndex);
        vM.LoadCurrentValuesFromPrefs(selectedIndex);
        sL.LoadSceneByName("Before Check");
    }

    //sits on the run choices
    public void RunButton()
    {
        TextMeshProUGUI buttonPressed = EventSystem.current.currentSelectedGameObject.GetComponent<TextMeshProUGUI>();

        //only after a choice is made the load button is made active
        loadButton.color = Colors.completeColor;
        loadButton.GetComponent<Button>().enabled = true;

        for (int i = 0; i < slots.Length; i++)
        {
            var cur = slots[i];
            if (cur == buttonPressed)
            {
                selectedIndex = i;
            }
        }
    }

    public void ClosePopup()
    {
        Debug.Log("run manager close popup runs");
        pM.DisablePopup();
    }


}



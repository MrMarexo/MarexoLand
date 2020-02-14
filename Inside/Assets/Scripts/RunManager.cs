using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunManager : MonoBehaviour
{
    //run name slots
    [SerializeField] TextMeshProUGUI slot0;
    [SerializeField] TextMeshProUGUI slot1;
    [SerializeField] TextMeshProUGUI slot2;
    [SerializeField] TextMeshProUGUI slot3;
    [SerializeField] TextMeshProUGUI slot4;

    [SerializeField] GameObject runTablePopup;

    TextMeshProUGUI[] slots;


    ValueManagement vM;
    DateManagement dM;
    SceneLoader sL;

    string[] runDates;
    bool[] runsFinishedState;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
        dM = FindObjectOfType<DateManagement>();
        sL = FindObjectOfType<SceneLoader>();
        runDates = vM.GetRunDates();
        runsFinishedState = vM.GetRunsFinishedState();
    }

    public void ShowRunSlots()
    {
        slots[0] = slot0;
        slots[1] = slot1;
        slots[2] = slot2;
        slots[3] = slot3;
        slots[4] = slot4;

        for(int i = 0; i < runDates.Length; i++) 
        {
            var cur = runDates[i];
            if(cur == "")
            {
                slots[i].text = "Empty slot";
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
            }
        }
        FindObjectOfType<PopupManagement>().EnablePopup(runTablePopup);

    }

    public void StartNewRun()
    {
        for (int i = 0; i < runDates.Length; i++)
        {
            var cur = runDates[i];
            if (cur == "")
            {
                dM.LoadDateOrSetDate(i);
                vM.LoadCurrentValuesFromPrefs(i);
                sL.LoadSceneByName("Before Check");
                return;
            }
        }
    }

    public void ContinueRun()
    {
        //finds open runs == runs that have started but not have yet finished
        List<int> listOfOpenRuns = new List<int>();
        for (int i = 0; i < runsFinishedState.Length; i++)
        {
            if (runDates[i] != "" && runsFinishedState[i] == false)
            {
                listOfOpenRuns.Add(i);
            }
        }

        if (listOfOpenRuns.Count == 0)
        {
            Debug.LogWarning("no open runs --- continue should not have been run");
        }
        else if (listOfOpenRuns.Count == 1)
        {
            dM.LoadDateOrSetDate(listOfOpenRuns[0]);
            vM.LoadCurrentValuesFromPrefs(listOfOpenRuns[0]);
            sL.LoadSceneByName("Before Check");
            return;
        }

    }
}



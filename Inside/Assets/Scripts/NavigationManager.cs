using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI day;

    SceneLoader sL;

    private void Start()
    {
        Init();
        sL = FindObjectOfType<SceneLoader>();

    }

    private void Init()
    {
        if (day != null)
        {
            day.text = "Day " + (FindObjectOfType<DateManagement>().GetCurrentDayIndex() + 1).ToString();
        }
    }

    public void GoToJournal()
    {
        if (sL.GetCurrentSceneIndex() < 4)
        {
            return;
        }
        sL.SaveSceneIndex();
        sL.LoadSceneByName("Journal");
    }

    public void GoToCalendar()
    {
        if (sL.GetCurrentSceneIndex() < 4)
        {
            return;
        }
        sL.LoadSceneByName("Calendar");
    }

    public void GoToMenu()
    {
        sL.LoadSceneByName("Menu");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI day;

    private void Start()
    {
        Init();

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
        FindObjectOfType<SceneLoader>().LoadSceneByName("Journal");
    }

    public void GoToCalendar()
    {
        FindObjectOfType<SceneLoader>().LoadSceneByName("Calendar");
    }

    public void GoToMenu()
    {
        FindObjectOfType<SceneLoader>().LoadSceneByName("Menu");
    }

}

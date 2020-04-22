using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FakeCalendarManager : MonoBehaviour
{
    ValueManagement vM;

    [SerializeField] TextMeshProUGUI habit;
    [SerializeField] TextMeshProUGUI weekly;
    [SerializeField] TextMeshProUGUI first;

    [SerializeField] TextMeshProUGUI pointsNumberDay;

    [SerializeField] TextMeshProUGUI checkpointNumberDay;
    [SerializeField] TextMeshProUGUI plusCheckpointDay;
    [SerializeField] TextMeshProUGUI minusCheckpointDay;

    [SerializeField] TextMeshProUGUI slowdownNumberDay;
    [SerializeField] TextMeshProUGUI plusSlowdownDay;
    [SerializeField] TextMeshProUGUI minusSlowdownDay;

    [SerializeField] TextMeshProUGUI insteadNumberDay;
    [SerializeField] TextMeshProUGUI plusInsteadDay;
    [SerializeField] TextMeshProUGUI minusInsteadDay;

    [SerializeField] GameObject dayPopup;

    int points = 45;

    int checkpointsBought = 0;
    int slowdownsBought = 0;
    int insteadsBought = 0;

    //prices
    [SerializeField] int checkpointPrice = 5;
    [SerializeField] int slowdownPrice = 5;
    [SerializeField] int insteadPrice = 5;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();

        string[] values = vM.GetNames();
        first.text = values[1];
        weekly.text = values[2];
        habit.text = values[0];

        UpdateCheckpoints();
        UpdateSlowdowns();
        UpdateInsteads();
    }


    void UpdatePoints()
    {
        pointsNumberDay.text = points.ToString();
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

        if (checkpointsBought == 3 || points - checkpointPrice < 0)
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
        if (points - checkpointPrice >= 0 && checkpointsBought < 3)
        {
            ++checkpointsBought;
            points -= checkpointPrice;
            UpdateCheckpoints();
        }

    }

    public void SubtractCheckpoint()
    {
        if (checkpointsBought > 0)
        {
            --checkpointsBought;
            points += checkpointPrice;
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

        if (insteadsBought == 3 || points - insteadPrice < 0)
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
        if (points - insteadPrice >= 0 && insteadsBought < 3)
        {
            ++insteadsBought;
            points -= insteadPrice;
            UpdateInsteads();
        }

    }

    public void SubtractInstead()
    {
        if (insteadsBought > 0)
        {
            --insteadsBought;
            points += insteadPrice;
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

        if (slowdownsBought == 3 || points - slowdownPrice < 0)
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
        if (points - slowdownPrice >= 0 && slowdownsBought < 3)
        {
            ++slowdownsBought;
            points -= slowdownPrice;
            UpdateSlowdowns();
        }

    }

    public void SubtractSlowdownd()
    {
        if (slowdownsBought > 0)
        {
            --slowdownsBought;
            points += slowdownPrice;
            UpdateSlowdowns();
        }

    }

    public void ClosePopup()
    {
        FindObjectOfType<PopupManagement>().DisablePopup();
    }

    public void OpenZeroDay()
    {
        FindObjectOfType<PopupManagement>().EnablePopup(dayPopup);
    }

    public void StartLevel()
    {
        vM.SaveBoughtCheckpointsForLevel(checkpointsBought);
        vM.SaveBoughtInsteadsForLevel(insteadsBought);
        vM.SaveBoughtSlowdownsForLevel(slowdownsBought);
        FindObjectOfType<SceneLoader>().LoadSceneByName("Day 0");
    }
}

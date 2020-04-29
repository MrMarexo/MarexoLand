using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willpower : MonoBehaviour
{
    [SerializeField] int willpower = 100;

    //int willpower;
    int state;

    private void Start()
    {
        //willpower = PlayerPrefs.GetInt("willpower", 100);
        UpdateWillpowerState();
    }

    void UpdateWillpowerDaily()
    {
        int dailyOffset = Random.Range(13, 24);
        willpower = -dailyOffset;
        UpdateWillpowerState();
    }

    void UpdateWillpowerState()
    {
        if (willpower < 5)
        {
            state = 5;
        }
        else if (willpower < 15)
        {
            state = 4;
        }
        else if (willpower < 30)
        {
            state = 3;
        }
        else if (willpower < 50)
        {
            state = 2;
        }
        else if (willpower < 70)
        {
            state = 1;
        }
        else
        {
            state = 0;
        }
    }

    public int GetWillpowerState()
    {
        Debug.Log(willpower);
        UpdateWillpowerState();
        return state;
    }

    public void IncreaseWillpower(int amount)
    {
        willpower += amount;
        if (willpower > 100) willpower = 100;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willpower : MonoBehaviour
{
    int willpower;

    int state;

    private void Awake()
    {
        willpower = PlayerPrefs.GetInt("willpower", 100);

        //for testing only
        willpower = 100;
        ///////////////
        
        UpdateWillpowerState();

    }

    public void UpdateWillpowerDaily(int daysPassed)
    {
        int dailyOffset = Random.Range(13, 24);
        willpower = -dailyOffset * daysPassed;
        if (willpower < 0) willpower = 0;
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
        Debug.Log(state);
        SaveWillpower();
    }
    
    public int GetWillpowerState()
    {
        UpdateWillpowerState();
        return state;
    }

    public int GetWillpower()
    {
        SaveWillpower();
        return willpower;
    }

    public void SetWillpower(int power)
    {
        willpower = power;
        SaveWillpower();
    }

    public void IncreaseWillpower(int amount)
    {
        willpower += amount;
        if (willpower > 100) willpower = 100;
    }

    void SaveWillpower()
    {
        PlayerPrefs.SetInt("willpower", willpower);
    }



}

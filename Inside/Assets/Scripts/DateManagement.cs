using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateManagement : MonoBehaviour
{
    System.DateTime curDate;

    //launch date retrieved from PlayerPrefs and transformed back to DateTime
    System.DateTime savedLaunchDate;

    //how many days from launch date, launch date being 1
    int daysInWar = 0;

    //how many days from launch date, launch date being 0 ---for arrays
    int curIndexInArray = 0;

    //week arrays
    System.DateTime[] firstWeek = new System.DateTime[7];
    System.DateTime[] secondWeek = new System.DateTime[7];
    System.DateTime[] thirdWeek = new System.DateTime[7];
    System.DateTime[] fourthWeek = new System.DateTime[7];
    System.DateTime[] lastTwoDays = new System.DateTime[2];

    //array of week arrays
    System.DateTime[][] arrayOfWeeks = new System.DateTime[5][];

    //index in arrayOfWeeks
    int indexWeeksArray;

    //array of calendar gameobjects
    [SerializeField] TextMeshProUGUI[] arrayOfDays;

    void Start()
    {
        Date();
        FillArrayOfWeeks();
        CreateCalendar();
    }

    //populate the arrayOfWeeks
    void FillArrayOfWeeks()
    {
        arrayOfWeeks[0] = firstWeek;
        arrayOfWeeks[1] = secondWeek;
        arrayOfWeeks[2] = thirdWeek;
        arrayOfWeeks[3] = fourthWeek;
        arrayOfWeeks[4] = lastTwoDays;
    }


    
    //used to define the launch date and current date and save/get from PlayerPrefs
    void Date()
    {
        curDate = System.DateTime.Now.Date;

        //for now ---for testing
        savedLaunchDate = curDate.AddDays(-5);


        curIndexInArray = (curDate - savedLaunchDate).Days;
        daysInWar = curIndexInArray + 1;

    }

    void CreateCalendar()
    {
        PopulateArrays();

        if (curIndexInArray < 7)
        {
            indexWeeksArray = 0;
        }
        else if (curIndexInArray < 14)
        {
            indexWeeksArray = 1;
        }
        else if (curIndexInArray < 21)
        {
            indexWeeksArray = 2;
        }
        else if (curIndexInArray < 28)
        {
            indexWeeksArray = 3;
        }
        else if (curIndexInArray < 30)
        {
            indexWeeksArray = 4;
        }




    }

    void PopulateArrays()
    {
        for (int i = 0; i < 30; i++)
        {
            var cur = savedLaunchDate.AddDays(i);
            if (i < 7)
            {
                firstWeek[i] = cur;
            }
            else if (i < 14)
            {
                secondWeek[i - 7] = cur;
            }
            else if (i < 21)
            {
                thirdWeek[i - 14] = cur;
            }
            else if (i < 28)
            {
                fourthWeek[i - 21] = cur;
            }
            else if (i < 30)
            {
                lastTwoDays[i - 28] = cur;
            }
        }

        //testing purposes

        //for (int i = 0; i < lastTwoDays.Length; i++)
        //{
        //    Debug.Log(lastTwoDays[i]);
        //}
    }


}

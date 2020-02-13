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

    TextMeshProUGUI[] slots;

    ValueManagement vM;

    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();
    }

    public void ShowRunSlots()
    {
        slots[0] = slot0;
        slots[1] = slot1;
        slots[2] = slot2;
        slots[3] = slot3;
        slots[4] = slot4;

        string[] runDates = vM.GetRunDates();

        for(int i = 0; i < runDates.Length; i++) 
        {
            var cur = runDates[i];
            if(cur == "")
            {
                slots[i].text = "Empty slot";
            }
            else
            { 
                slots[i].text = cur + " - " + vM.GetBadHabitNameFromRun(i);
            }
        }
    }
}



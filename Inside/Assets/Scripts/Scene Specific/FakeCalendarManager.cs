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

    [SerializeField] GameObject dayPopup;


    private void Start()
    {
        vM = FindObjectOfType<ValueManagement>();

        string[] values = vM.GetNames();
        first.text = values[1];
        weekly.text = values[2];
        habit.text = values[0];
        
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
        FindObjectOfType<SceneLoader>().LoadSceneByName("0");
    }
}

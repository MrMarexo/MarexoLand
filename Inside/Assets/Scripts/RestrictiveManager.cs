using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictiveManager : MonoBehaviour
{
    PopupManagement pM;

    [SerializeField] GameObject calendarRestrictionPopup;

    private void Start()
    {
        pM = FindObjectOfType<PopupManagement>();
    }

    public void CalendarRestrictionPopup()
    {
        pM.EnablePopup(calendarRestrictionPopup);
    }

    public void ClosePopup()
    {
        pM.DisablePopup();
    }
}

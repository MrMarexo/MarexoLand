using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Toggle : MonoBehaviour
{
    //toggles between two buttons
    void ToggleTwo(int activeIndex, GameObject parentButton)
    {
        int inactiveIndex = Mathf.Abs(activeIndex - 1);
        parentButton.GetComponentsInChildren<TextMeshProUGUI>()[inactiveIndex].color = Colors.toggleGrayColor;
        parentButton.GetComponentsInChildren<TextMeshProUGUI>()[activeIndex].color = Colors.completeColor;
    }

    public void FirstButton()
    {
        GameObject parentOfButton = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        ToggleTwo(0, parentOfButton);
    }

    public void SecondButton()
    {
        GameObject parentOfButton = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        ToggleTwo(1, parentOfButton);
    }
}

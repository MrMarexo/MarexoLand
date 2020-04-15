using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    //toggles between two buttons
    void ToggleTwo(int activeIndex, GameObject parentButton)
    {
        int inactiveIndex = Mathf.Abs(activeIndex - 1);
        parentButton.GetComponentsInChildren<Image>()[inactiveIndex].color = Colors.toggleGrayColor;
        parentButton.GetComponentsInChildren<Image>()[inactiveIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        parentButton.GetComponentsInChildren<Image>()[activeIndex].color = Colors.completeColor;
        parentButton.GetComponentsInChildren<Image>()[activeIndex].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
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

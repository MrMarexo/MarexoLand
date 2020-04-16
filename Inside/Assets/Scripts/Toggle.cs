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
        var image = parentButton.GetComponentInChildren<Image>();
        var images = parentButton.GetComponentsInChildren<Image>();
        var texts = parentButton.GetComponentsInChildren<TextMeshProUGUI>();
        if (image)
        {
            images[inactiveIndex].color = Colors.toggleGrayColor;
            images[inactiveIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            images[activeIndex].color = Colors.completeColor;
            images[activeIndex].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }
        else
        {
            texts[inactiveIndex].color = Colors.toggleGrayColor;
            texts[inactiveIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            texts[activeIndex].color = Colors.completeColor;
            texts[activeIndex].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }
        
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

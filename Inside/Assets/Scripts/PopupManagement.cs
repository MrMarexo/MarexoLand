using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManagement : MonoBehaviour
{
    //list of popups
    List<GameObject> popups = new List<GameObject>();

    void Start()
    {
        FindAllPopups();
        DisableAllPopups();
    }

    void FindAllPopups()
    {
        //finds all canvases in a scene and if their tag is popup, it adds them in the popups list
        Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i].gameObject.tag == "Popup")
            {
                popups.Add(canvases[i].gameObject);
            }
        }
    }

    void DisableAllPopups()
    {
        foreach (GameObject popup in popups)
        {
            popup.SetActive(false);
        }
    }

    public GameObject GetPopup()
    {
        return popups[0];
    }

}

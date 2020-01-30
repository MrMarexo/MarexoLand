using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupManagement : MonoBehaviour
{
    //list of popups
    List<GameObject> popups = new List<GameObject>();
    List<GameObject> popupPanels = new List<GameObject>();

    float timeToLoadPopup;

    int unifiedIndex = 0;

    void Start()
    {
        FindAllPopups();
        DisableAllPopups();
        timeToLoadPopup = FindObjectOfType<SceneManagement>().GetTimeToLoad();
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

        //finds all images(panels) in a scene and if their tag is popup, it adds them in the popups list
        Image[] images = Resources.FindObjectsOfTypeAll<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].gameObject.tag == "Popup")
            {
                popupPanels.Add(images[i].gameObject);
            }
        }
    }

    //disables all popups and popup panels
    void DisableAllPopups()
    {
        foreach (GameObject popup in popups)
        {
            popup.SetActive(false);
        }
        foreach (GameObject panel in popupPanels)
        {
            panel.SetActive(false);
        }

    }
    
    public void EnablePopup()
    {
        StartCoroutine(LoadPopup(0, true));
    }

    public void DisablePopup()
    {
        StartCoroutine(LoadPopup(0, false));
    }

    IEnumerator LoadPopup(int index, bool enableDisable)
    {
        yield return new WaitForSecondsRealtime(timeToLoadPopup);
        popups[index].SetActive(enableDisable);
        popupPanels[index].SetActive(enableDisable);
    }

    
}

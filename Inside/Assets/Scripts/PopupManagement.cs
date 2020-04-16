using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupManagement : MonoBehaviour
{
    //list of popups
    List<GameObject> popups = new List<GameObject>();
    [SerializeField] GameObject popupPanel;

    float timeToLoadPopup;

    [SerializeField] float timeToLoadCanvas = 0.7f;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        FindAllPopups();
        DisableAllPopups();
        timeToLoadPopup = FindObjectOfType<SceneLoader>().GetTimeToLoad();
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

        //does not work for some fucked up reason

        ////finds the popup panel
        //Image[] images = Resources.FindObjectsOfTypeAll<Image>();
        //for (int i = 0; i < images.Length; i++)
        //{
        //    if (images[i].gameObject.tag == "Popup")
        //    {
        //        popupPanel = images[i].gameObject;
        //    }
        //}

    }

    //disables all popups and the popup panel
    void DisableAllPopups()
    {
        foreach (GameObject popup in popups)
        {
            popup.SetActive(false);
        }

        popupPanel.SetActive(false);
    }
    
    public void EnablePopup(GameObject popup)
    {
        StartCoroutine(LoadPopup(popup, true, timeToLoadPopup));
    }

    public void DisablePopup()
    {
        GameObject popup = EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject;
        StartCoroutine(LoadPopup(popup, false, timeToLoadPopup));
    }

    public void EnableGameCanvas(GameObject popup)
    {
        StartCoroutine(LoadPopup(popup, true, timeToLoadCanvas));
    }

    IEnumerator LoadPopup(GameObject popup, bool enableDisable, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        popup.SetActive(enableDisable);
        popupPanel.SetActive(enableDisable);
    }

    
}

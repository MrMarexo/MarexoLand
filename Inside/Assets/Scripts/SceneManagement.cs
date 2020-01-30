using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] float timeToLoadScene = 0.1f;
    

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSecondsRealtime(timeToLoadScene);
        SceneManager.LoadScene(index);
    }


    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        //finds the canvas where the button is
        GameObject parentCanvas = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;

        //if there is an empty input component dont allow to load the next scene
        var inputComponent = parentCanvas.GetComponentInChildren<TMP_InputField>();

        //there also needs to be a popup management script present
        var popupMgmt = FindObjectOfType<PopupManagement>();
        if (inputComponent && popupMgmt)
        {
            //check if its text is not ""
            if (inputComponent.text == "")
            {
                //enable a pop-up canvas that prompts the user to input sth and return
                popupMgmt.EnablePopup();
                return;
            }
        }
        StartCoroutine(LoadScene(currentIndex + 1));     
    }

    public float GetTimeToLoad()
    {
        return timeToLoadScene;
    }

}

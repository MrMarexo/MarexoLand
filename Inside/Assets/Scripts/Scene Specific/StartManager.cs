using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] letters = new TextMeshProUGUI[7];

    [SerializeField] float blinkTime = 0.1f;

    [SerializeField] TextMeshProUGUI startButton;
    [SerializeField] TextMeshProUGUI continueButton;

    bool loopFinished = true;


    private void Start()
    {
        StartOrContinue(); 
    }

    void Update()
    {
        if (loopFinished)
        {
            RandomFlashing();
        }
    }

    void StartOrContinue()
    {
        //disable both buttons
        startButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        if (FindObjectOfType<ValueManagement>().GetNames()[2] == "")
        {
            startButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    void RandomFlashing()
    {
        var randomLetter = letters[Mathf.FloorToInt(Random.Range(0f, letters.Length))];
        var randomWaitTime = Random.Range(0.2f, 1f);
        StartCoroutine(Loop(randomLetter, randomWaitTime));
    }

    IEnumerator Loop(TextMeshProUGUI letter, float time)
    {
        loopFinished = false;
        yield return new WaitForSecondsRealtime(time);
        letter.color = Colors.incompleteColor;
        yield return new WaitForSecondsRealtime(blinkTime);
        letter.color = Colors.completeColor;
        loopFinished = true;
    }


    public void StartButton()
    {
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }

    public void ContinueButton()
    {
        string[] sceneNames = { "Calendar", "Today Check", "Yesterday Check" };
        string scenetoLoad;
        var vM = FindObjectOfType<ValueManagement>();
        int curDayIndex = FindObjectOfType<DateManagement>().GetCurrentDayIndex();
        string[] todayValues = vM.GetValuesOfIndex(curDayIndex); //0 is first, 1 is second, 2 is habit

        if (curDayIndex > 2)
        {
            string[] beforeValues = vM.GetValuesOfIndex(curDayIndex - 2); //0 is first, 1 is second, 2 is habit
            if (beforeValues[0] == "" || beforeValues[2] == "")
            {
                ////////////////do a popup saying its been too long and you need to start over again
            }
        }
        if (curDayIndex == 0) //launch day
        {
            if (todayValues[0] == "" || todayValues[2] == "")
            {
                scenetoLoad = sceneNames[1];
            }
            else
            {
                scenetoLoad = sceneNames[0];
            }
        }
        else
        {
            string[] yesterdayValues = vM.GetValuesOfIndex(curDayIndex - 1); //0 is first, 1 is second, 2 is habit
            if (yesterdayValues[0] == "" || yesterdayValues[2] == "")
            {
                scenetoLoad = sceneNames[2];
            }
            else if (todayValues[0] == "" || todayValues[2] == "")
            {
                scenetoLoad = sceneNames[1];
            }
            else
            {
                scenetoLoad = sceneNames[0];
            }
        }
        //load chosen scene
        FindObjectOfType<SceneLoader>().LoadSceneByName(scenetoLoad);

    }

   
}

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

    RunManager rM;

    bool loopFinished = true;


    private void Start()
    {
        rM = FindObjectOfType<RunManager>();
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

        string result = rM.StartOrContinue();
        if (result == "Start")
        {
            startButton.gameObject.SetActive(true);
        }
        else if (result == "Continue")
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("OOOPs, something is wrong!");
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
        rM.StartNewRun();
    }

    public void ContinueButton()
    {
        rM.ContinueRun();
    }

    //letter buttons

    public void LoadInfo()
    {
        FindObjectOfType<SceneLoader>().LoadSceneByName("Info");
    }

    public void LoadNewRun()
    {
        rM.ShowRunSlots();
    }

   
}

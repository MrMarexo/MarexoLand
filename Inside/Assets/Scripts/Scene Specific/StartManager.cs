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
        string[] names = FindObjectOfType<ValueManagement>().GetNames();
        if (names[0] == "" || names[1] == "" || names[2] == "")
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
        FindObjectOfType<SceneLoader>().LoadSceneByName("Before Check");
    }

   
}

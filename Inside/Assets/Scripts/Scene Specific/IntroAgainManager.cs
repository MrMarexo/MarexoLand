using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAgainManager : MonoBehaviour
{
    public void LoadNext()
    {
        FindObjectOfType<SceneLoader>().LoadSceneByName("Bad Habit");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public void GoBack()
    {
        FindObjectOfType<SceneLoader>().LoadPastScene();
    }
}

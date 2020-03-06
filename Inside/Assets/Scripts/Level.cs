using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public void ReloadLevel()
    {
        FindObjectOfType<SceneLoader>().ReloadCurrentScene();
    }


}

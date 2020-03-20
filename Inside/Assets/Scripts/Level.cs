using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    PlayerMovement mov;
    SceneLoader sL;
    Player pl;

    [SerializeField] TextMeshProUGUI keyText;
    [SerializeField] TextMeshProUGUI checkpointText;

    [SerializeField] TextMeshProUGUI checkLoad;

    int checkpointAllowed = 1;
    int checkpointCount = 0;


    private void Start()
    {
        checkpointCount = PlayerPrefs.GetInt("checkpointCount", 0);

        keyText.color = keyText.color = Colors.semiTransparentColor;
        ShouldShowCheckpoint();
        ShouldShowLoadCheckpoint();
        mov = FindObjectOfType<PlayerMovement>();
        sL = FindObjectOfType<SceneLoader>();
        pl = FindObjectOfType<Player>();
    }
    
    public void ReloadLevel()
    {
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.ResetPrefs();
        }
        mov.ResetPrefs();
        pl.DeleteKey();
        DeletePrefCount();
        sL.ReloadCurrentScene();
    }

    public void SaveCheckpoint()
    {
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.SavePositionToPrefs();
        }
        mov.SaveCheckpointLocation();
        pl.SaveKey();

        ++checkpointCount;
        SaveCheckpointCount();

        ShouldShowCheckpoint();
        ShouldShowLoadCheckpoint();
    }

    public void LoadFromCheckpoint()
    {
        sL.ReloadCurrentScene();
    }

    public void BackToMenu()
    {
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.ResetPrefs();
        }
        mov.ResetPrefs();
        pl.DeleteKey();
        DeletePrefCount();
        sL.LoadSceneByName("Menu");
    }

    public void UpdateKeyText(bool gotKey)
    {
        if (gotKey)
        {
            keyText.color = Colors.completeColor;
        }
        else
        {
            keyText.color = keyText.color = Colors.semiTransparentColor;
        }
         
    }

    void ShouldShowCheckpoint()
    {
        Debug.Log("show");
        if (IsCheckpointAllowed())
        {
            Debug.Log("show true");
            checkpointText.color = Colors.completeColor;
            checkpointText.gameObject.GetComponent<Button>().enabled = true;
        }
        else
        {
            Debug.Log("show false");
            checkpointText.color = Colors.semiTransparentColor;
            checkpointText.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    bool IsCheckpointAllowed()
    {
        return checkpointCount < checkpointAllowed;
    }

    void SaveCheckpointCount()
    {
        PlayerPrefs.SetInt("checkpointCount", checkpointCount);
    }

    void DeletePrefCount()
    {
        PlayerPrefs.DeleteKey("checkpointCount");
    }

    void ShouldShowLoadCheckpoint()
    {
        if (checkpointCount > 0)
        {
            checkLoad.color = Colors.completeColor;
            checkLoad.gameObject.GetComponent<Button>().enabled = true;
        }
        else
        {
            checkLoad.color = Colors.semiTransparentColor;
            checkLoad.gameObject.GetComponent<Button>().enabled = false;
        }
    }



}

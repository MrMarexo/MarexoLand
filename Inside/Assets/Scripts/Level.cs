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

    [SerializeField] TextMeshProUGUI checkSlot1;
    [SerializeField] TextMeshProUGUI checkSlot2;
    [SerializeField] TextMeshProUGUI checkSlot3;

    [SerializeField] TextMeshProUGUI dayNumber;

    int checkpointAllowed = 1;
    int checkpointCount = 0;


    private void Start()
    {
        checkpointCount = PlayerPrefs.GetInt("checkpointCount", 0);
        keyText.color = keyText.color = Colors.semiTransparentColor;
        ShouldShowCheckpoint();
        ShouldShowLoadCheckpoint();
        UpdateCheckSlots();
        mov = FindObjectOfType<PlayerMovement>();
        sL = FindObjectOfType<SceneLoader>();
        pl = FindObjectOfType<Player>();
        dayNumber.text = sL.GetCurrentSceneName();
    }

    void UpdateCheckSlots()
    {
        int actualCheckpointSum = checkpointAllowed - checkpointCount;
        if (actualCheckpointSum == 3)
        {
            checkSlot1.color = Colors.completeColor;
            checkSlot2.color = Colors.completeColor;
            checkSlot3.color = Colors.completeColor;
        } 
        else if (actualCheckpointSum == 2)
        {
            checkSlot1.color = Colors.completeColor;
            checkSlot2.color = Colors.completeColor;
            checkSlot3.color = Colors.failedColor;
        }
        else if (actualCheckpointSum == 1)
        {
            checkSlot1.color = Colors.completeColor;
            checkSlot2.color = Colors.failedColor;
            checkSlot3.color = Colors.failedColor;
        }
        else if (actualCheckpointSum == 0)
        {
            checkSlot1.color = Colors.failedColor;
            checkSlot2.color = Colors.failedColor;
            checkSlot3.color = Colors.failedColor;
        }
        else
        {
            Debug.LogError("invalid number of checkpoints: " + actualCheckpointSum);
        }
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
        UpdateCheckSlots();
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
        sL.LoadSceneByName("Calendar");
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
        if (IsCheckpointAllowed())
        {
            checkpointText.color = Colors.completeColor;
            checkpointText.gameObject.GetComponent<Button>().enabled = true;
        }
        else
        {
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

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
    ValueManagement vM;

    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

    [SerializeField] TextMeshProUGUI keyText;
    [SerializeField] TextMeshProUGUI checkpointText;

    [SerializeField] TextMeshProUGUI checkLoad;

    [SerializeField] TextMeshProUGUI checkSlot1;
    [SerializeField] TextMeshProUGUI checkSlot2;
    [SerializeField] TextMeshProUGUI checkSlot3;

    [SerializeField] TextMeshProUGUI dayNumber;

    [SerializeField] Image keyImage;

    [SerializeField] float timeSlowedDown = 3f;
    [SerializeField] float slowDownRatio = 2.5f;

    int checkpointAllowed = 0;
    int checkpointCount = 0;

    bool insteadWorks = false;


    private void Start()
    {
        mov = FindObjectOfType<PlayerMovement>();
        sL = FindObjectOfType<SceneLoader>();
        pl = FindObjectOfType<Player>();
        vM = FindObjectOfType<ValueManagement>();


        TurnOffDev();
        ShowUI();
        //for testing
        //checkpointAllowed = vM.GetBoughtCheckpoints();
        checkpointAllowed = 3;
        checkpointCount = PlayerPrefs.GetInt("checkpointCount", 0);
        keyImage.color = Colors.toggleGrayColor;
        ShouldShowCheckpoint();
        ShouldShowLoadCheckpoint();
        UpdateCheckSlots();
        dayNumber.text = sL.GetCurrentSceneName();
    }

    void TurnOffDev()
    {
        var devLight = GameObject.FindGameObjectWithTag("DevLight");
        if (devLight)
        {
            devLight.SetActive(false);
        }
    }

    void ShowUI()
    {
        var canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas canvas in canvases)
        {
            if (canvas.gameObject.name == "GameCanvas")
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }

    public void ShowWinCanvas()
    {
        FindObjectOfType<PopupManagement>().EnableGameCanvas(winCanvas);
    }

    public void ShowLoseCanvas()
    {
        FindObjectOfType<PopupManagement>().EnableGameCanvas(loseCanvas);
    }

    private void Update()
    {
        if (insteadWorks)
        {
            Instead();
        }
        
    }

    public void SlowDownAbility()
    {

        var pms = FindObjectsOfType<PlatformMover>();
        foreach (PlatformMover pm in pms)
        {
            pm.SlowDown(timeSlowedDown, slowDownRatio);
        }
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.SlowDown(timeSlowedDown, slowDownRatio);
        }
        
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

    public void UpdateKeyImage(bool gotKey)
    {
        if (gotKey)
        {
            keyImage.color = Colors.completeColor;
        }
        else
        {
            keyImage.color = Colors.toggleGrayColor;
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

    public void ClickInstead()
    {
        insteadWorks = true;
    }

    void Instead()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            LayerMask enemyLayer = LayerMask.NameToLayer("Enemy");
            LayerMask wallsLayer = LayerMask.NameToLayer("Walls");
            RaycastHit2D hit = Physics2D.Raycast(touchPos, -Vector2.zero);
            if (hit)
            {
                if (hit.transform.gameObject.layer == enemyLayer)
                {
                    var go = hit.transform.gameObject;
                    go.GetComponent<SpriteRenderer>().color = Colors.completeColor;
                    go.layer = wallsLayer;
                    Debug.Log("name: " + hit.transform.name);
                    

                    insteadWorks = false;
                }
            }
            
        }

    }



}

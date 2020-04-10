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

    [SerializeField] TextMeshProUGUI checkpointText;
    [SerializeField] TextMeshProUGUI insteadText;
    [SerializeField] TextMeshProUGUI slowdownText;

    [SerializeField] Image checkpointButton;
    [SerializeField] Image insteadButton;
    [SerializeField] Image slowdownButton;

    [SerializeField] TextMeshProUGUI checkLoad;

    [SerializeField] TextMeshProUGUI checkSlot1;
    [SerializeField] TextMeshProUGUI checkSlot2;
    [SerializeField] TextMeshProUGUI checkSlot3;

    [SerializeField] TextMeshProUGUI insteadSlot1;
    [SerializeField] TextMeshProUGUI insteadSlot2;
    [SerializeField] TextMeshProUGUI insteadSlot3;

    [SerializeField] TextMeshProUGUI slowSlot1;
    [SerializeField] TextMeshProUGUI slowSlot2;
    [SerializeField] TextMeshProUGUI slowSlot3;

    [SerializeField] TextMeshProUGUI dayNumber;

    [SerializeField] Image keyImage;

    [SerializeField] float timeSlowedDown = 3f;
    [SerializeField] float slowDownRatio = 2.5f;

    int checkpointAllowed = 0;
    int checkpointCount = 0;

    int slowAllowed = 0;
    int slowCount = 0;

    int insteadAllowed = 0;
    int insteadCount = 0;

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
        slowAllowed = 3;
        insteadAllowed = 3;
        ///////////////////////
        checkpointCount = PlayerPrefs.GetInt("checkpointCount", 0);
        slowCount = PlayerPrefs.GetInt("slowCount", 0);
        insteadCount = PlayerPrefs.GetInt("insteadCount", 0);

        keyImage.color = Colors.toggleGrayColor;

        ShouldShowInsteadUI();
        UpdateInsteadSlots();

        ShouldShowSlowdownUI();
        UpdateSlowSlots();

        ShouldShowCheckpointUI();
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

        ++slowCount;
        SaveOtherSkillsCount();

        ShouldShowSlowdownUI();
        UpdateSlowSlots();

    }

    void UpdateSlowSlots()
    {
        int actualSlowdownSum = slowAllowed - slowCount;
        if (actualSlowdownSum == 3)
        {
            slowSlot1.color = Colors.completeColor;
            slowSlot2.color = Colors.completeColor;
            slowSlot3.color = Colors.completeColor;
        }
        else if (actualSlowdownSum == 2)
        {
            slowSlot1.color = Colors.completeColor;
            slowSlot2.color = Colors.completeColor;
            slowSlot3.color = Colors.invisibleColor;
        }
        else if (actualSlowdownSum == 1)
        {
            slowSlot1.color = Colors.completeColor;
            slowSlot2.color = Colors.invisibleColor;
            slowSlot3.color = Colors.invisibleColor;
        }
        else if (actualSlowdownSum == 0)
        {
            slowSlot1.color = Colors.invisibleColor;
            slowSlot2.color = Colors.invisibleColor;
            slowSlot3.color = Colors.invisibleColor;
        }
        else
        {
            Debug.LogError("invalid number of slowdowns: " + actualSlowdownSum);
        }
    }

    void UpdateInsteadSlots()
    {
        int actualInsteadSum = insteadAllowed - insteadCount;
        if (actualInsteadSum == 3)
        {
            insteadSlot1.color = Colors.completeColor;
            insteadSlot2.color = Colors.completeColor;
            insteadSlot3.color = Colors.completeColor;
        }
        else if (actualInsteadSum == 2)
        {
            insteadSlot1.color = Colors.completeColor;
            insteadSlot2.color = Colors.completeColor;
            insteadSlot3.color = Colors.invisibleColor;
        }
        else if (actualInsteadSum == 1)
        {
            insteadSlot1.color = Colors.completeColor;
            insteadSlot2.color = Colors.invisibleColor;
            insteadSlot3.color = Colors.invisibleColor;
        }
        else if (actualInsteadSum == 0)
        {
            insteadSlot1.color = Colors.invisibleColor;
            insteadSlot2.color = Colors.invisibleColor;
            insteadSlot3.color = Colors.invisibleColor;
        }
        else
        {
            Debug.LogError("invalid number of insteads: " + actualInsteadSum);
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
            checkSlot3.color = Colors.invisibleColor;
        }
        else if (actualCheckpointSum == 1)
        {
            checkSlot1.color = Colors.completeColor;
            checkSlot2.color = Colors.invisibleColor;
            checkSlot3.color = Colors.invisibleColor;
        }
        else if (actualCheckpointSum == 0)
        {
            checkSlot1.color = Colors.invisibleColor;
            checkSlot2.color = Colors.invisibleColor;
            checkSlot3.color = Colors.invisibleColor;
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

        ShouldShowCheckpointUI();
        ShouldShowLoadCheckpoint();
        UpdateCheckSlots();
    }

    void SaveInsteadCount()
    {
        PlayerPrefs.SetInt("insteadCount", insteadCount);
    }

    void SaveSlowCount()
    {
        PlayerPrefs.SetInt("slowCount", slowCount);
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
            keyImage.color = Colors.buttonTransparentGrey;
        }
         
    }

    void ShouldShowCheckpointUI()
    {
        if (IsCheckpointAllowed())
        {
            checkpointText.color = Colors.completeColor;
            checkpointButton.color = Colors.buttonTransparentWhite;
            checkpointButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            checkpointText.color = Colors.buttonTransparentGrey;
            checkpointButton.color = Colors.buttonTransparentGrey;
            checkpointButton.GetComponent<Button>().enabled = false;
        }
    }

    void ShouldShowSlowdownUI()
    {
        if (IsSlowAllowed())
        {
            slowdownText.color = Colors.completeColor;
            slowdownButton.color = Colors.buttonTransparentWhite;
            slowdownButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            slowdownText.color = Colors.buttonTransparentGrey;
            slowdownButton.color = Colors.buttonTransparentGrey;
            slowdownButton.GetComponent<Button>().enabled = false;
        }
    }

    void ShouldShowInsteadUI()
    {
        if (IsInsteadAllowed())
        {
            insteadText.color = Colors.completeColor;
            insteadButton.color = Colors.buttonTransparentWhite;
            insteadButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            insteadText.color = Colors.buttonTransparentGrey;
            insteadButton.color = Colors.buttonTransparentGrey;
            insteadButton.GetComponent<Button>().enabled = false;
        }
    }
    
    bool IsCheckpointAllowed()
    {
        return checkpointCount < checkpointAllowed;
    }

    bool IsSlowAllowed()
    {
        return slowCount < slowAllowed;
    }

    bool IsInsteadAllowed()
    {
        return insteadCount < insteadAllowed;
    }

    void SaveOtherSkillsCount()
    {
        PlayerPrefs.SetInt("slowCount", slowCount);
        PlayerPrefs.SetInt("insteadCount", insteadCount);
    }

    void SaveCheckpointCount()
    {
        PlayerPrefs.SetInt("checkpointCount", checkpointCount);
    }

    void DeletePrefCount()
    {
        PlayerPrefs.DeleteKey("checkpointCount");
        PlayerPrefs.DeleteKey("slowCount");
        PlayerPrefs.DeleteKey("insteadCount");
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
        //add stoppage of everything else

        ++insteadCount;
        SaveOtherSkillsCount();

        ShouldShowInsteadUI();
        UpdateInsteadSlots();
        

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

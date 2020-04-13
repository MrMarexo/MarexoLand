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
    Background bg;

    LayerMask enemyLayer;
    LayerMask wallsLayer;

    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

    [SerializeField] TextMeshProUGUI checkpointText;
    [SerializeField] TextMeshProUGUI insteadText;
    [SerializeField] TextMeshProUGUI slowdownText;

    [SerializeField] Image checkpointButton;
    [SerializeField] Image insteadButton;
    [SerializeField] Image slowdownButton;

    [SerializeField] TextMeshProUGUI cancelInsteadButton;

    [SerializeField] Image leftButton;
    [SerializeField] Image rightButton;
    [SerializeField] Image jumpButton;

    [SerializeField] Image fakeLeftButton;
    [SerializeField] Image fakeRightButton;
    [SerializeField] Image fakeJumpButton;

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

    List<string> insteadNames = new List<string>();

    int checkpointAllowed = 0;
    int checkpointCount = 0;

    int slowAllowed = 0;
    int slowCount = 0;

    int insteadAllowed = 0;
    int insteadCount = 0;

    bool insteadWorks = false;

    private void Awake()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        wallsLayer = LayerMask.NameToLayer("Walls");
        LoadChangedPlatforms();
    }

    void LoadChangedPlatforms()
    {
        var names = PlayerPrefsX.GetStringArray("insteadNamesArray");
        if (names.Length == 0)
        {
            return;
        }
        else
        {
            var sprites = FindObjectsOfType<SpriteRenderer>();
            var enemies = new List<GameObject>();
            foreach (SpriteRenderer sprite in sprites)
            {
                if (sprite.gameObject.layer == enemyLayer)
                {
                    enemies.Add(sprite.gameObject);
                }
            }
            foreach(GameObject enemy in enemies)
            {
                foreach (string name in names)
                {
                    if (enemy.name == name)
                    {
                        enemy.GetComponent<SpriteRenderer>().color = Colors.completeColor;
                        enemy.layer = wallsLayer;
                    }
                }
            }
        }

    }

    private void Start()
    {
        mov = FindObjectOfType<PlayerMovement>();
        sL = FindObjectOfType<SceneLoader>();
        pl = FindObjectOfType<Player>();
        vM = FindObjectOfType<ValueManagement>();
        bg = FindObjectOfType<Background>();


        TurnOffDev();
        fakeLeftButton.gameObject.SetActive(false);
        fakeRightButton.gameObject.SetActive(false);
        fakeJumpButton.gameObject.SetActive(false);
        cancelInsteadButton.gameObject.SetActive(false);

        ShowUI();
        //for testing
        //checkpointAllowed = vM.GetBoughtCheckpoints();
        //slowAllowed = vM.GetBoughtSlowdowns();
        //insteadAllowed = vM.GetBoughtInsteads();
        checkpointAllowed = 3;
        slowAllowed = 3;
        insteadAllowed = 3;
        ///////////////////////
        checkpointCount = PlayerPrefs.GetInt("checkpointCount", 0);
        slowCount = PlayerPrefs.GetInt("slowCount", 0);
        insteadCount = PlayerPrefs.GetInt("insteadCount", 0);

        keyImage.color = Colors.toggleGrayColor;
        SkillsEnabled();
        ShouldShowInsteadText();
        UpdateInsteadSlots();

        ShouldShowSlowdownText();
        UpdateSlowSlots();

        ShouldShowCheckpointUIAndText();
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

    //called by a button
    public void SlowDownAbility()
    {
        bg.PlayToGrey();
        ++slowCount;
        ShouldShowSlowdownText();
        SaveOtherSkillsCount();
        UpdateSlowSlots();
    }

    //called in animation
    public IEnumerator Slowdown()
    {
        //the actual slowdown of stuff
        var pms = FindObjectsOfType<PlatformMover>();
        foreach (PlatformMover pm in pms)
        {
            pm.SlowDown(slowDownRatio);
        }
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.SlowDown(slowDownRatio);
        }
        ///////////////////////
        SkillsDisabled();

        yield return new WaitForSecondsRealtime(timeSlowedDown);

        foreach (PlatformMover pm in pms)
        {
            pm.SlowUp();
        }
        if (death)
        {
            death.SlowUp();
        }

        bg.PlayGreyToRed();
        
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
        DeletePrefs();
        sL.ReloadCurrentScene();
    }

    public void SaveCheckpoint()
    {
        bg.PlayFlash();
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.SavePositionToPrefs();
        }
        mov.SaveCheckpointLocation();
        pl.SaveKey();
        SaveInsteadIDs();
        ++checkpointCount;
        SaveCheckpointCount();
        ShouldShowCheckpointUIAndText();
        ShouldShowLoadCheckpoint();
        UpdateCheckSlots();
    }

    void SaveInsteadIDs()
    {
        var prevNamesArray = PlayerPrefsX.GetStringArray("insteadNamesArray");
        insteadNames.AddRange(prevNamesArray);
        var insteadNamesArray = new string[insteadNames.Count];
        for (int i = 0; i < insteadNamesArray.Length; i++)
        {
            insteadNamesArray[i] = insteadNames[i];
        }
        PlayerPrefsX.SetStringArray("insteadNamesArray", insteadNamesArray);
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
        DeletePrefs();
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

    void ShouldShowCheckpointUIAndText()
    {
        if (IsCheckpointAllowed())
        {
            checkpointText.color = Colors.completeColor;
            CheckpointButtonsEnabled();
        }
        else
        {
            checkpointText.color = Colors.buttonTransparentGrey;
            CheckpointButtonsDisabled();
        }
    }

    void CheckpointButtonsEnabled()
    {
        checkpointButton.color = Colors.buttonTransparentWhite;
        checkpointButton.GetComponent<Button>().enabled = true;
    }

    void CheckpointButtonsDisabled()
    {
        checkpointButton.color = Colors.buttonTransparentGrey;
        checkpointButton.GetComponent<Button>().enabled = false;
    }

    //void ShouldShowSlowdownUI()
    //{
    //    if (IsSlowAllowed())
    //    {
    //        slowdownText.color = Colors.completeColor;
    //        SlowButtonsEnabled();
    //    }
    //    else
    //    {
    //        slowdownText.color = Colors.buttonTransparentGrey;
    //        SlowButtonsDisabled();
    //    }
    //}

    void ShouldShowSlowdownText()
    {
        if (IsSlowAllowed())
        {
            slowdownText.color = Colors.completeColor;
        }
        else
        {
            slowdownText.color = Colors.buttonTransparentGrey;
        }
    }


    void SlowButtonsEnabled()
    {
        slowdownButton.color = Colors.buttonTransparentWhite;
        slowdownButton.GetComponent<Button>().enabled = true;
    }

    void SlowButtonsDisabled()
    {
        slowdownButton.color = Colors.buttonTransparentGrey;
        slowdownButton.GetComponent<Button>().enabled = false;
    }

    


    void ShouldShowInsteadText()
    {
        if (IsInsteadAllowed())
        {
            insteadText.color = Colors.completeColor;
        }
        else
        {
            insteadText.color = Colors.buttonTransparentGrey;
        }
    }

    void InsteadButtonsEnabled()
    {
        insteadButton.color = Colors.buttonTransparentWhite;
        insteadButton.GetComponent<Button>().enabled = true;
    }

    void InsteadButtonsDisabled()
    {
        insteadButton.color = Colors.buttonTransparentGrey;
        insteadButton.GetComponent<Button>().enabled = false;
    }

    public void SkillsEnabled()
    {
        if (IsInsteadAllowed())
        {
            InsteadButtonsEnabled();
        }
        if (IsCheckpointAllowed())
        {
            CheckpointButtonsEnabled();
        }
        if (IsSlowAllowed())
        {
            SlowButtonsEnabled();
        }
    }

    void SkillsDisabled()
    {
        InsteadButtonsDisabled();
        SlowButtonsDisabled();
        CheckpointButtonsDisabled();
    }

    void MovementButtonsEnabled()
    {
        leftButton.gameObject.SetActive(true);
        fakeLeftButton.gameObject.SetActive(false);

        rightButton.gameObject.SetActive(true);
        fakeRightButton.gameObject.SetActive(false);

        jumpButton.gameObject.SetActive(true);
        fakeJumpButton.gameObject.SetActive(false);
    }

    void MovementButtonsDisabled()
    {
        fakeLeftButton.gameObject.SetActive(true);
        leftButton.gameObject.SetActive(false);

        fakeRightButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(false);

        fakeJumpButton.gameObject.SetActive(true);
        jumpButton.gameObject.SetActive(false);
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

    void DeletePrefs()
    {
        PlayerPrefs.DeleteKey("checkpointCount");
        PlayerPrefs.DeleteKey("slowCount");
        PlayerPrefs.DeleteKey("insteadCount");
        PlayerPrefs.DeleteKey("insteadNamesArray");
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
        bg.PlayToBrightRed();
        mov.PauseAnim();
        insteadWorks = true;
        ++insteadCount;
        SaveOtherSkillsCount();
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.Pause();
        }
        var pms = FindObjectsOfType<PlatformMover>();
        foreach (PlatformMover pm in pms)
        {
            pm.Pause();
        }

        ShouldShowInsteadText();
        SkillsDisabled();
        MovementButtonsDisabled();
        cancelInsteadButton.gameObject.SetActive(true);
        UpdateInsteadSlots();
    }

    void Instead()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, -Vector2.zero);
            if (hit)
            {
                if (hit.transform.gameObject.layer == enemyLayer)
                {
                    var go = hit.transform.gameObject;
                    go.GetComponent<SpriteRenderer>().color = Colors.completeColor;
                    go.layer = wallsLayer;
                    var name = go.name;
                    insteadNames.Add(name);
                    BackFromInstead();
                    
                }
            }
            
        }

    }

    void BackFromInstead()
    {
        var death = FindObjectOfType<Death>();
        if (death)
        {
            death.BackFromPause();
        }
        var pms = FindObjectsOfType<PlatformMover>();
        foreach (PlatformMover pm in pms)
        {
            pm.BackFromPause();
        }

        bg.PlayBrightRedToRed();
        MovementButtonsEnabled();
        SkillsEnabled();
        cancelInsteadButton.gameObject.SetActive(false);
        insteadWorks = false;

        mov.UnpauseAnim();
    }

    public void CancelInstead()
    {
        BackFromInstead();
    }

    



}

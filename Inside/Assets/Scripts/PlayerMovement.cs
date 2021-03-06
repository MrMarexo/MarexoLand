﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpForce = 3f;

    BoxCollider2D col;
    CapsuleCollider2D capCol;
    LayerMask mask;

    Rigidbody2D rb;
    Animator anim;

    int horizontal = 0;

    float regularGravity;

    [SerializeField] float placementOffset = 0.6f;

    Vector2 curVelocity;
    Vector2 newVelocity;

    ContactFilter2D contFilter;
    RaycastHit2D[] hits = new RaycastHit2D[10];

    bool isGrounded;

    bool canRun = true;
    bool canJump = true;
    bool shooting = false;

    [HideInInspector]
    public bool alive = true;

    Vector3 teleportLocation;

    Vector3 startingPos;

    float groundedTimer = 0f;
    [SerializeField] float jumpTimeLimit = 0.2f;



    private void Awake()
    {
        startingPos = GameObject.FindGameObjectWithTag("StartingPos").transform.position;
        StartFromCheckpoint();
        ////////
        //ResetPrefs();
        /////////

    }

    private void StartFromCheckpoint()
    {
        if (PlayerPrefs.GetString("mpName", "NA") == "NA")
        {
            transform.position = LoadPositionFromPrefs();
        }
        else
        {
            string name = PlayerPrefs.GetString("mpName");
            Vector3[] platformsPos = PlayerPrefsX.GetVector3Array("platformsPosArray");
            GameObject correctPM = null;
            var pms = FindObjectsOfType<PlatformMover>();
            for (int i = 0; i < pms.Length; i++)
            {
                var pm = pms[i];
                pm.LoadWithPos(platformsPos[i]);
                if (pm.name == name)
                {
                    correctPM = pm.gameObject;
                }
            }
            
            //gameObject.transform.SetParent(correctPM.transform);
            var pos = new Vector2(correctPM.transform.position.x, correctPM.transform.position.y + placementOffset);
            transform.position = pos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        capCol = GetComponent<CapsuleCollider2D>();


        col.enabled = true;
        capCol.enabled = false;
        regularGravity = rb.gravityScale;
        
    }

    private void FixedUpdate()
    {
        Run();
        Flip();

    }

    private void Update()
    {
        isGrounded = CollideTest(Vector2.down);
        Jump();
        //Shoot();
        //ShootingFlip();
    }

    public void SaveCheckpointLocation()
    {
        //if (transform.parent.tag == "MovingPlatform")
        if (transform.parent)
        {
            string name = transform.parent.name;
            PlayerPrefs.SetString("mpName", name);
            //var platformPos = transform.parent.gameObject.GetComponent<PlatformMover>().GetPlatformPosition();
            //PlayerPrefsX.SetVector3("mpPosition", platformPos);
            var platformsArray = Resources.FindObjectsOfTypeAll<PlatformMover>();
            var platformsPosArray = new Vector3[platformsArray.Length];
            for (int i = 0; i < platformsArray.Length; i++)
            {
                var pm = platformsArray[i];
                var pos = pm.GetPlatformPosition();
                platformsPosArray[i] = pos;
            }
            PlayerPrefsX.SetVector3Array("platformsPosArray", platformsPosArray);

        }
        else
        {
            PlayerPrefs.DeleteKey("mpName");
            PlayerPrefs.DeleteKey("platformsPosArray");
            SavePositionToPrefs();
        }
    } 

    void SavePositionToPrefs()
    {
        PlayerPrefs.SetFloat("checkpointPosX", transform.position.x);
        PlayerPrefs.SetFloat("checkpointPosY", transform.position.y);
        PlayerPrefs.SetFloat("checkpointPosZ", transform.position.z);
    }

    Vector3 LoadPositionFromPrefs()
    {
        var x = PlayerPrefs.GetFloat("checkpointPosX", startingPos.x);
        var y = PlayerPrefs.GetFloat("checkpointPosY", startingPos.y);
        var z = PlayerPrefs.GetFloat("checkpointPosZ", startingPos.z);
        var pos = new Vector3(x, y, z);
        return pos;
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteKey("mpName");
        PlayerPrefs.DeleteKey("platformsPosArray");

        PlayerPrefs.DeleteKey("checkpointPosX");
        PlayerPrefs.DeleteKey("checkpointPosY");
        PlayerPrefs.DeleteKey("checkpointPosZ");
    }
    
    public void Die()
    {
        canJump = false;
        canRun = false;
        anim.SetTrigger("isDead");
        rb.velocity = new Vector2(0f, 0f);
        capCol.enabled = true;
        col.enabled = false;
        alive = false;
    }

    public void Win()
    {
        canJump = false;
        canRun = false;
        rb.velocity = new Vector2(0f, 0f);
    }

    bool CollideTest(Vector2 direction)
    {
        float dist = 0.1f;


        if (rb.Cast(direction, contFilter, hits, dist) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //movement
    private void Jump()
    {
        bool verticalInputUpwards = CrossPlatformInputManager.GetButtonDown("Jump");

        if (isGrounded)
        {
            anim.SetTrigger("isNotJumping");
            groundedTimer = 0f;
        }

        if (!isGrounded)
        {
            groundedTimer += Time.deltaTime;
        }
        bool kindaGrounded = groundedTimer <= jumpTimeLimit;

        if (verticalInputUpwards && kindaGrounded && canJump)
        {
            var jumpVector = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVector;
            anim.SetTrigger("isJumping");
            
        }
    }

    private void Run()
    {
        var input = CrossPlatformInputManager.GetAxis("Horizontal");
        bool runInputPositive = Mathf.Abs(input) > 0;
        if (canRun)
        {
            var moveVector = new Vector2(input * runSpeed * Time.fixedDeltaTime, rb.velocity.y);
            rb.velocity = moveVector;
        }
        if (isGrounded && canRun)
        {
            anim.SetBool("isRunning", runInputPositive);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void Flip()
    {
        var input = CrossPlatformInputManager.GetAxis("Horizontal");
        bool playerHasSpeed = Mathf.Abs(input) > 0 && Mathf.Abs(rb.velocity.x) > 0;
        if (playerHasSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
        }
    }
    
    //private void Shoot()
    //{
    //    if (Input.GetMouseButtonDown(0) && alive)
    //    {
    //        anim.SetBool("shotgun", true);
    //        rb.velocity = new Vector2(0f, 0f);
    //        canJump = false;
    //        canRun = false;
    //        shooting = true;
    //    }
    //    else if (Input.GetMouseButtonUp(0) && rb.velocity.x == 0f && rb.velocity.y == 0f)
    //    {
    //        anim.SetBool("shotgunShoot", true);
    //    }
        
    //}

    //public void HideShotgun()
    //{
    //    anim.SetBool("shotgun", false);
    //    anim.SetBool("shotgunShoot", false);
    //    canRun = true;
    //    canJump = true;
    //    shooting = false;
    //}

    //void ShootingFlip()
    //{
    //    if (shooting)
    //    {
    //        if (Input.GetAxis("Horizontal") > 0)
    //        {
    //            transform.localScale = new Vector2(1, 1);
    //        }
    //        else if (Input.GetAxis("Horizontal") < 0)
    //        {
    //            transform.localScale = new Vector2(-1, 1);
    //        }
    //    }
    //}

    public void StartTeleportAnim(Vector3 location)
    {
        anim.Play("Teleport");
        teleportLocation = location;
    }

    public void Teleport()
    {
        transform.position = teleportLocation;
        ReturnMovement();
    }

    public void StopMovement()
    {
        canJump = false;
        canRun = false;
        rb.velocity = new Vector3(0, 0, 0);
        rb.gravityScale = 0f;
    }

    void ReturnMovement()
    {
        canJump = true;
        canRun = true;
        rb.gravityScale = regularGravity;
    }

    public void PauseAnim()
    {
        anim.speed = 0f;
    }

    public void UnpauseAnim()
    {
        anim.speed = 1f;
    }



}

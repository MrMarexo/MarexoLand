﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpForce = 3f;

    CapsuleCollider2D col;
    LayerMask mask;

    Rigidbody2D rb;
    Animator anim;

    int horizontal = 0;

    Vector2 curVelocity;
    Vector2 newVelocity;

    ContactFilter2D contFilter;
    RaycastHit2D[] hits = new RaycastHit2D[10];

    bool isGrounded;

    bool canRun = true;
    bool canJump = true;
    bool shooting = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
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
        Shoot();
        ShootingFlip();
    }

    public void Die()
    {
        canJump = false;
        canRun = false;
        anim.SetTrigger("isDead");
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
        bool verticalInputUpwards = Input.GetButtonDown("Jump");

        if (isGrounded)
        {
            anim.SetTrigger("isNotJumping");
        }

        if (verticalInputUpwards && isGrounded && canJump)
        {
            var jumpVector = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVector;
            anim.SetTrigger("isJumping");
        }
    }

    private void Run()
    {
        var input = Input.GetAxis("Horizontal");
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
        var input = Input.GetAxis("Horizontal");
        bool playerHasSpeed = Mathf.Abs(input) > 0 && Mathf.Abs(rb.velocity.x) > 0;
        if (playerHasSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
        }
    }
    
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("shotgun", true);
            rb.velocity = new Vector2(0f, 0f);
            canJump = false;
            canRun = false;
            shooting = true;
        }
        else if (Input.GetMouseButtonUp(0) && rb.velocity.x == 0f && rb.velocity.y == 0f)
        {
            Debug.Log("weird");
            anim.SetBool("shotgunShoot", true);
        }
        
    }

    public void HideShotgun()
    {
        anim.SetBool("shotgun", false);
        anim.SetBool("shotgunShoot", false);
        canRun = true;
        canJump = true;
        shooting = false;
    }

    void ShootingFlip()
    {
        if (shooting)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }



}

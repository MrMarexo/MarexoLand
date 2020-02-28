using System.Collections;
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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        CalcHorizontal();
        isGrounded = CollideTest(Vector2.down);

        //calc movement
        curVelocity = rb.velocity;
        newVelocity = new Vector2(horizontal * runSpeed, curVelocity.y);
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            newVelocity.y = jumpForce;
            anim.SetTrigger("isJumping");
        }

        if (horizontal == -1 && CollideTest(Vector2.left))
        {
            newVelocity.x = 0;
        }

        if (horizontal == 1 && CollideTest(Vector2.right))
        {
            newVelocity.x = 0;
        }

        rb.velocity = newVelocity;

    }

    void CalcHorizontal()
    {
        
        if (Input.GetAxis("Horizontal") > 0)
        {
            horizontal = 1;
            var scale = new Vector3(horizontal, 1, 1);
            transform.localScale = scale;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            horizontal = -1;
            var scale = new Vector3(horizontal, 1, 1);
            transform.localScale = scale;
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            horizontal = 0;
        }
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

    


}

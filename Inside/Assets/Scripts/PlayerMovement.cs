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
        Run();
        Flip();

    }

    private void Update()
    {
        isGrounded = CollideTest(Vector2.down);
        Jump();
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

        if (verticalInputUpwards && isGrounded)
        {
            var jumpVector = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVector;
        }
    }

    private void Run()
    {
        var input = Input.GetAxis("Horizontal");
        bool playerHasSpeed = Mathf.Abs(input) > Mathf.Epsilon;
        if (playerHasSpeed)
        {
            var moveVector = new Vector2(input * runSpeed * Time.fixedDeltaTime, rb.velocity.y);
            rb.velocity = moveVector;
        }
        anim.SetBool("isRunning", playerHasSpeed);

    }

    private void Flip()
    {
        var input = Input.GetAxis("Horizontal");
        bool playerHasSpeed = Mathf.Abs(input) > 0;
        if (playerHasSpeed && !CollideTest(Vector2.left) && !CollideTest(Vector2.right))
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1);
        }
    }




}

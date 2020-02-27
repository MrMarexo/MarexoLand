using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpForce = 3f;

    BoxCollider2D col;
    LayerMask mask;

    Rigidbody2D rb;
    Animator anim;

    int horizontal = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CalcHorizontal();
        Run();

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

    void Run()
    {
        var velocity = new Vector2(horizontal * runSpeed, rb.velocity.y);
        rb.velocity = velocity;
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    


}

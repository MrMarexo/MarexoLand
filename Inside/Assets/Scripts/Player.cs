using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

    PlayerMovement mov;

    bool gotKey = false;

    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (GetComponent<PlayerMovement>().alive == true)
            {
                mov.Die();
                FindObjectOfType<PopupManagement>().EnableGameCanvas(loseCanvas);
            }    
        }
        if (collision.gameObject.tag == "Gate" && gotKey)
        {
            Destroy(collision.gameObject);
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            gotKey = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(collision.gameObject);
            FindObjectOfType<PopupManagement>().EnableGameCanvas(winCanvas);
        }
        if (collision.gameObject.tag == "Border")
        {
            mov.Die();
            FindObjectOfType<PopupManagement>().EnableGameCanvas(loseCanvas);
        }
    }


}

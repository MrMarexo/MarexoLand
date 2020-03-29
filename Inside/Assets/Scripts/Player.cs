using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

    [SerializeField] GameObject key;


    PlayerMovement mov;

    bool gotKey = false;
    bool gameEnded = false;

    private void Start()
    {
        gotKey = PlayerPrefsX.GetBool("gotKeyCheckpoint", false);
        if (gotKey)
        {
            key.SetActive(false);
        }
        FindObjectOfType<Level>().UpdateKeyText(gotKey);
        mov = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (GetComponent<PlayerMovement>().alive == true)
            {
                if (!gameEnded)
                {
                    gameEnded = true;
                    mov.Die();
                    FindObjectOfType<PopupManagement>().EnableGameCanvas(loseCanvas);
                }
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
            FindObjectOfType<Level>().UpdateKeyText(gotKey);
        }
        if (collision.gameObject.tag == "Finish")
        {
            if (!gameEnded)
            {
                gameEnded = true;
                mov.Win();
                Destroy(collision.gameObject);
                FindObjectOfType<PopupManagement>().EnableGameCanvas(winCanvas);
            }
        }
        if (collision.gameObject.tag == "Border")
        {
            if (!gameEnded)
            {
                gameEnded = true;
                mov.Die();
                Destroy(gameObject);
                FindObjectOfType<PopupManagement>().EnableGameCanvas(loseCanvas);
            }
        }
        if (collision.gameObject.tag == "Portal")
        {
            var teleportLocation = collision.transform.GetComponent<Portal>().GetPortalTargetPosition();
            Debug.Log(teleportLocation);
            mov.StartTeleportAnim(teleportLocation);
        }
    }

    public void SaveKey()
    {
        PlayerPrefsX.SetBool("gotKeyCheckpoint", gotKey);
    }

    public void DeleteKey()
    {
        PlayerPrefs.DeleteKey("gotKeyCheckpoint");
    }

    


}

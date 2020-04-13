using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    PlayerMovement mov;
    Level lvl;

    bool gotKey = false;
    bool gameEnded = false;

    private void Start()
    {
        gotKey = PlayerPrefsX.GetBool("gotKeyCheckpoint", false);
        if (gotKey)
        {
            GameObject.FindGameObjectWithTag("Key").SetActive(false);
        }
        FindObjectOfType<Level>().UpdateKeyImage(gotKey);
        mov = GetComponent<PlayerMovement>();
        lvl = FindObjectOfType<Level>();
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
                    lvl.ShowLoseCanvas();
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
            FindObjectOfType<Level>().UpdateKeyImage(gotKey);
        }
        if (collision.gameObject.tag == "Finish")
        {
            if (!gameEnded)
            {
                gameEnded = true;
                mov.Win();
                Destroy(collision.gameObject);
                lvl.ShowWinCanvas();
            }
        }
        if (collision.gameObject.tag == "Border")
        {
            if (!gameEnded)
            {
                gameEnded = true;
                mov.Die();
                Destroy(gameObject);
                lvl.ShowLoseCanvas();
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

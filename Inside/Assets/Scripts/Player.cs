using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerMovement mov;

    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            mov.Die();
        }
    }
}

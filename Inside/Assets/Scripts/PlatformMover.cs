using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;

    Transform nextPos;
    [SerializeField] float moveSpeed = 1f;

    private void Start()
    {
        nextPos = pos2;
    }

    private void FixedUpdate()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2;
        }
        else if (transform.position == pos2.position)
        {
            nextPos = pos1;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            //
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            //
        }
    }
}

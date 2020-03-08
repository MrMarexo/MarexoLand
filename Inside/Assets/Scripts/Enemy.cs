using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;

    Vector3 pos1P;
    Vector3 pos2P;

    Vector3 nextPos;
    [SerializeField] float moveSpeed = 1f;

    List<Rigidbody2D> rbs = new List<Rigidbody2D>();

    private void Start()
    {
        pos1P = pos1.position;
        pos2P = pos2.position;
        nextPos = pos2P;
    }

    private void FixedUpdate()
    {
        if (transform.position == pos1P)
        {
            nextPos = pos2P;
        }
        else if (transform.position == pos2P)
        {
            nextPos = pos1P;
        }

        if (nextPos == pos1P)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.fixedDeltaTime);
    }
}

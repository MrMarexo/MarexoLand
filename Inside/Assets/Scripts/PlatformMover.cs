using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;

    Transform nextPos;
    [SerializeField] float moveSpeed = 10f;

    private void Start()
    {
        nextPos = pos2;
        transform.position = startPos.position;
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
}

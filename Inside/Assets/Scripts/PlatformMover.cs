﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
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

    private void Update()
    {
        if (transform.position == pos1P)
        {
            nextPos = pos2P;
        }
        else if (transform.position == pos2P)
        {
            nextPos = pos1P;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
    }

    //private void LateUpdate()
    //{
    //    if (rbs.Count > 0)
    //    {
    //        foreach (Rigidbody2D rb in rbs)
    //        {
    //            Vector3 velocity = transform.position - lastPosition;
    //            rb.transform.Translate(velocity, transform);
    //        }

    //        lastPosition = transform.position;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Debug.Log("collision");
            collision.gameObject.transform.SetParent(transform);
            //var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            //if (rb != null)
            //{
            //    AddRigidBody(rb);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.transform.SetParent(null);
            //var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            //if (rb != null)
            //{
            //    RemoveRigidBody(rb);
            //}
        }
    }
    

    //void AddRigidBody( Rigidbody2D rb)
    //{
    //    if (!rbs.Contains(rb))
    //    {
    //        rbs.Add(rb);
    //    }
    //}

    //void RemoveRigidBody(Rigidbody2D rb)
    //{
    //    if (rbs.Contains(rb))
    //    {
    //        rbs.Remove(rb);
    //    }
    //}
}

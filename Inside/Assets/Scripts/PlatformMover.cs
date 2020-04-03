using System.Collections;
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

    [SerializeField] bool shouldPause = false;
    [SerializeField] float pauseTime = 1f;

    float regularPauseTime;
    float regularMoveSpeed;
    float slowDownTimer = 0f;
    bool timerOn = false;
    float timeSlowedDown;

    List<Rigidbody2D> rbs = new List<Rigidbody2D>();

    bool waitOngoing;

    private void Start()
    {
        regularMoveSpeed = moveSpeed;
        regularPauseTime = pauseTime;
        pos1P = pos1.position;
        pos2P = pos2.position;
        nextPos = pos2P;
    }

    private void Update()
    {
        Timer();
        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
        if (transform.position == pos1P)
        {
            if (!shouldPause)
            {
                nextPos = pos2P;
            }
            else
            {
                if (!waitOngoing)
                {
                    StartCoroutine(Wait(pos2P));
                }
            }
            
        }
        else if (transform.position == pos2P)
        {
            if (!shouldPause)
            {
                nextPos = pos1P;
            }
            else
            {
                if (!waitOngoing)
                {
                    StartCoroutine(Wait(pos1P));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.transform.SetParent(transform);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.transform.SetParent(null);
            
        }
    }

    public void SlowDown(float time, float ratio)
    {
        slowDownTimer = 0f;
        timeSlowedDown = time;
        moveSpeed = moveSpeed / ratio;
        pauseTime = pauseTime / ratio;
        timerOn = true;
    }

    void SlowUp()
    {
        moveSpeed = regularMoveSpeed;
        pauseTime = regularPauseTime;
        timerOn = false;
    }

    void Timer()
    {
        if (timerOn)
        {
            slowDownTimer += Time.deltaTime;
            if (slowDownTimer >= timeSlowedDown)
            {
                SlowUp();
            }
        }
    }

    IEnumerator Wait(Vector3 nextPosition)
    {
        waitOngoing = true;
        yield return new WaitForSecondsRealtime(pauseTime);
        nextPos = nextPosition;
        waitOngoing = false;
    }
    

}

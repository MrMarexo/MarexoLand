using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    float regularSpeed;
    float slowDownTimer = 0f;
    bool timerOn = false;
    float timeSlowedDown;

    Vector3 startingPos;

    private void Awake()
    {
        startingPos = transform.position;
        transform.position = LoadPositionFromPrefs();
    }

    private void Start()
    {
        regularSpeed = speed;
    }

    void Update()
    {
        Timer();
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    public void SavePositionToPrefs()
    {
        PlayerPrefs.SetFloat("deathPosX", transform.position.x);
        PlayerPrefs.SetFloat("deathPosY", transform.position.y);
        PlayerPrefs.SetFloat("deathPosZ", transform.position.z);
    }

    Vector3 LoadPositionFromPrefs()
    {
        var x = PlayerPrefs.GetFloat("deathPosX", startingPos.x);
        var y = PlayerPrefs.GetFloat("deathPosY", startingPos.y);
        var z = PlayerPrefs.GetFloat("deathPosZ", startingPos.z);
        var pos = new Vector3(x, y, z);
        return pos;
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteKey("deathPosX");
        PlayerPrefs.DeleteKey("deathPosY");
        PlayerPrefs.DeleteKey("deathPosZ");
    }

    public void SlowDown(float time, float ratio)
    {
        slowDownTimer = 0f;
        timeSlowedDown = time;
        speed = speed / ratio;
        timerOn = true;
    }

    void SlowUp()
    {
        speed = regularSpeed;
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

    public void Pause()
    {
        speed = 0f;
    }

    public void BackFromPause()
    {
        speed = regularSpeed;
    }
    

}

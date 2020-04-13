using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Level lvl;

    void Start()
    {
        //GetComponent<SpriteRenderer>().color = Colors.bkgRed;
        lvl = FindObjectOfType<Level>();

    }

    public void PlayToGrey()
    {
        GetComponent<Animator>().Play("ToGrey");
    }

    public void PlayGreyToRed()
    {
        GetComponent<Animator>().Play("GreyToRed");
    }

    public void PlayToBrightRed()
    {
        GetComponent<Animator>().Play("ToBrightRed");
    }

    public void PlayBrightRedToRed()
    {
        GetComponent<Animator>().Play("BrightRedToRed");
    }

    public void PlayFlash()
    {
        GetComponent<Animator>().Play("Flash");
        
    }

    //animation events
    public void ChangeToGrey()
    {
        GetComponent<SpriteRenderer>().color = Colors.bkgGrey;
    }

    public void ChangeToRed()
    {
        GetComponent<SpriteRenderer>().color = Colors.bkgRed;
    }

    public void ChangeToBrightRed()
    {
        GetComponent<SpriteRenderer>().color = Colors.bkgBrightRed;
    }

    public void PlayDefault()
    {
        GetComponent<Animator>().Play("Red");
    }

    public void InitiateSlowdown()
    {
        StartCoroutine(lvl.Slowdown());
    }

    public void AfterSlowdown()
    {
        lvl.SkillsEnabled();
    }
    //////////////////////////////////////////
}

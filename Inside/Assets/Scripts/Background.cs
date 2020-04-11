using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SpriteRenderer>().color = Colors.bkgRed;
        
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
}

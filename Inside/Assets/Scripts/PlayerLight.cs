using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    Light2D plLight;
    Willpower will;

    float regOuterRadius;
    float regInnerRadius;
    float regLightIntensity;
    float unchangedOuterRadius;
    float unchangedInnerRadius;
    float unchangedIntensity;

    [SerializeField] float radius = 3.76f;

    [SerializeField] float flickerMagnitude = 0.5f;

    float innerOuterOffset = 6.5f;

    bool flickerOn = false;
    int blackChanceNumber = 60;

    private void Awake()
    {
        plLight = GetComponentInChildren<Light2D>();
        will = FindObjectOfType<Willpower>();
        plLight.pointLightOuterRadius = radius;
        plLight.pointLightInnerRadius = radius / innerOuterOffset;
    }

    void Start()
    {
        regOuterRadius = plLight.pointLightOuterRadius;
        regInnerRadius = plLight.pointLightInnerRadius;
        regLightIntensity = plLight.intensity;
        unchangedOuterRadius = regOuterRadius;
        unchangedInnerRadius = regInnerRadius;
        unchangedIntensity = regLightIntensity;
        ChooseLightMode();
    }

    void Update()
    {
        Flicker();
    }

    void Flicker()
    {
        if (flickerOn)
        {
            float randomOffset = Random.Range(0, flickerMagnitude);
            float intensityOffset = randomOffset / 3;
            float newOuterRadius = regOuterRadius + randomOffset;
            float newIntensity = regLightIntensity - intensityOffset;
            plLight.pointLightOuterRadius = newOuterRadius;
            plLight.intensity = newIntensity;
            int chanceOfBlack = Random.Range(0, blackChanceNumber);
            if (chanceOfBlack == 0)
            {
                plLight.pointLightOuterRadius = 0f;
            }
        }
    }

    public void ChooseLightMode()
    {
        regOuterRadius = unchangedOuterRadius;
        regInnerRadius = unchangedInnerRadius;
        regLightIntensity = unchangedIntensity;

        int willState = will.GetWillpowerState();
        Debug.Log(willState);
        regOuterRadius -= willState * 0.5f;
        if (willState > 1)
        {
            flickerOn = true;
            regLightIntensity -= willState * 0.1f;
            blackChanceNumber -= willState * 5;

        }
        else flickerOn = false;
        plLight.pointLightOuterRadius = regOuterRadius;
        plLight.pointLightInnerRadius = regOuterRadius / innerOuterOffset;
        plLight.intensity = regLightIntensity;
    }

    public void PauseFlicker()
    {
        if (flickerOn)
        {
            flickerOn = false;
        }
    }


}

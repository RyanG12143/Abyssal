using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CreatureSonarPulse : MonoBehaviour
{
    // SpriteRenderer of the creature
    private SpriteRenderer SR;

    // Is this creature being scanned(fading in/out)?
    private Boolean isFadeRunning = false;

    // SpriteLight of the creature
    public GameObject SpriteLight;

    // Animations of the creature
    public GameObject Animated;

    public Material lit;

    public Material unlit;


    // Start is called before the first frame update
    void Start()
    {
        // Sets SR to the SpriteRenderer of the creature
        if (GetComponent<SpriteRenderer>() != null)
        {
            SR = GetComponent<SpriteRenderer>();
        }
        else
        {
            SR = Animated.GetComponent<SpriteRenderer>();
        }
    }

    /*
    Leo Dresang
    10/20/2023
    Begins the Fade effect
    */
    public void StartFade()
    {
        if (!isFadeRunning)
        {
            StartCoroutine(Fade());
        }
    } 

    /*
    Leo Dresang
    10/30/2023
    Creates the Fade in/out effect as the creature gets scanned.
    */
    IEnumerator Fade()
    {

        isFadeRunning = true;

        SpriteLight.GetComponent<Light2D>().intensity = (0f);

        Color scannedColor = Color.red;

            Color defaultColor = new Color((SR.color.r), (SR.color.g), (SR.color.b));

            for (int i = 0; i <= 26; i++)
            {

                SR.color = scannedColor;

                SpriteLight.GetComponent<Light2D>().intensity = (i * 0.04f);
                yield return new WaitForSeconds(.01f);

            }


            yield return new WaitForSeconds(3f);

            for (int i = 26; i >= 0; i--)
            {

                SpriteLight.GetComponent<Light2D>().intensity = (i * 0.04f);
                yield return new WaitForSeconds(.05f);

            }

            SR.color = defaultColor;
       


        isFadeRunning = false;

    }
}

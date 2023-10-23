using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvironmentSonarPulse : MonoBehaviour
{
    // SpriteRenderer of the tile
    private SpriteRenderer SR;

    // Is this tile being scanned(fading in/out)?
    private Boolean isFadeRunning = false;

    // SpriteLight of the tile
    public GameObject SpriteLight;

    // Animations of the tile
    public GameObject Animated;


    // Start is called before the first frame update
    void Start()
    {
        // Sets SR to the SpriteRenderer of the tile
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
    Beings the Fade effect
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
    10/20/2023
    Creates the Fade in/out effect as the tile gets scanned.
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


            yield return new WaitForSeconds(2f);

            for (int i = 26; i >= 0; i--)
            {

                SpriteLight.GetComponent<Light2D>().intensity = (i * 0.04f);
                yield return new WaitForSeconds(.05f);

            }

            SR.color = defaultColor;
       


        isFadeRunning = false;

    }
}

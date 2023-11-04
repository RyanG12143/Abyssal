using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CrackedWallSonarPulse : MonoBehaviour
{
    // SpriteRenderer of the walls cracks
    private SpriteRenderer SR;

    // Is this wall being scanned(fading in/out)?
    private Boolean isFadeRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        // Sets SR to the SpriteRenderer of the highlighted cracks

            SR = GetComponent<SpriteRenderer>();
            SR.enabled = false;
            SR.color = new Color((225f), (225f), (0f), (0f));

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
    10/30/2023
    Creates the Fade in/out effect as the creature gets scanned.
    */
    IEnumerator Fade()
    {


        SR.enabled = true;

        isFadeRunning = true;

        SR.color = new Color((225f), (225f), (0f), (0f));


        for (int i = 0; i <= 26; i++)
        {

            SR.color += new Color((0f), (0f), (0f), (i * 0.04f));

            yield return new WaitForSeconds(.05f);

        }


        yield return new WaitForSeconds(2f);

        for (int i = 26; i >= 0; i--)
        {

            SR.color -= new Color((0f), (0f), (0f), (i * 0.04f));
            yield return new WaitForSeconds(.05f);

        }

        SR.color = new Color((225f), (225f), (0f), (0f));

        SR.enabled = false;

        isFadeRunning = false;

    }
}

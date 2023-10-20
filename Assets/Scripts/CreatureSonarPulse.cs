using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CreatureSonarPulse : MonoBehaviour
{
    private SpriteRenderer SR;

    private Boolean isFadeRunning = false;

    public GameObject SpriteLight;

    public GameObject Animated;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            SR = GetComponent<SpriteRenderer>();
        }
        else
        {
            //GameObject Animated = gameObject.name.Append("AnimatedFrames_0);
            SR = Animated.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFade()
    {
        if (!isFadeRunning)
        {
            StartCoroutine(Fade());
        }
    } 

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

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CreatureSonarPulse : MonoBehaviour
{
    private Boolean isFadeRunning = false;

    public GameObject SpriteLight;

    // Start is called before the first frame update
    void Start()
    {
        
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

        SpriteLight.GetComponent<Light2D>().intensity = (0f); ;

        Color defaultColor = new Color((GetComponent<SpriteRenderer>().color.r), (GetComponent<SpriteRenderer>().color.g), (GetComponent<SpriteRenderer>().color.b));

        Color scannedColor = Color.red;

        for (int i = 0; i <= 26; i++)
        {

            GetComponent<SpriteRenderer>().color = scannedColor;

            SpriteLight.GetComponent<Light2D>().intensity = (i*0.04f);
            yield return new WaitForSeconds(.01f);

        }


        yield return new WaitForSeconds(2f);

        for (int i = 26; i >= 0; i--)
        {

            SpriteLight.GetComponent<Light2D>().intensity = (i * 0.04f);
            yield return new WaitForSeconds(.05f);

        }
        Debug.Log(SpriteLight.GetComponent<Light2D>().intensity);
        GetComponent<SpriteRenderer>().color = defaultColor;



        isFadeRunning = false;

    }
}

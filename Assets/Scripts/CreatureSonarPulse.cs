using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CreatureSonarPulse : MonoBehaviour
{
    private Boolean isFadeRunning = false;

    public Material lit;

    public Material unlit;

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

        float current =  GetComponent<Light2D>().intensity = 0f;

        Color theDefault = new Color((GetComponent<SpriteRenderer>().color.r), (GetComponent<SpriteRenderer>().color.g), (GetComponent<SpriteRenderer>().color.b));

        float scanned =  GetComponent<Light2D>().intensity = 1f;

        Color scannedColor = Color.red;

        GetComponent<SpriteRenderer>().material = unlit;

        for (int i = 0; i < 25; i++)
        {

            GetComponent<SpriteRenderer>().color = scannedColor;

            GetComponent<Light2D>().intensity = float(current, scanned, i / 25.0f);
            yield return new WaitForSeconds(.05f);

        }


        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 25; i++)
        {

            GetComponent<SpriteRenderer>().color = theDefault;

            yield return new WaitForSeconds(.05f);

        }


        GetComponent<SpriteRenderer>().material = lit;
        isFadeRunning = false;

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSonarPulse : MonoBehaviour
{
    private Boolean isFadeRunning = false;

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

        Color current = new Color((GetComponent<SpriteRenderer>().color.r), (GetComponent<SpriteRenderer>().color.g), (GetComponent<SpriteRenderer>().color.b));

        Color scanned = Color.red;

        for (int i = 0; i < 25; i++)
        {

            GetComponent<SpriteRenderer>().color = Color.Lerp(current, scanned, i / 25.0f);
            yield return new WaitForSeconds(.05f);

        }


        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 25; i++)
        {

            GetComponent<SpriteRenderer>().color = Color.Lerp(scanned, current, i / 25.0f);

            yield return new WaitForSeconds(.05f);

        }

        isFadeRunning = false;

    }
}

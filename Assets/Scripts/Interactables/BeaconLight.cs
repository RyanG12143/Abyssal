using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine.Rendering.Universal;
using System.Data;

public class BeaconLight : MonoBehaviour
{

    private SpriteRenderer SR;
    public GameObject SpriteLight;
    public GameObject Animated;
    private bool isLightRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        // Sets SR to the SpriteRenderer of the Beacon
        if (GetComponent<SpriteRenderer>() != null)
        {
            SR = GetComponent<SpriteRenderer>();
        }
        else
        {
            SR = Animated.GetComponent<SpriteRenderer>();
        }

        SpriteLight.GetComponent<Light2D>().intensity = (0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLightRunning && gameObject.GetComponent<Beacon>().currState == BeaconState.Idle)
        {
            StartCoroutine(Light());
        }
        
    }

    IEnumerator Light()
    {

        isLightRunning = true;

       SpriteLight.GetComponent<Light2D>().intensity = (0f);

            for (int i = 0; i <= 100; i++)
            {

                SpriteLight.GetComponent<Light2D>().intensity = i;
                yield return new WaitForSeconds(.01f);

            }


            yield return new WaitForSeconds(1f);

            for (int i = 100; i >= 0; i--)
            {

                SpriteLight.GetComponent<Light2D>().intensity = i;
                yield return new WaitForSeconds(.01f);

            }       

            yield return new WaitForSeconds(0.1f);


        isLightRunning = false;

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    // List of scannable creatures
    private GameObject[] creaturesScanned;

    // List of scannable environment tiles
    private GameObject[] tilesScanned;

    // List of beacons (should only be one at a time)
    private GameObject[] beaconsScanned;

    // List of interactable objects
    private GameObject[] interactablesScanned;

    // Nightingale
    private GameObject Nightingale = null;

    // Sonar ring
    public GameObject SonarSpread;

    // Is sonar ring increasing in size?
    private Boolean sonarSizeIncrease;

    // Is sonar ring visual effects running?
    private Boolean isEffectsRunning = false;

    // Is sonar currently on cooldown?
    private Boolean isCooldownActive = false;

    // Sonar sound effect
    public AudioSource sonarBeep;

    public GameObject SonarArrow;


    // Start is called before the first frame update
    void Start()
    {
        sonarSizeIncrease = false;
        SonarSpread.transform.localScale = new Vector3(1f, 1f, 1f);
        SonarSpread.GetComponent<SpriteRenderer>().color = new Color((SonarSpread.GetComponent<SpriteRenderer>().color.r), (SonarSpread.GetComponent<SpriteRenderer>().color.g), (SonarSpread.GetComponent<SpriteRenderer>().color.b), 0f);
        creaturesScanned = GameObject.FindGameObjectsWithTag("CreatureSonar");
        //tilesScanned = GameObject.FindGameObjectsWithTag("Wall");
        interactablesScanned = GameObject.FindGameObjectsWithTag("Interactable");
        beaconsScanned = GameObject.FindGameObjectsWithTag("Beacon");
    }

    // Update is called once per frame
    void Update()
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        // Activates sonar if space is pressed and it is off cooldown.
        if (Input.GetKeyDown(KeyCode.Space) && !isCooldownActive)
        {
            sonarBeep.Play();
            SonarSpread.GetComponent<SpriteRenderer>().color = new Color((SonarSpread.GetComponent<SpriteRenderer>().color.r), (SonarSpread.GetComponent<SpriteRenderer>().color.g), (SonarSpread.GetComponent<SpriteRenderer>().color.b), 1f);
            SonarScan();
            StartCoroutine(Cooldown());
        }

        UpdateSonar();

    }

        /*
        Leo Dresang
        10/20/2023
        Updates sonar information.
        */
        void UpdateSonar(){
        if ((SonarSpread.transform.localScale.x < 40f) && sonarSizeIncrease)
        {
            if (!isEffectsRunning)
            {
                StartCoroutine(Effects());
            }

            SonarSpread.transform.localScale += new Vector3(20f, 20f, 0f) * Time.deltaTime;

            foreach (var creature in creaturesScanned)
            {

                if ((Vector2.Distance(SonarSpread.transform.position, creature.transform.position) < (SonarSpread.transform.localScale.x * 0.5)))
                {
                    creature.GetComponent<CreatureSonarPulse>().StartFade();
                }
            }

            foreach (var interactable in interactablesScanned)
            {

                if ((Vector2.Distance(SonarSpread.transform.position, interactable.transform.position) < (SonarSpread.transform.localScale.x * 0.5)))
                {
                    
                    interactable.GetComponent<InteractableSonarPulse>().StartFade();
                }
            }

          // foreach (var tile in tilesScanned)
            {

           //     if ((Vector2.Distance(SonarSpread.transform.position, tile.transform.position) < (SonarSpread.transform.localScale.x * 0.5)))
                {
                    
           //         tile.GetComponent<EnvironmentSonarPulse>().StartFade();
                }
            }


        }
        else
        {
            sonarSizeIncrease = false;
            SonarSpread.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        }


    }


    /*
    Leo Dresang
    10/20/2023
    Prepares allows for sonar scanning to begin.
    */
    void SonarScan()
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        Transform target = Nightingale.transform;

        Vector3 newPos = new Vector3(target.position.x, target.position.y, 0f);

        SonarSpread.transform.position = newPos;

        sonarSizeIncrease = true;

        SonarArrow.GetComponent<BeaconSonar>().StartPoint(beaconsScanned[0]);

    }

    /*
    Leo Dresang
    10/20/2023
    Controls the visual effects of the sonar ring.
    */
    IEnumerator Effects()
    {

        isEffectsRunning = true;

        Color current = new Color((SonarSpread.GetComponent<SpriteRenderer>().color.r), (SonarSpread.GetComponent<SpriteRenderer>().color.g), (SonarSpread.GetComponent<SpriteRenderer>().color.b), 0.1f);

        Color end = new Color((SonarSpread.GetComponent<SpriteRenderer>().color.r), (SonarSpread.GetComponent<SpriteRenderer>().color.g), (SonarSpread.GetComponent<SpriteRenderer>().color.b), 0f);

        for (int i = 0; i < 60; i++)
        {

            SonarSpread.GetComponent<SpriteRenderer>().color = Color.Lerp(current, end, i / 60.0f);

            yield return new WaitForSeconds(.03f);

        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 1; i++)
        {

            SonarSpread.GetComponent<SpriteRenderer>().color = Color.Lerp(end, current, i / 1.0f);

        }

        isEffectsRunning = false;

    }

    /*
    Leo Dresang
    10/20/2023
    Cooldown for sonar.
    */
    IEnumerator Cooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(5f);
        isCooldownActive = false;
    }

}

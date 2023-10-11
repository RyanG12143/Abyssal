using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    private GameObject[] creaturesScanned;

    private Transform target;
    private GameObject Nightingale = null;

    public GameObject SonarSpread;

    private Boolean sonarSizeIncrease;
    

    // Start is called before the first frame update
    void Start()
    {
        sonarSizeIncrease = false;
        SonarSpread.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        creaturesScanned = GameObject.FindGameObjectsWithTag("CreatureSonar");

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        { 

            SonarScan(creaturesScanned);
        }


    }

    private void FixedUpdate()
    {
        if ((SonarSpread.transform.localScale.x < 20f) && sonarSizeIncrease)
        {

            SonarSpread.transform.localScale += new Vector3(4f, 4f, 4f) * Time.deltaTime;

            foreach (GameObject creature in creaturesScanned)
            {

                creature.GetComponent<SpriteRenderer>().color = Color.red;

            }

        }
        else
        {
            sonarSizeIncrease = false;
            SonarSpread.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    void SonarScan(GameObject[] creaturesScanned)
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        target = Nightingale.transform;

        Instantiate(SonarSpread, target.position, target.rotation);

        sonarSizeIncrease = true;

    }

}

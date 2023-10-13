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
        creaturesScanned = GameObject.FindGameObjectsWithTag("CreatureSonar");
    }

    // Update is called once per frame
    void Update()
    {

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
        if ((SonarSpread.transform.localScale.x < 40f) && sonarSizeIncrease)
        {

            SonarSpread.transform.localScale += new Vector3(14f, 14f, 14f) * Time.deltaTime;
            //SonarSpread.GetComponent<CapsuleCollider2D>().size += new Vector2(14f, 14f) * Time.deltaTime;

            Debug.Log(SonarSpread.transform.localScale.x);

            foreach (var creature in creaturesScanned)
            {

                Debug.Log(Vector2.Distance(target.position, creature.transform.position));
                if(Vector2.Distance(target.position, creature.transform.position) < (SonarSpread.transform.localScale.x))
                {
         
                    creature.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }

        }
        else
        {
            sonarSizeIncrease = false;
            SonarSpread.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //SonarSpread.GetComponent<CapsuleCollider2D>().size += new Vector2(0.1f, 0.1f) * Time.deltaTime;
        }

    }

    void SonarScan(GameObject[] creaturesScanned)
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        target = Nightingale.transform;

        Vector3 newPos = new Vector3(target.position.x, target.position.y, 0f);

        SonarSpread.transform.position = newPos;

        sonarSizeIncrease = true;

    }

}

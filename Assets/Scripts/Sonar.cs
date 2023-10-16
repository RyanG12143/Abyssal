using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    private GameObject[] creaturesScanned;

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

        if ((SonarSpread.transform.localScale.x < 40f) && sonarSizeIncrease)
        {

            SonarSpread.transform.localScale += new Vector3(14f, 14f, 0f) * Time.deltaTime;

            foreach (var creature in creaturesScanned)
            {

                if (Vector2.Distance(SonarSpread.transform.position, creature.transform.position) < (SonarSpread.transform.localScale.x * 0.5))
                {

                    //creature.GetComponent<SpriteRenderer>().color = Color.red;
                    
                    StartCoroutine(Fade(creature));
                    Debug.Log("Coroutine Called!");
                   
                }
            }

        }
        else
        {
            sonarSizeIncrease = false;
            SonarSpread.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        }

    }

    void SonarScan(GameObject[] creaturesScanned)
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        Transform target = Nightingale.transform;

        Vector3 newPos = new Vector3(target.position.x, target.position.y, 0f);

        SonarSpread.transform.position = newPos;

        sonarSizeIncrease = true;

    }

    IEnumerator Fade(GameObject creature)
    {
        Color current = new Color((creature.GetComponent<SpriteRenderer>().color.r), (creature.GetComponent<SpriteRenderer>().color.g), (creature.GetComponent<SpriteRenderer>().color.b));

        Color scanned = Color.red;

        for(int i = 0; i < 25; i++)
        {

            creature.GetComponent<SpriteRenderer>().color = Color.Lerp(current, scanned, i/25.0f);

            yield return new WaitForSeconds(.05f);

        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 25; i++)
        {

            creature.GetComponent<SpriteRenderer>().color = Color.Lerp(scanned, current, i/25.0f);

            yield return new WaitForSeconds(.05f);

        }

        //  creature.GetComponent<SpriteRenderer>().color = Color.Lerp(scanned, current, Time.deltaTime);

    }

}

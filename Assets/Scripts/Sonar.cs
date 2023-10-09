using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    GameObject[] creaturesScanned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            creaturesScanned = GameObject.FindGameObjectsWithTag("CreatureSonar");

            SonarScan(creaturesScanned);
        }
    }

    void SonarScan(GameObject[] creaturesScanned)
    {
        foreach(GameObject creature in creaturesScanned)
        {
            creature.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

}

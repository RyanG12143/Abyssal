using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWallGone : MonoBehaviour
{
    private GameObject package;
    // Start is called before the first frame update
    void Start()
    {
        if (package == null)
        {
            package = GameObject.Find("Explosive package");
        }

        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        blowUp();
    }


    void blowUp()
    {
        if((Vector2.Distance(package.transform.position, gameObject.transform.position) < 1.3f) && package.GetComponent<StrongExplosion>().checkExplode())
        {
            gameObject.SetActive(false);
            
        }
    }
}

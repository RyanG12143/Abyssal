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
        dropPackage();
    }


    void blowUp()
    {
        if((Vector2.Distance(package.transform.position, gameObject.transform.position) < 0.75f) && package.GetComponent<StrongExplosion>().checkExplode())
        {
            gameObject.SetActive(false);
            
        }
    }

    void dropPackage()
    {
        if(Vector2.Distance(package.transform.position, gameObject.transform.position) < 0.75f && package.GetComponent<PickUpAble>().isPickedUp())
        {
            package.GetComponent<PickUpAble>().setPickedUp(false);
        }
    }

}

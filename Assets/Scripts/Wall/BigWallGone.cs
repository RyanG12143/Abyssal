using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BigWallGone : MonoBehaviour
{
    private GameObject package;
    public bool isPlaced = false;

    // Start is called before the first frame update
    void Start()
    {

        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (package == null)
        {
            package = GameObject.FindGameObjectWithTag("ExplosivePackage");
        }

        blowUp();
        //dropPackage();
    }


    void blowUp()
    {
        if (package.GetComponent<StrongExplosion>().checkExplode())
        {
            gameObject.SetActive(false);

        }
    }

    //void dropPackage()
    //{
    //    if(Vector2.Distance(package.transform.position, gameObject.transform.position) < 5f && package.GetComponent<PickUpAble>().isPickedUp())
    //    {
    //        isPlaced = true;
    //        package.GetComponent<PickUpAble>().setPickedUp(false);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("collison");

        if ((collision.gameObject.tag == "Player"))
        {
            isPlaced = true;
            package.GetComponent<PickUpAble>().setPickedUp(false);
            Vector3 startPos = new Vector3(package.transform.position.x, package.transform.position.y, package.transform.position.z);
            Vector3 endPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
            while (startPos != endPos)
            {
                package.transform.position = Vector3.Slerp(startPos, endPos, 10f * Time.deltaTime);
            }
        }
    }

}

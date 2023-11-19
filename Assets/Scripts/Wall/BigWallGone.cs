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

        isPlaced = false;

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

    private void FixedUpdate()
    {
        if (isPlaced)
        {
            Vector3 endPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
            package.transform.position = Vector3.Slerp(package.transform.position, endPos, 2.5f * Time.deltaTime);

        }
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

        if ((collision.gameObject.tag == "Player") && package.GetComponent<PickUpAble>().isPickedUp())
        {
            isPlaced = true;
            package.GetComponent<PickUpAble>().setPickedUp(false);

        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickUpAble : MonoBehaviour
{
    private float FollowSpeed = 5f;

    private GameObject Nightingale = null;

    private Transform target;

    private Vector2 currentVelocity;

    private bool pickedUp;

    private bool NightingaleFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickedUp)
        {
            checkPickUp();
        }
    }

    private void FixedUpdate()
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }


        if (pickedUp)
        {
            Follow();
        }
    }

    void Follow()
    {
 

        target = Nightingale.transform;

        currentVelocity = Nightingale.GetComponent<NightingaleMovement>().getMovementDirection();

        NightingaleFacingRight = Nightingale.GetComponent<NightingaleMovement>().getIsFacingRight();


        if (NightingaleFacingRight)
        {
            Vector3 newPos = new Vector3(target.position.x + 1.15f, target.position.y + (currentVelocity.y * -0.10f), 0f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 newPos = new Vector3(target.position.x - 1.15f, target.position.y + (currentVelocity.y * -0.10f), 0f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }

      
    }

    void checkPickUp()
    {
        if((Vector2.Distance(Nightingale.transform.position, gameObject.transform.position) < 2f))
        {
            pickedUp = true;
        }
    }
}

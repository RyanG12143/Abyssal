using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Key : MonoBehaviour
{
    private float FollowSpeed = 5f;

    private GameObject Nightingale = null;

    private Transform target;

    private Vector2 currentVelocity;

    public bool pickedUp;

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
        if (pickedUp)
        {
            Follow();
        }
    }

    void Follow()
    {
        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        target = Nightingale.transform;

        currentVelocity = Nightingale.GetComponent<NightingaleMovement>().getMovementDirection();

        NightingaleFacingRight = Nightingale.GetComponent<NightingaleMovement>().getIsFacingRight();


        if (NightingaleFacingRight)
        {
            Vector3 newPos = new Vector3(target.position.x + 1.5f, target.position.y + (currentVelocity.y * -0.5f), 0f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 newPos = new Vector3(target.position.x - 1.5f, target.position.y + (currentVelocity.y * -0.5f), 0f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }

      
    }

    void checkPickUp()
    {

    }
}

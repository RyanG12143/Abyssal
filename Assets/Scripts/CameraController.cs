using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Speed of the camera (would not recommend messing with this)
    private float FollowSpeed = 10f;

    // The transform of the Nightingale
    private Transform target;

    // Nightingale
    private GameObject Nightingale = null;

    // Current Nightingale Velocity
    private Vector2 currentVelocity;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        target = Nightingale.transform;

        currentVelocity = Nightingale.GetComponent<NightingaleMovement>().getMovementDirection();


        // These two lines are what effect camera movement
        Vector3 newPos = new Vector3(target.position.x + (currentVelocity.x * 1.20f), target.position.y + (currentVelocity.y * 0.60f), -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);


    }

}

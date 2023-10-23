using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float FollowSpeed = 10f;
    private Transform target;
    private GameObject Nightingale = null;
    private Vector2 currentVelocity;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        target = Nightingale.transform;

        currentVelocity = Nightingale.GetComponent<NightingaleMovement>().getVelocity();



        Vector3 newPos = new Vector3(target.position.x + (currentVelocity.x * 1.25f), target.position.y + (currentVelocity.y * 0.85f), -10f);

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);


    }

}

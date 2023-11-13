using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class PickUpAble : MonoBehaviour
{
    private float FollowSpeed = 5f;

    private GameObject Nightingale = null;

    private Transform target;

    private Vector2 currentVelocity;

    private bool pickedUp;

    private bool NightingaleFacingRight;

    public AudioSource pickUpSound;

    public Material lit;

    public Material unlit;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material = lit;
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }


        if (pickedUp)
        {
            GetComponent<SpriteRenderer>().material = unlit;
            Follow();
        }
        else
        {
            GetComponent<SpriteRenderer>().material = lit;

        }
    }

    void Follow()
    {
 

        target = Nightingale.transform;

        currentVelocity = Nightingale.GetComponent<NightingaleMovement>().getVelocity();

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !pickedUp)
        {
            pickedUp = true;
            pickUpSound.Play();
            collision.gameObject.GetComponent<PlayerController>().addCrystal(gameObject);
        }
    }

    public bool isPickedUp()
{
    return pickedUp;

}

    public void setPickedUp(bool pickedup)
    {
        this.pickedUp = pickedup;
    }

}

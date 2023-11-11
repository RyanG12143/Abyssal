using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum BeaconState
{
    Idle,
    Active,
    Inactive,
}

public class Beacon : MonoBehaviour
{
    public Transform target;
    GameObject player;
    public BeaconState currState = BeaconState.Idle;
    Rigidbody2D myRigidbody;
    public float floatInterval = 1f;
    public float floatSpeed = 0.05f;
   // public float range = 2f;
    private bool floatFlip;
    //public bool playerInteract = false;
    public GameObject nextBeacon;
    private bool inRange = false;

    public AudioSource soundQue;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(ChangeFloatDirection());

    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case BeaconState.Idle:
                Idle();
                break;
            case BeaconState.Active:
                Active();
                break;
            case BeaconState.Inactive:
                Inactive();
                break;
        }

        // Checks what state to be in
        if (inRange && currState == BeaconState.Idle)
        {
            currState = BeaconState.Active;
            if (gameObject.GetComponent<TextHolder>().Text.Length > 0)
            {
                if(soundQue != null){
                soundQue.Play();
                }
                GameObject.Find("EventHandler").GetComponent<EventHandler>().displayText(gameObject.GetComponent<TextHolder>().Text);
            }
        }
        else if (!inRange && currState == BeaconState.Active)
        {
            currState = BeaconState.Inactive;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }




    void Idle()
    {
        if (floatFlip)
        {
            myRigidbody.velocity = new Vector2(0f, floatSpeed);
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, -floatSpeed);
        }


    }

    void Inactive()
    {
        if (floatFlip)
        {
            myRigidbody.velocity = new Vector2(0f, floatSpeed);
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, -floatSpeed);
        }

    }

    void Active()
    {
        if (nextBeacon != null)
        {
            nextBeacon.GetComponent<Beacon>().currState = BeaconState.Idle;
        }

        if (floatFlip)
        {
            myRigidbody.velocity = new Vector2(0f, floatSpeed);
        }
        else
        {
            myRigidbody.velocity = new Vector2(0f, -floatSpeed);
        }

    }

    IEnumerator ChangeFloatDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(floatInterval);
            floatFlip = !floatFlip; // Toggle the value of creatureTurn
        }
    }
    //private bool IsPlayerInRange(float range)
    //{
    //    return Vector3.Distance(transform.position, player.transform.position) <= range;
    //}


}

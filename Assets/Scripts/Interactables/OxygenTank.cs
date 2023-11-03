using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum TankState
{
    Idle,
    Stolen,
}

public class OxygenTank : MonoBehaviour
{
    public Transform target;
    GameObject player;
    public TankState currState = TankState.Idle;
    Rigidbody2D myRigidbody;
    public float floatInterval = 1f;
    public float floatSpeed = 0.05f;
    public float range = 2f;
    private bool floatFlip;
    public bool playerInteract = false;


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
            case TankState.Idle:
                Idle();
                break;
            case TankState.Stolen:
                Stolen();
                break;
        }

        //// Checks what state to be in
        //if (!IsPlayerInRange(range) && currState != TankState.Inactive)
        //{
        //    currState = TankState.Idle;
        //}
        //else (IsPlayerInRange(range) && currState != TankState.Inactive)
        //{
        //    //currState = TankState.Stolen;
        //}
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

    void Stolen()
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

    IEnumerator ChangeFloatDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(floatInterval);
            floatFlip = !floatFlip; // Toggle the value of creatureTurn
        }
    }
}

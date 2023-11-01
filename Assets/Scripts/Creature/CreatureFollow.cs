using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public enum EnemyAction
{
    Wander,
    Follow,
    Run,
    Die,
}

public class EnemyController : MonoBehaviour
{
    GameObject player;
    public EnemyAction currState = EnemyAction.Wander;

    public Transform target;
    Rigidbody2D myRigidbody;

    public float range = 2f;
    public float moveSpeed = 2f;

    public bool hitPlayer = false;
    public bool hitByTorpedo = false;
    private bool isFacingRight = true;
    private bool initalRight = false;
    private bool creatureTurn = false;
    public float moveDistance = 5f;
    public float turnInterval = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(ChangeCreatureTurn());
    }

    // Update is called once per frame
    void Update()
    {
        stateSwitch();
        facingUpdate();

    }
    
    // Switches the states of the enemy creature
    private void stateSwitch()
    {
        // Checks what state to be in
        if (IsPlayerInRange(range) && currState != EnemyAction.Die && hitPlayer == true)
        {
            currState = EnemyAction.Run;
        }
        else if (IsPlayerInRange(range) && currState != EnemyAction.Die && currState != EnemyAction.Run)
        {
            currState = EnemyAction.Follow;
        }
        else if (!IsPlayerInRange(range) && currState != EnemyAction.Die)
        {
            currState = EnemyAction.Wander;
        }
        else if (hitByTorpedo)
        {
            currState = EnemyAction.Die;
        }

        // Switches current state based on currState
        switch (currState)
        {
            case EnemyAction.Wander:
                Wander();
                break;
            case EnemyAction.Follow:
                Follow();
                break;
            case EnemyAction.Run:
                Run();
                break;
            case EnemyAction.Die:
                Die();
                break;
        }
    }

    // Different creature state methods
    //
    //
    // Wander creature state
    void Wander()
    {
        Vector3 localScale = transform.localScale;
        if (transform.up.y < 0f)
        {
            //FIX THIS
            localScale.y = -1;
            Debug.Log("UpsideDown");
        }

        if (creatureTurn)
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    // Follow creature state
    void Follow()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;


        //// Use LookAt to make the enemy face the player
        //transform.right = direction;
        transform.right = myRigidbody.velocity;

        myRigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        flip();
    }

// Run creature state
void Run()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Make the enemy face away from the player
        //transform.right = -direction;
        
        // Invert the direction for running away
        direction = -direction;




        myRigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        
        flip();
    }

    // Die method
    void Die()
    {
        Destroy(gameObject);
    }



    //Flipping sprite at critical points
    private void flip()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        if ((isFacingRight && direction.x < -0.01) || (!isFacingRight && direction.x > 0.01))
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.y *= -1;
            transform.localScale = localScale;
        }
    }

    // Updates facing direction of creature based on velocity
    private void facingUpdate()
    {
        // Velocity Based Direction
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;



        Vector2 direction = (targetPosition - currentPosition).normalized;


        transform.up = myRigidbody.velocity;

        //// Use LookAt to make the enemy face the player
        //transform.right = direction;
        transform.right = myRigidbody.velocity;

        float angle;
        float rotateSpeed = 2;

        angle = Mathf.Sign(Vector2.SignedAngle(transform.up, myRigidbody.velocity));

        //this is to stop overrotation
        if (Mathf.Abs(Vector2.Angle(transform.right, myRigidbody.velocity)) < 5f)
        {
            angle = 0;
        }

        if (angle != 0)
        {
            myRigidbody.MoveRotation(myRigidbody.rotation + rotateSpeed * angle * Time.fixedDeltaTime);
        }
    }


    // Status Checks
    //
    //
    // Checking enemy hit player
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hitPlayer)
        {
            hitPlayer = true;
            Vector3 localScale = transform.localScale;
            //localScale.y *= -1;
            transform.localScale = localScale;
        }
    }

    // Checks if player is in range
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    // Changing Creature Momentum Direction
    IEnumerator ChangeCreatureTurn()
    {
        Vector3 localScale = transform.localScale;

        while (true)
        {
            yield return new WaitForSeconds(turnInterval);
            creatureTurn = !creatureTurn; // Toggle the value of creatureTurn
            localScale.x *= -1f;
        }
    }

    //private bool CheckDirection()
    //{
    //    if()
    //}
}

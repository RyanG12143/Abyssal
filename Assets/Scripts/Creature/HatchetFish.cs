using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class HatchetFish : MonoBehaviour
{
    public enum EnemyAction
    {
        Wander,
        Seek,
        Die,
    }

    private GameObject player;
    public EnemyAction currState = EnemyAction.Wander;

    public Transform target;
    Rigidbody2D myRigidbody;
    
    // Editable movement variables
    public float range = 15f;
    public float moveSpeed = 3f;
    public float moveDistance = 5f;
    public float turnInterval = 2.0f;
    public float slowTimeInterval = 0.5f;

    // Bool's for creature state changes
    public bool hitPlayer = false;
    public bool hitByTorpedo = false;
    private bool isFacingRight = true;
    private bool creatureTurn = false;
    private bool upFlipped = false;
    private bool slowTimeActive = false;
    private bool slowTimeCancel = false;

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
        //stateSwitch();

        if (!slowTimeCancel)
        {
            facingUpdate();
        }
    }
    
    // Switches the states of the enemy creature
    private void stateSwitch()
    {
        // Checks what state to be in
        if (hitByTorpedo)
        {
            currState = EnemyAction.Die;
        }
        else if (IsPlayerInRange(range))
        {
            currState = EnemyAction.Seek;
        }
        else
        {
            currState = EnemyAction.Wander;
        }

            // Switches current state based on currState
            switch (currState)
            {
                case EnemyAction.Wander:
                    Wander();
                    break;
                case EnemyAction.Seek:
                    Seek();
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
            localScale.y = 1;
            transform.localScale = localScale;
            upFlipped = true;
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
    void Seek()
    {
        //Vector2 targetPosition = target.position;
        //Vector2 currentPosition = transform.position;

        //Vector2 direction = (targetPosition - currentPosition).normalized;

        //// Use LookAt to make the enemy face the player;
        //transform.right = myRigidbody.velocity;

        //myRigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        //buffer = true;
        flip();
        correctFlip();
    }

    // Die method
    void Die()
    {
        if (slowTimeActive == true)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x * 0.2f, myRigidbody.velocity.y * 0.2f);
            slowTimeActive = false;
        }
    }

    // Helper methods
    //
    //
    // Flipping sprite at critical points (Looking Straight Up and Down)
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

    // Corrects Y flip after wander method and before die method (Edge Cases)
    private void correctFlip()
    {
        Vector3 localScale = transform.localScale;

        if (upFlipped)
        {
            localScale.y *= -1;
            transform.localScale = localScale;
            upFlipped = false;
        }
    }

    // Updates facing direction of creature based on velocity
    private void facingUpdate()
    {
        // Velocity Based Direction
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        transform.right = myRigidbody.velocity;

        float angle;
        float rotateSpeed = 2;

        angle = Mathf.Sign(Vector2.SignedAngle(transform.up, myRigidbody.velocity));

        // This is to stop overrotation
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
    // Checking if enemy hit player or Torpedo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Nightingale" && !hitPlayer && !hitByTorpedo)
        {
            hitPlayer = true;

            Vector3 localScale = transform.localScale;
            localScale.y *= -1;
            transform.localScale = localScale;
        }
        else if (collision.gameObject.name == "Torpedo2(Clone)" && hitByTorpedo == false)
        {
            hitByTorpedo = true;
            myRigidbody.bodyType = RigidbodyType2D.Dynamic;
            myRigidbody.gravityScale = 0.01f;
            StartCoroutine(deathSlowTime());
            StartCoroutine(deathSlowTimeCancel());
            correctFlip();
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
            creatureTurn = !creatureTurn;
            localScale.x *= -1f;
        }
    }

    // Time to slow down after dying
    IEnumerator deathSlowTime()
    {
        if (!slowTimeCancel)
        {
            yield return new WaitForSeconds(slowTimeInterval);
            slowTimeActive = true;
        }
    }

    //Cancels the slow time after a given time
    IEnumerator deathSlowTimeCancel()
    {
        yield return new WaitForSeconds(4);
        slowTimeCancel = true;
    }
}

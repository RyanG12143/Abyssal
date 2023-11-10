using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class SwordFish : MonoBehaviour
{
    public enum EnemyAction
    {
        Wander,
        Prime,
        Dash,
        Die,
    }

    private GameObject player;
    public EnemyAction currState = EnemyAction.Wander;
    public Vector2 oneDirection;
    public Transform target;
    Rigidbody2D myRigidbody;
    
    // Editable movement variables
    public float range = 6f;
    public float moveSpeed = 2f;
    public float dashSpeed = 8f;
    public float dashChargeTimer = 2f;
    public float dashCooldownTimer = 5f;
    private float turnInterval = 5.0f;
    private float slowTimeInterval = 0.5f;

    // Bool's for creature state changes
    private bool hitPlayer = false;
    private bool hitByTorpedo = false;
    private bool isFacingRight = true;
    private bool creatureTurn = false;
    private bool upFlipped = false;
    private bool slowTimeActive = false;
    private bool slowTimeCancel = false;
    public bool dashPrimed = false;
    public bool dashOnCooldown = true;
    public bool directionTaken = false;
    public bool dashActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(ChangeCreatureTurn());
        StartCoroutine(DashCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        stateSwitch();

        if (!slowTimeCancel && currState != EnemyAction.Dash)
        {
            FacingUpdate();
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
        else if (dashPrimed && !dashOnCooldown)
        {
            currState = EnemyAction.Dash;
        }
        else if (IsPlayerInRange(range) && !dashOnCooldown && !dashActive)
        {
            currState = EnemyAction.Prime;
        }
        else if (currState != EnemyAction.Prime)
        {
            currState = EnemyAction.Wander;
        }

        // Switches current state based on currState
        switch (currState)
        {
            case EnemyAction.Wander:
                Wander();
                break;
            case EnemyAction.Prime:
                Prime();
                break;
            case EnemyAction.Dash:
                Dash();
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
        dashPrimed = false;
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
    void Prime()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        transform.right = myRigidbody.velocity;

        myRigidbody.velocity = new Vector2(direction.x * 0.1f, direction.y * 0.1f);
        StartCoroutine(DashCharge());
        Flip();
        CorrectFlip();
    }

    // Run creature state
    void Dash()
    {
        // Single direction at time of dash
        OneDirection();
        directionTaken = true;
        dashActive = true;
        //myRigidbody.velocity = new Vector2(oneDirection.x * dashSpeed, oneDirection.y * dashSpeed);
        myRigidbody.velocity = new Vector2(oneDirection.x * dashSpeed, oneDirection.y * dashSpeed);
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
    private void Flip()
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
    private void CorrectFlip()
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
    private void FacingUpdate()
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
            myRigidbody.velocity = new Vector2(0, 0);
            Vector3 localScale = transform.localScale;
            transform.localScale = localScale;
        }
        else if (collision.gameObject.name == "Torpedo2(Clone)" && hitByTorpedo == false)
        {
            hitByTorpedo = true;
            myRigidbody.bodyType = RigidbodyType2D.Dynamic;
            myRigidbody.gravityScale = 0.01f;
            StartCoroutine(deathSlowTime());
            StartCoroutine(deathSlowTimeCancel());
            CorrectFlip();
        }
        else if (collision.gameObject.name == "Monstrosquid")
        {
            Destroy(gameObject);
        }
        
        dashPrimed = false;
        if (dashActive)
        {
            StartCoroutine(DashCooldown());
            dashActive = false;
            directionTaken = false;
        }
    }

    // Gets one direction
    private void OneDirection()
    {
        if (!directionTaken)
        {
            Vector2 targetPosition = target.position;
            Vector2 currentPosition = transform.position;

            oneDirection = (targetPosition - currentPosition).normalized;
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

    // Buffer timer so creature gets further away
    IEnumerator DashCharge()
    {
        yield return new WaitForSeconds(dashChargeTimer);
        dashPrimed = true;
    }

    IEnumerator DashCooldown()
    {
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldownTimer);
        dashOnCooldown = false;
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

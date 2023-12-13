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
        Stunned,
    }

    private GameObject player;
    public GameObject stunAnimation;
    public EnemyAction currState = EnemyAction.Wander;
    public Vector2 oneDirection;
    public Transform target;
    Rigidbody2D rb;
    
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

    // Stun variables
    public bool stunned = false;
    private float stunTime = 4;

    // Animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(ChangeCreatureTurn());
        StartCoroutine(DashCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        stateSwitch();
        FacingUpdate();
        FlippingUpdate();
    }
    
    // Switches the states of the enemy creature
    private void stateSwitch()
    {
        // Checks what state to be in
        if (hitByTorpedo && stunned)
        {
            currState = EnemyAction.Stunned;
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
            case EnemyAction.Stunned:
                Stunned();
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
        upFlipped = false;

        Vector3 localScale = transform.localScale;
        localScale.y = 1;
        transform.localScale = localScale;

        if (creatureTurn)
        {
            rb.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    // Follow creature state
    void Prime()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        transform.right = rb.velocity;

        rb.velocity = new Vector2(direction.x * 0.1f, direction.y * 0.1f);
        StartCoroutine(DashCharge());
    }

    // Run creature state
    void Dash()
    {
        // Single direction at time of dash
        OneDirection();
        directionTaken = true;
        dashActive = true;
        //rb.velocity = new Vector2(oneDirection.x * dashSpeed, oneDirection.y * dashSpeed);
        rb.velocity = new Vector2(oneDirection.x * dashSpeed, oneDirection.y * dashSpeed);
    }

    // Stunned
    void Stunned()
    {
        animator.SetBool("stunned", true);
        stunAnimation.SetActive(true);
        
    }

    // Helper methods
    //
    //
    // Sets facing direction to velocity direction
    private void FacingUpdate()
    {
        transform.right = rb.velocity;

        float angle;
        float rotateSpeed = 2;

        angle = Mathf.Sign(Vector2.SignedAngle(transform.up, rb.velocity));

        // This is to stop overrotation
        if (Mathf.Abs(Vector2.Angle(transform.right, rb.velocity)) < 5f)
        {
            angle = 0;
        }

        if (angle != 0)
        {
            rb.MoveRotation(rb.rotation + rotateSpeed * angle * Time.fixedDeltaTime);
        }
    }

    private void FlippingUpdate()
    {
        Vector3 localScale = transform.localScale;

        bool test1 = transform.eulerAngles.z > 90f && transform.eulerAngles.z < 270f;

        if (test1 != upFlipped)
        {
            localScale.y *= -1;
            transform.localScale = localScale;
            upFlipped = !upFlipped;
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
            rb.velocity = new Vector2(0, 0);
            Vector3 localScale = transform.localScale;
            transform.localScale = localScale;
        }
        else if (collision.gameObject.name == "Torpedo2(Clone)")
        {
            hitByTorpedo = true;
            stunned = true;
            hitPlayer = true;
            rb.velocity = new Vector2(0, 0);
            Vector3 localScale = transform.localScale;
            transform.localScale = localScale;
            StartCoroutine(Timer(stunTime));
        }
        else if (collision.gameObject.name == "Monstrosquid")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Nightingale" && !stunned)
        {
            Health.GetInstance().damage();
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

    // Stun Timer
    IEnumerator Timer(float timer)
    {
        yield return new WaitForSeconds(timer);
        stunned = false;
        animator.SetBool("stunned", false);
        stunAnimation.SetActive(false);
        dashActive = false;
        directionTaken = false;
        hitByTorpedo = false;
        StartCoroutine(DashCooldown());
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
}

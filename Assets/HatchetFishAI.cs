using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;
using static UnityEditor.Rendering.InspectorCurveEditor;
using System;

public class HatchetFishAI : MonoBehaviour
{
    // A* variables
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    
    // Variables
    public float detectionRange = 5.0f;
    public float turnInterval = 2.0f;
    public float wanderBufferTime = 2.0f;
    public float slowTimeInterval = 0.5f;
    public float wanderSpeed = 2.0f;

    // Bool's for creature state change
    public bool hitPlayer = false;
    public bool hitByTorpedo = false;
    private bool isFacingRight = true;
    private bool creatureTurn = false;
    // Fix FLIPPING
    public bool upFlipped = true;
    private bool slowTimeActive = false;
    private bool slowTimeCancel = false;

    // State Enum
    public enum EnemyAction
    {
        Wander,
        Seek,
        Die,
    }

    public EnemyAction currState = EnemyAction.Wander;

    //player declaration
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        // Declaring player
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Starting creature turning
        StartCoroutine(ChangeCreatureTurn());
    }
    
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Pathfinding.Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update
    void FixedUpdate()
    {
        stateSwitch();
        AAI();
        if (!slowTimeCancel)
        {
            FacingUpdate();
        }
    }

    // Core AI
    private void AAI()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    private void stateSwitch()
    {
        // Checks what state to be in
        if (hitByTorpedo)
        {
            currState = EnemyAction.Die;
        }
        else if (IsPlayerInRange(detectionRange) && currState != EnemyAction.Die)
        {
            currState = EnemyAction.Seek;
        }
        else if (!IsPlayerInRange(detectionRange) && currState != EnemyAction.Die)
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

    // Wander
    void Wander()
    {
        Vector3 localScale = transform.localScale;

        correctFlip();

        if (creatureTurn)
        {
            rb.velocity = new Vector2(wanderSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-wanderSpeed, 0f);
        }
    }

    // Seek
    void Seek()
    {
        AAI();
        correctFlip();
    }

    // Die
    void Die()
    {
        if (slowTimeActive == true)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.2f, rb.velocity.y * 0.2f);
            slowTimeActive = false;
        }
    }

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

    // Changing Creature Momentum Direction
    IEnumerator ChangeCreatureTurn()
    {
        Vector3 localScale = transform.localScale;

        while (true)
        {
            yield return new WaitForSeconds(turnInterval);
            creatureTurn = !creatureTurn;
            //localScale.x *= -1f;
        }

    }

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

    // Time to slow down after dying
    IEnumerator deathSlowTime()
    {
        if (!slowTimeCancel)
        {
            yield return new WaitForSeconds(slowTimeInterval);
            yield return slowTimeActive = true;
        }
    }

    // Cancels the slow time after a given time
    IEnumerator deathSlowTimeCancel()
    {
        yield return new WaitForSeconds(4);
        slowTimeCancel = true;
    }

    // Checks if the player
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;
using static UnityEditor.Rendering.InspectorCurveEditor;

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
    public float wanderSpeed = 2.0f;

    // Bool's for creature state change
    private bool hitPlayer = false;
    private bool hitByTorpedo = false;
    private bool creatureTurn = false;
    
    // Fix flipping
    public bool upFlipped = false;

    // Stun variables
    public bool stunned = false;
    private float stunTime = 5f;

    // State Enum
    public enum EnemyAction
    {
        Wander,
        Seek,
        Stunned,
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
        FacingUpdate();
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
        if (stunned)
        {
            currState = EnemyAction.Stunned;
        }
        else if (IsPlayerInRange(detectionRange) && !stunned)
        {
            currState = EnemyAction.Seek;
        }
        else if (!IsPlayerInRange(detectionRange) && !stunned)
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
            case EnemyAction.Stunned:
                Stunned();
                break;
        }
    }

    // Wander
    void Wander()
    {
        upFlipped = false;

        Vector3 localScale = transform.localScale;
        localScale.y = 1;
        transform.localScale = localScale;

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
        Vector3 localScale = transform.localScale;

        bool test1 = transform.eulerAngles.z > 90f && transform.eulerAngles.z < 270f;
        //Debug.Log("Test value : " + test1 + "  Flipped: " + upFlipped);
        //Debug.Log("Flipped" + transform.eulerAngles.z);

        if (test1 != upFlipped)
        {
            localScale.y *= -1;
            transform.localScale = localScale;
            upFlipped = !upFlipped;
        }

        AAI();
    }

    // Stunned
    void Stunned()
    {
        //Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        //Vector2 force = -direction * speed/10 * Time.deltaTime;

        //rb.AddForce(force);
    }

    // Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hitPlayer && !stunned)
        {
            
        }
        else if (collision.gameObject.name == "Torpedo2(Clone)" && stunned == false)
        {
            stunned = true;
            //rb.bodyType = RigidbodyType2D.Dynamic;
            //rb.gravityScale = 0.01f;
            currentWaypoint = 0;
            StartCoroutine(StunTimer());
        }
    }


    // Helper Methods
    //
    //
    // Changing Creature Momentum Direction
    IEnumerator ChangeCreatureTurn()
    {
        Vector3 localScale = transform.localScale;

        while (true)
        {
            yield return new WaitForSeconds(turnInterval);
            creatureTurn = !creatureTurn;
        }
    }

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

    // Stun Timer
    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        stunned = false;
    }

    // Checks if the player
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
}

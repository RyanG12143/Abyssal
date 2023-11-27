using System.Collections;
using UnityEngine;
using Pathfinding;
using System.IO;

public class SeaMonkeyScene : MonoBehaviour
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
    //
    // Wander variables
    public float detectionRange = 5.0f;
    public float wanderSpeed = 2.0f;

    // Bool's for creature state change
    private bool hitPlayer = false;
    public bool hitByTorpedo = false;

    // Fix flipping
    public bool upFlipped = false;

    // Animator
    public Animator animator;

    // State Enum
    public enum EnemyAction
    {
        Primed,
        Lunge,
        Run,
        Leave,
    }

    public EnemyAction currState = EnemyAction.Primed;

    //player declaration
    private GameObject player;
    public GameObject objectToSpawn;
    public GameObject stunAnimation;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        // Declaring player
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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

    // Update is called once per frame
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

    // Switches the states of the enemy creature
    private void stateSwitch()
    {
        if (hitByTorpedo)
        {
            currState = EnemyAction.Leave;
        }
        else if (IsPlayerInRange(detectionRange) && !hitByTorpedo)
        {
            currState = EnemyAction.Lunge;
        }
        else if (!IsPlayerInRange(detectionRange) && !hitByTorpedo)
        {
            currState = EnemyAction.Run;
        }
        else
        {
            currState = EnemyAction.Primed;
        }

        // Switches current state based on currState
        switch (currState)
        {
            case EnemyAction.Primed:
                Primed();
                break;
            case EnemyAction.Run:
                Run();
                break;
            case EnemyAction.Lunge:
                Lunge();
                break;
            case EnemyAction.Leave:
                Leave();
                break;
        }
    }


    // Different creature state methods
    //
    //
    // Wander creature state
    void Primed()
    {

    }

    // Lunge creature state
    void Lunge()
    {
        FlippingUpdate();
        AAI();
    }

    // Run creature state
    void Run()
    {
        //Vector2 targetPosition = target.position;
        //Vector2 currentPosition = transform.position;

        //Vector2 direction = (targetPosition - currentPosition).normalized;

        //// Invert the direction for running away
        //direction = -direction;

        //rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        FlippingUpdate();
        AAI();
    }

    // Leaving level
    void Leave()
    {
        FlippingUpdate();
        AAI();
    }

    // Helper methods
    //
    //
    // Flipping sprite at critical points (Looking Straight Up and Down)
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
        if (collision.gameObject.tag == "Player")
        {
            hitPlayer = true;

            //Vector3 localScale = transform.localScale;
            //localScale.y *= -1;
            //transform.localScale = localScale;
            Oxygen.GetInstance().activateOxygen();
        }
        else if (collision.gameObject.tag == "Torpedo" && hitByTorpedo == false)
        {
            hitByTorpedo = true;
        }
    }

    // Checks if player is in range
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
}

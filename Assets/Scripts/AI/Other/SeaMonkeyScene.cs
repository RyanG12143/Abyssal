using System.Collections;
using UnityEngine;
using Pathfinding;

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
    public float changeDirectionCooldown = 2.0f;
    public float wanderSpeed = 2.0f;

    // Bool's for creature state change
    private bool hitPlayer = false;
    private bool creatureTurn = true;
    public bool hitByTorpedo = false;

    // Fix flipping
    public bool upFlipped = false;

    // Stun variables
    public bool stunned = false;
    private float stunTime = 5f;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        stateSwitch();
        AAI();
        facingUpdate();
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
        if (stunned)
        {
            currState = EnemyAction.Leave;
        }
        else if (IsPlayerInRange(detectionRange) && !stunned)
        {
            currState = EnemyAction.Lunge;
        }
        else if (!IsPlayerInRange(detectionRange) && !stunned)
        {
            currState = EnemyAction.Run;
        } else
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

    // Follow creature state
    void Lunge()
    {
        FlippingUpdate();
        AAI();
    }

    // Run creature state
    void Run()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Invert the direction for running away
        direction = -direction;

        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        FlippingUpdate();
    }

    // Die method
    void Leave()
    {
        
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
            Oxygen.GetInstance().activateOxygen();
        }
        else if (collision.gameObject.name == "Torpedo2(Clone)" && hitByTorpedo == false)
        {
            if (hitPlayer)
            {
                Vector3 dropModify = new Vector3(1f, -0.5f, 0f);
                Instantiate(objectToSpawn, transform.position + dropModify, objectToSpawn.transform.rotation);
            }
            hitByTorpedo = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0.01f;
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
            yield return new WaitForSeconds(changeDirectionCooldown);
            creatureTurn = !creatureTurn;
        }
    }

    // Buffer timer so creature gets further away
    IEnumerator wanderBuffer()
    {
        yield return new WaitForSeconds(wanderBufferTime);
        buffer = false;
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

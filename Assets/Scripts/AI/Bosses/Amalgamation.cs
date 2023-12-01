using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Amalgamation : MonoBehaviour
{
    // variables
    public Transform currentTarget;
    private Transform nextTarget;
    public float speed = 400f;
    public float chargeSpeed = 200f;
    private float nextWaypointDistance = 6f;
    private bool hidden = false;
    public float detectionRange = 10;
    public float jumpscareTime = 2;
    public bool jumpscare = false;

    public Transform enemyGFX;

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public GameObject getCamera;

    // Fix flipping
    public bool upFlipped = false;
    private GameObject player;
    public GameObject stunAnimation;

    // State Enum
    public enum EnemyAction
    {
        Hide,
        Jumpscare,
        Chase,
        Primed,
        Charge,
        Wait,
        Roar,
    }

    public EnemyAction currState = EnemyAction.Chase;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        // Declaring player
        player = GameObject.FindGameObjectWithTag("Player");
        currentTarget = player.GetComponent<Transform>();
        nextTarget = GameObject.Find("nextTarget").GetComponent<Transform>();
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
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
        FacingUpdate();
        FlippingUpdate();
        Chase();
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
        //if (currState == EnemyAction.Hide && IsPlayerInRange(detectionRange))
        //{
        //    currState = EnemyAction.Jumpscare;
        //}
        //else if (currState == EnemyAction.Jumpscare && jumpscare)
        //{
        //    currState = EnemyAction.Chase;
        //}
        //else if (hitPlayer)
        //{
        //    currState = EnemyAction.Run;
        //}
        //else
        //{
        //    currState = EnemyAction.Hide;
        //}

        //Switches current state based on currState
        //switch (currState)
        //{
        //    case EnemyAction.Hide:
        //        Hide();
        //        break;
        //    case EnemyAction.Jumpscare:
        //        Jumpscare();
        //        break;
        //    case EnemyAction.Lunge:
        //        Lunge();
        //        break;
        //    case EnemyAction.Leave:
        //        Leave();
        //        break;
        //}
    }

    // Different creature state methods
    //
    //
    void Hide()
    {

    }

    void Jumpscare()
    {
        getCamera.GetComponent<CameraController>().setCameraSize(8f);
        StartCoroutine(jumpscareTimer(jumpscareTime));
    }

    void Chase()
    {
        AAI();
    }

    void Primed()
    {

    }

    void Charge()
    {
        AAI();
    }

    void Roar()
    {

    }

    void Wait()
    {

    }

    // Helper Methods
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
        //if (collision.gameObject.tag == "Player" && !hitPlayer)
        //{
        //    hitPlayer = true;
        //    animator.SetBool("oxygenTank", true);
        //    Oxygen.GetInstance().activateOxygen();
        //}
        //else if (collision.gameObject.tag == "Torpedo" && hitByTorpedo == false && hitPlayer == true)
        //{
        //    hitByTorpedo = true;
        //    if (hitPlayer == true)
        //    {
        //        Instantiate(objectToSpawn, transform.position, objectToSpawn.transform.rotation);
        //        stunAnimation.SetActive(true);
        //        animator.SetBool("stunned", true);
        //    }
        //}
    }

    //timer
    IEnumerator jumpscareTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        jumpscare = true;

    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
}
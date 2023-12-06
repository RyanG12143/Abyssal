using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Amalgamation : MonoBehaviour
{
    // variables
    public Transform currentTarget;
    private Transform nextTarget;
    public Transform chargeTarget;
    private float currentSpeed;
    private float speed = 1000f;
    private float chargeSpeed = 3000f;
    private float jumpscareSpeed = 200f;
    private float primedSpeed = 10f;
    private float nextWaypointDistance = 4f;
    private float jumpscareTime = 2;
    private float chargeTargetRange = 30;
    private float chargeEndTimer = 5;
    private float chargeUpTimer = 5;
    private bool hitWallCharging = false;
    private bool jumpscare = false;
    private bool chaseActive = false;
    private bool eggActive = false;
    private bool waitTimer = false;
    private bool roarCooldown = true; // set false
    private bool chargeEvent = false;
    private bool charge1 = false;
    private float waitTime;
    private bool slowForceApplied = false;

    // Animator
    public Animator animator;

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

    public EnemyAction currState = EnemyAction.Hide;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = jumpscareSpeed;
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        // Declaring taragets
        player = GameObject.FindGameObjectWithTag("Player");
        currentTarget = player.GetComponent<Transform>();
        nextTarget = GameObject.Find("nextTarget").GetComponent<Transform>();
        chargeTarget = GameObject.Find("chargeTarget").GetComponent<Transform>();
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
        stateActivate();
        FacingUpdate();
        FlippingUpdate();
        if(chaseActive == true)
        {
            stateSwitch();
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
        Vector2 force = direction * currentSpeed * Time.deltaTime;

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
        if(waitTimer == true)
        {
            currState = EnemyAction.Wait;
        }
        else if(!roarCooldown)
        {
            currState = EnemyAction.Roar;
        }
        else if (!chargeEvent && IsChargeWallInRange(chargeTargetRange) && !charge1)
        {
            currState = EnemyAction.Primed;
        }
        else if (chargeEvent && IsChargeWallInRange(chargeTargetRange))
        {
            currState = EnemyAction.Charge;
        }
        else
        {
            currState = EnemyAction.Chase;
        }
    }


    private void stateActivate()
    {
        //Switches current state based on currState
        switch (currState)
        {
            case EnemyAction.Hide:
                Hide();
                break;
            case EnemyAction.Jumpscare:
                Jumpscare();
                break;
            case EnemyAction.Chase:
                Chase();
                break;
            case EnemyAction.Primed:
                Primed();
                break;
            case EnemyAction.Charge:
                Charge();
                break;
            case EnemyAction.Wait:
                Wait();
                break;
            case EnemyAction.Roar:
                Roar();
                break;
        }
    }

    // Different creature state methods
    //
    // Pre Scare States
    void Hide()
    {
        if (eggActive)
        {
            currState = EnemyAction.Jumpscare;
        }
    }

    void Jumpscare()
    {
        AAI();
        getCamera.GetComponent<CameraController>().setCameraSize(5f);
        StartCoroutine(jumpscareTimer(jumpscareTime));
    }

    // Post Scare States
    void Chase()
    {
        AAI();
        if (IsPlayerInRange(5))
        {
            rb.GetComponent<Rigidbody2D>().drag = 5;
        }
        else
        {
            rb.GetComponent<Rigidbody2D>().drag = 1.5f;
        }
    }

    void Primed()
    {
        if(!slowForceApplied)
        {
            //Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            //Vector2 force = -direction * speed * Time.deltaTime * 50;
            //rb.AddForce(force);
            rb.GetComponent<Rigidbody2D>().drag = 1000;
            slowForceApplied = true;
        }
        else
        {
            rb.GetComponent<Rigidbody2D>().drag = 1;
        }
        
        currentSpeed = primedSpeed;
        currentTarget = chargeTarget;
        StartCoroutine(chargingTimer(chargeUpTimer));
        AAI();
    }

    void Charge()
    {
        currentSpeed = chargeSpeed;
        if (hitWallCharging)
        {
            animator.SetBool("charge", false);
            currentSpeed = speed;
            animator.SetBool("stunned", true);
            stunAnimation.SetActive(true);
            StartCoroutine(endChargeTimer(chargeEndTimer));
        }
        else
        {
            animator.SetBool("charge", true);
            AAI();
        }
    }

    void Roar()
    {
        animator.SetBool("roar", true);
    }

    void Wait()
    {
        waitingTimer(waitTime);
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
        if (collision.gameObject.tag == "Player")
        {
            // Ask Ryan If Right!!!!!
            Health.GetInstance().kill();
        }
        else if (collision.gameObject.tag == "BossChargeWall")
        {
            hitWallCharging = true;
            charge1 = true;
        }
        
    }

    // Activation from egg
    public void StartChase()
    {
        StartCoroutine(chasingTimer(2));
    }


    //timers
    //
    //
    //
    IEnumerator jumpscareTimer(float timer)
    {
        yield return new WaitForSeconds(2);
        animator.SetBool("active", true);
        yield return new WaitForSeconds(timer);
        chaseActive = true;
        currentSpeed = speed;

    }

    IEnumerator chasingTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        eggActive = true;

    }

    IEnumerator waitingTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        waitTimer = true;

    }

    IEnumerator chargingTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        chargeEvent = true;

    }

    IEnumerator endChargeTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        chargeEvent = false;
        currentTarget = player.GetComponent<Transform>();
        currentSpeed = speed;
        stunAnimation.SetActive(false);
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private bool IsChargeWallInRange(float range)
    {
        return Vector3.Distance(transform.position, chargeTarget.transform.position) <= range;
    }
}
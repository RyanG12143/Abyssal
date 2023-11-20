//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UIElements;
//using UnityEngine.UIElements.Experimental;

//public class SeaMonkeyScene : MonoBehaviour
//{
//    // A* variables
//    public Transform target;
//    public float speed = 200f;
//    public float nextWaypointDistance = 3f;

//    public Transform enemyGFX;

//    Pathfinding.Path path;
//    int currentWaypoint = 0;
//    bool reachedEndOfPath = false;

//    Seeker seeker;
//    Rigidbody2D rb;

//    // Variables
//    //
//    // Wander variables
//    public float detectionRange = 5.0f;
//    public float changeDirectionCooldown = 2.0f;
//    public float wanderSpeed = 2.0f;

//    // Bool's for creature state change
//    private bool hitPlayer = false;
//    private bool creatureTurn = true;

//    // Fix flipping
//    public bool upFlipped = false;

//    // Stun variables
//    public bool stunned = false;
//    private float stunTime = 5f;

//    // Animator
//    public Animator animator;

//    // State Enum
//    public enum EnemyAction
//    {
//        Primed,
//        Lunge,
//        Run,
//        Leave,
//    }

//    public EnemyAction currState = EnemyAction.Primed;

//    //player declaration
//    private GameObject player;

//    public GameObject stunAnimation;


//    //Old Variables
//    //public enum EnemyAction
//    //{
//    //    Lunge,
//    //    Run,
//    //    Leave,
//    //}

//    //private GameObject player;
//    //public GameObject objectToSpawn;
//    //public EnemyAction currState = EnemyAction.Wander;

//    //public Transform target;
//    //Rigidbody2D myRigidbody;

//    //// Editable movement variables
//    //public float range = 2f;
//    //public float moveSpeed = 2f;
//    //public float moveDistance = 5f;
//    //public float turnInterval = 2.0f;
//    //public float wanderBufferTime = 2.0f;
//    //public float slowTimeInterval = 0.5f;

//    //// Bool's for creature state changes
//    //public bool hitPlayer = false;
//    //public bool hitByTorpedo = false;
//    //private bool isFacingRight = true;
//    //private bool creatureTurn = false;
//    //private bool upFlipped = false;
//    //private bool buffer = false;
//    //private bool slowTimeActive = false;
//    //private bool slowTimeCancel = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        myRigidbody = GetComponent<Rigidbody2D>();
//        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
//        StartCoroutine(ChangeCreatureTurn());
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        stateSwitch();

//        if (!slowTimeCancel)
//        {
//            facingUpdate();
//        }
//    }
    
//    // Switches the states of the enemy creature
//    private void stateSwitch()
//    {
//        // Checks what state to be in
//        if (hitByTorpedo)
//        {
//            currState = EnemyAction.Die;
//        }
//        else if (IsPlayerInRange(range) && currState != EnemyAction.Die && hitPlayer == true)
//        {
//            currState = EnemyAction.Run;
//        }
//        else if (IsPlayerInRange(range) && currState != EnemyAction.Die && currState != EnemyAction.Run)
//        {
//            currState = EnemyAction.Follow;
//        }
//        else if (!IsPlayerInRange(range) && currState != EnemyAction.Die)
//        {
//            currState = EnemyAction.Wander;
//        }

//        // Switches current state based on currState
//        switch (currState)
//        {
//            case EnemyAction.Wander:
//                Wander();
//                break;
//            case EnemyAction.Follow:
//                Follow();
//                break;
//            case EnemyAction.Run:
//                Run();
//                break;
//            case EnemyAction.Die:
//                Die();
//                break;
//        }
//    }


//    // Different creature state methods
//    //
//    //
//    // Wander creature state
//    void Wander()
//    {
//        StartCoroutine (wanderBuffer());

//        Vector3 localScale = transform.localScale;
        


//        if(buffer == false)
//        {
//            if (transform.up.y < 0f)
//            {
//                localScale.y = 1;
//                transform.localScale = localScale;
//                upFlipped = true;
//            }

//            if (creatureTurn)
//            {
//                myRigidbody.velocity = new Vector2(moveSpeed, 0f);
//            }
//            else
//            {
//                myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
//            }
//        }
//    }

//    // Follow creature state
//    void Follow()
//    {
//        Vector2 targetPosition = target.position;
//        Vector2 currentPosition = transform.position;

//        Vector2 direction = (targetPosition - currentPosition).normalized;

//        //// Use LookAt to make the enemy face the player;
//        transform.right = myRigidbody.velocity;

//        myRigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

//        buffer = true;
//        flip();
//        correctFlip();
//    }

//    // Run creature state
//    void Run()
//    {
//        Vector2 targetPosition = target.position;
//        Vector2 currentPosition = transform.position;

//        Vector2 direction = (targetPosition - currentPosition).normalized;
        
//        // Invert the direction for running away
//        direction = -direction;

//        myRigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

//        buffer = true;
//        flip();
//        correctFlip();
//    }

//    // Die method
//    void Die()
//    {
//        if (slowTimeActive == true)
//        {
//            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x * 0.2f, myRigidbody.velocity.y * 0.2f);
//            slowTimeActive = false;
//        }
//    }

//    // Helper methods
//    //
//    //
//    // Flipping sprite at critical points (Looking Straight Up and Down)
//    private void flip()
//    {
//        Vector2 targetPosition = target.position;
//        Vector2 currentPosition = transform.position;

//        Vector2 direction = (targetPosition - currentPosition).normalized;

//        if ((isFacingRight && direction.x < -0.01) || (!isFacingRight && direction.x > 0.01))
//        {
//            isFacingRight = !isFacingRight;

//            Vector3 localScale = transform.localScale;
//            localScale.y *= -1;
//            transform.localScale = localScale;
//        }
//    }

//    // Corrects Y flip after wander method and before die method (Edge Cases)
//    private void correctFlip()
//    {
//        Vector3 localScale = transform.localScale;

//        if (upFlipped)
//        {
//            localScale.y *= -1;
//            transform.localScale = localScale;
//            upFlipped = false;
//        }
//    }

//    // Updates facing direction of creature based on velocity
//    private void facingUpdate()
//    {
//        // Velocity Based Direction
//        Vector2 targetPosition = target.position;
//        Vector2 currentPosition = transform.position;

//        Vector2 direction = (targetPosition - currentPosition).normalized;

//        transform.right = myRigidbody.velocity;

//        float angle;
//        float rotateSpeed = 2;

//        angle = Mathf.Sign(Vector2.SignedAngle(transform.up, myRigidbody.velocity));

//        // This is to stop overrotation
//        if (Mathf.Abs(Vector2.Angle(transform.right, myRigidbody.velocity)) < 5f)
//        {
//            angle = 0;
//        }

//        if (angle != 0)
//        {
//            myRigidbody.MoveRotation(myRigidbody.rotation + rotateSpeed * angle * Time.fixedDeltaTime);
//        }
//    }


//    // Status Checks
//    //
//    //
//    // Checking if enemy hit player or Torpedo
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.name == "Nightingale" && !hitPlayer && !hitByTorpedo)
//        {
//            hitPlayer = true;

//            Vector3 localScale = transform.localScale;
//            localScale.y *= -1;
//            transform.localScale = localScale;
//            Oxygen.GetInstance().activateOxygen();
//        }
//        else if (collision.gameObject.name == "Torpedo2(Clone)" && hitByTorpedo == false)
//        {
//            if(hitPlayer)
//            {
//                Vector3 dropModify = new Vector3(1f, -0.5f, 0f);
//                Instantiate(objectToSpawn, transform.position + dropModify, objectToSpawn.transform.rotation);
//            }
//            hitByTorpedo = true;
//            myRigidbody.bodyType = RigidbodyType2D.Dynamic;
//            myRigidbody.gravityScale = 0.01f;
//            StartCoroutine(deathSlowTime());
//            StartCoroutine(deathSlowTimeCancel());
//            correctFlip();
//        }
//    }

//    // Checks if player is in range
//    private bool IsPlayerInRange(float range)
//    {
//        return Vector3.Distance(transform.position, player.transform.position) <= range;
//    }

//    // Changing Creature Momentum Direction
//    IEnumerator ChangeCreatureTurn()
//    {

//        Vector3 localScale = transform.localScale;

//        while (true)
//        {
//            yield return new WaitForSeconds(turnInterval);
//            creatureTurn = !creatureTurn;
//            localScale.x *= -1f;
//        }
//    }

//    // Buffer timer so creature gets further away
//    IEnumerator wanderBuffer()
//    {
//        yield return new WaitForSeconds(wanderBufferTime);
//        buffer = false;
//    }

//    // Time to slow down after dying
//    IEnumerator deathSlowTime()
//    {
//        if (!slowTimeCancel)
//        {
//            yield return new WaitForSeconds(slowTimeInterval);
//            slowTimeActive = true;
//        }
//    }

//    //Cancels the slow time after a given time
//    IEnumerator deathSlowTimeCancel()
//    {
//        yield return new WaitForSeconds(4);
//        slowTimeCancel = true;
//    }
//}
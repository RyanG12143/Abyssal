using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class Monstrosquid : MonoBehaviour
{
    public enum EnemyAction
    {
        Appear,
        Idle,
        Eat,
        Leave,
    }

    private GameObject player;
    private Animator myAnimator;
    public EnemyAction currState = EnemyAction.Appear;

    public Transform target;
    Rigidbody2D myRigidbody;

    // Editable movement variables
    public float range = 2f;
    public float moveSpeed = 2f;
    public float moveDistance = 5f;
    public float turnInterval = 2.0f;
    public float wanderBufferTime = 2.0f;
    public float slowTimeInterval = 0.5f;

    // Bool's for creature state changes
    public bool hitPlayer = false;
    public bool hitByTorpedo = false;
    private bool isFacingRight = true;
    private bool creatureTurn = false;
    private bool upFlipped = false;
    private bool buffer = false;
    private bool slowTimeActive = false;
    private bool slowTimeCancel = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        stateSwitch();
    }

    // Switches the states of the enemy creature
    private void stateSwitch()
    {
        // Checks what state to be in
        //if (hitByTorpedo)
        //{
        //    currState = EnemyAction.Leave;
        //}
        //else if (IsPlayerInRange(range) && currState != EnemyAction.Die && hitPlayer == true)
        //{
        //    currState = EnemyAction.Run;
        //}
        //else if (IsPlayerInRange(range) && currState != EnemyAction.Die && currState != EnemyAction.Run)
        //{
        //    currState = EnemyAction.Follow;
        //}
        //else if (!IsPlayerInRange(range) && currState != EnemyAction.Die)
        //{
        //    currState = EnemyAction.Wander;
        //}

        // Switches current state based on currState
        switch (currState)
        {
            case EnemyAction.Appear:
                Appear();
                break;
            case EnemyAction.Idle:
                Idle();
                break;
            case EnemyAction.Eat:
                Eat();
                break;
            case EnemyAction.Leave:
                Leave();
                break;
        }
    }


    // Different creature state methods
    //
    //

    void Appear()
    {
        myAnimator.SetTrigger(name: "jumpScare");
        currState = EnemyAction.Idle;
    }

    // Wander creature state
    void Idle()
    {
        //StartCoroutine(wanderBuffer());

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

    void Eat()
    {
        
    }

    // Die method
    void Leave()
    {
      
    }

    // Helper methods
    //
    //
    // Flipping sprite at critical points (Looking Straight Up and Down)
    //private void flip()
    //{
    //    Vector2 targetPosition = target.position;
    //    Vector2 currentPosition = transform.position;

    //    Vector2 direction = (targetPosition - currentPosition).normalized;

    //    if ((isFacingRight && direction.x < -0.01) || (!isFacingRight && direction.x > 0.01))
    //    {
    //        isFacingRight = !isFacingRight;

    //        Vector3 localScale = transform.localScale;
    //        localScale.y *= -1;
    //        transform.localScale = localScale;
    //    }
    //}

    // Corrects Y flip after wander method and before die method (Edge Cases)
    //private void correctFlip()
    //{
    //    Vector3 localScale = transform.localScale;

    //    if (upFlipped)
    //    {
    //        localScale.y *= -1;
    //        transform.localScale = localScale;
    //        upFlipped = false;
    //    }
    //}

    // Updates facing direction of creature based on velocity
    //private void facingUpdate()
    //{
    //    // Velocity Based Direction
    //    Vector2 targetPosition = target.position;
    //    Vector2 currentPosition = transform.position;

    //    Vector2 direction = (targetPosition - currentPosition).normalized;

    //    transform.right = myRigidbody.velocity;

    //    float angle;
    //    float rotateSpeed = 2;

    //    angle = Mathf.Sign(Vector2.SignedAngle(transform.up, myRigidbody.velocity));

    //    // This is to stop overrotation
    //    if (Mathf.Abs(Vector2.Angle(transform.right, myRigidbody.velocity)) < 5f)
    //    {
    //        angle = 0;
    //    }

    //    if (angle != 0)
    //    {
    //        myRigidbody.MoveRotation(myRigidbody.rotation + rotateSpeed * angle * Time.fixedDeltaTime);
    //    }
    //}


    // Status Checks
    //
    //
    // Checking if enemy hit player or Torpedo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Nightingale" && !hitPlayer && !hitByTorpedo)
        {
            hitPlayer = true;
        }
    }

    // Checks if player is in range
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Monstrosquid : MonoBehaviour
//{
//    private Animator myAnimator;

//    // Start is called before the first frame update
//    void Start()
//    {
//        myAnimator = GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        myAnimator.SetTrigger(name: "jumpScare");
//    }
//}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum EnemyState
{
    Wander,
    Follow,
    Run,
    Die,
}

public class EnemyController : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Wander;

    public Transform target;
    Rigidbody2D myRigidbody;

    public float range = 2f;
    public float moveSpeed = 2f;

    public bool hitPlayer = false;
    public bool hitByTorpedo = false;
    private bool isFacingRight = true;
    private bool creatureTurn = false;
    public float moveDistance = 5f;
    public float turnInterval = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(ChangeCreatureTurn());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case EnemyState.Wander:
                Wander();
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Run:
                Run();
                break;
            case EnemyState.Die:
                Die();
                break;
        }

        // Checks what state to be in
        if (IsPlayerInRange(range) && currState != EnemyState.Die && hitPlayer == true)
        {
            currState = EnemyState.Run;
        }
        else if (IsPlayerInRange(range) && currState != EnemyState.Die && currState != EnemyState.Run)
        {
            currState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }
        else if (hitByTorpedo)
        {
            currState = EnemyState.Die;
        }

        // Checking enemy hit player
        if (Vector3.Distance(transform.position, player.transform.position) <= 0.7)
        {
            hitPlayer = true;
        }

        // Flipping sprite
        flip();
    }

    // Checking if player is in range
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    // Wander method
    void Wander()
    {
        if (creatureTurn)
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }
    IEnumerator ChangeCreatureTurn()
    {
        while (true)
        {
            yield return new WaitForSeconds(turnInterval);
            creatureTurn = !creatureTurn; // Toggle the value of creatureTurn
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    // Follow method
    void Follow()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Use LookAt to make the enemy face the player
        transform.right = direction;

        myRigidbody.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }

    //private bool isTravelingRight()
    //{
    //    return transform.localScale.x > 0;
    //}

    // Run method

    void Run()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Use LookAt to make the enemy face the player
        transform.right = -direction;

        myRigidbody.velocity = new Vector2(-direction.x * moveSpeed, -direction.y * moveSpeed);
    }

    void Die()
    {
        Destroy(gameObject);
    }

   

    //flips
    private void flip()
    {
        Vector2 targetPosition = target.position;
        Vector2 currentPosition = transform.position;

        Vector2 direction = (targetPosition - currentPosition).normalized;

        if ((isFacingRight && direction.x < -0.1) || (!isFacingRight && direction.x > 0.1))
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.y *= -1;
            
            if (hitPlayer == false)
            {
                transform.localScale = localScale;
            } else
            {
                //need to impliment running away flipping
                transform.localScale = localScale;
            }
        }
    }

}

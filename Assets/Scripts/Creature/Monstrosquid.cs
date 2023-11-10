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
        Attack,
        Leave,
    }

    private GameObject player;
    public Animator animator;
    public EnemyAction currState = EnemyAction.Idle;

    public Transform target;
    Rigidbody2D myRigidbody;

    // Editable movement variables
    public float scareRange = 10f;
    public float playerRange = 4f;
    public float creatureRange = 4f;

    // Bool's for creature state changes
    public bool grabPlayer = false;
    public bool grabCreature = false;
    private bool jumpscare = true;
    private bool attacking = true;
    private bool leaveTrack = true;

    // Start is called before the first frame update
    void Start()
    {
        //myAnimator = GetComponent<Animator>();
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
        if (isCreatureInRange(creatureRange))
        {
            currState = EnemyAction.Leave;
        }
        else if (isPlayerInRange(playerRange))
        {
            currState = EnemyAction.Attack;
        }
        else if (isPlayerInRange(scareRange) && jumpscare)
        {
            currState = EnemyAction.Appear;
        }
        else
        {
            currState = EnemyAction.Idle;
        }

        // Switches current state based on currState
        switch (currState)
        {
            case EnemyAction.Appear:
                appear();
                break;
            case EnemyAction.Idle:
                idle();
                break;
            case EnemyAction.Attack:
                attack();
                break;
            case EnemyAction.Leave:
                leave();
                break;
        }
    }


    // Different creature state methods
    //
    //
    // Jumpskare and moving into level
    void appear()
    {
        StartCoroutine(jumpscareActivate());
        myRigidbody.velocity = new Vector2(0f, 4f);
    }

    void idle()
    {

    }

    void attack()
    {
        StartCoroutine(attackActivate());
    }

    void leave()
    {
        grabCreature = true;
        StartCoroutine(Leaving());
    }

    // Checking if enemy hit player or Torpedo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            grabCreature = true;
        }
    }

    // Checks if player is in range
    private bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    // Checks if a creature is in range
    private bool isCreatureInRange(float range)
    {
        //Need Charging Fish
        if (!grabCreature)
        {
            Vector3 enemyPosition = GameObject.Find("SwordFish").transform.position;
            return Vector3.Distance(transform.position, enemyPosition) <= range;
        }
        else
        {
            return false;
        }
    }

    IEnumerator jumpscareActivate()
    {
        animator.SetBool("Jumpscare", true);
        yield return new WaitForSeconds(0.3f);
        jumpscare = false;
        animationClear();
        yield return new WaitForSeconds(1);
        myRigidbody.velocity = new Vector2(0f, 0f);
    }

    IEnumerator attackActivate()
    {
        if(attacking)
        {
            attacking = false;
            animator.SetBool("Attack", true);
            myRigidbody.velocity = new Vector2(-4f, 0f);
            yield return new WaitForSeconds(0.1f);
            animationClear();
            yield return new WaitForSeconds(0.5f);
            myRigidbody.velocity = new Vector2(2f, 0f);
            yield return new WaitForSeconds(1);
            myRigidbody.velocity = new Vector2(0f, 0f);
            attacking = true;
        }
    }

    IEnumerator Leaving()
    {
        if (leaveTrack)
        {
            leaveTrack = false;
            //attack part
            animator.SetBool("Attack", true);
            myRigidbody.velocity = new Vector2(-4f, 0f);
            yield return new WaitForSeconds(0.1f);
            animationClear();
            yield return new WaitForSeconds(0.5f);
            myRigidbody.velocity = new Vector2(2f, 0f);
            yield return new WaitForSeconds(1);
            myRigidbody.velocity = new Vector2(0f, 0f);


            //leaving part
            animator.SetBool("grabFish", true);
            myRigidbody.velocity = new Vector2(0f, -1f);
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0f);
            animator.SetBool("Attack", false);
            yield return new WaitForSeconds(1);
            myRigidbody.velocity = new Vector2(0f, 0f);
        }
        
    }

    private void animationClear()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("grabFish", false);
        animator.SetBool("Jumpscare", false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class NightingaleMovement : MonoBehaviour
{
    // velocity
    private float direction;
    // direction of vehicle facing
    private Vector2 facing;
    // speed of acceleration
    private float speed = 0.1f;
    // which way object is facing for flip
    private bool isFacingRight;
    // refrence to game object to flip rotation
    public GameObject spotlight;

    // max velocity of object
    [SerializeField] private float maxSpeed;
    // rigid body of object
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        faceMouse();
        direction = Input.GetAxisRaw("Vertical");

        flip();

    }

    // updates at fixed times
    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        Vector2 acceleration = new Vector2(facing.x * direction * speed, facing.y * direction * speed);

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += acceleration;
        }
        if(rb.velocity.magnitude < 0.1)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void flip()
    {
        if ((isFacingRight && facing.x > 0.05) || (!isFacingRight && facing.x < -0.05))
        {
            isFacingRight = !isFacingRight;

            Vector3 subLocaleScale = transform.localScale;
            subLocaleScale.y *= -1;
            transform.localScale = subLocaleScale;

            Vector3 lightRotation = spotlight.transform.eulerAngles;
            lightRotation.z += 180;
            spotlight.transform.eulerAngles = lightRotation;

        }
    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        facing = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        facing.Normalize();
        transform.right = Vector3.Slerp(transform.right, facing, 7.5f * Time.deltaTime);
    }
    public Vector2 getFacing()
    {
        return facing;
    }

    public bool getIsFacingRight()
    {
        return isFacingRight;
    }

    public Vector2 getMovementDirection()
    {
        Vector2 movementDirection = rb.velocity;
        return movementDirection;
    }

    public Vector2 getMovementDirectionNormal()
    {
        Vector2 movementDirection = rb.velocity;
        movementDirection.Normalize();
        return movementDirection;
    }
}

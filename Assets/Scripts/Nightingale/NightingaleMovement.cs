using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightingaleMovement : MonoBehaviour
{

    private float direction;
    private Vector2 facing;
    private float speed = 0.5f;
    private bool isFacingRight;


    [SerializeField] private float maxSpeed;
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

    private void FixedUpdate()
    {
        Vector2 acceleration = new Vector2(facing.x * direction * speed, facing.y * direction * speed);

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += acceleration;
        }
    }

    private void flip()
    {
        if ((isFacingRight && facing.x > 0.05) || (!isFacingRight && facing.x < -0.05))
        {
            isFacingRight = !isFacingRight;
            Vector3 localeScale = transform.localScale;
            localeScale.y *= -1;
            transform.localScale = localeScale;

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
        transform.right = facing;
    }

    Vector2 getFacing()
    {
        return facing;
    }

    bool getIsFacingRight()
    {
        return isFacingRight;
    }

    Vector2 getMovementDirection()
    {
        Vector2 movementDirection = rb.velocity;
        return movementDirection;
    }

    Vector2 getMovementDirectionNormal()
    {
        Vector2 movementDirection = rb.velocity;
        movementDirection.Normalize();
        return movementDirection;
    }
}

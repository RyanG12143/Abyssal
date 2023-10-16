using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class NightingaleMovement : MonoBehaviour
{

    private float direction;
    private Vector2 facing;
    private float speed = 0.1f;
    private bool isFacingRight;
    public GameObject spotLight;


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

            Vector3 subLocaleScale = transform.localScale;
            subLocaleScale.y *= -1;
            transform.localScale = subLocaleScale;

            Vector3 lightLocaleScale = spotLight.transform.localScale;
            lightLocaleScale.x *= -1;
            spotLight.transform.localScale = lightLocaleScale;

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

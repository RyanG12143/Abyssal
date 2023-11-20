using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureLeftRight : MonoBehaviour
{
    private float speed = 2f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // AI movement variables
    private bool movingRight = true;
    public float moveDistance = 5f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the creature has moved the desired distance
        if (Mathf.Abs(transform.position.x - initialPosition.x) >= moveDistance)
        {
            // Change direction
            movingRight = !movingRight;
            initialPosition = transform.position;
        }

        // Flip the creature if needed
        Flip();

        // Set the velocity to move the creature
        rb.velocity = new Vector2((movingRight ? 1f : -1f) * speed, rb.velocity.y);
    }

    private void Flip()
    {
        if ((isFacingRight && rb.velocity.x < 0f) || (!isFacingRight && rb.velocity.x > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

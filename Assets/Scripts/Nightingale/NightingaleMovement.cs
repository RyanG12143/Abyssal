using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightingale : MonoBehaviour
{

    private float direction;
    private Vector2 facing;
    private float speed = 4f;


    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        attachToFaceMouse(FindObjectOfType<FaceMouse>());
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(facing.x * direction * speed, facing.y * direction * speed);
    }

    public void attachToFaceMouse(FaceMouse faceMouse)
    {
        faceMouse.addOnDirectionChanged(updateFacing);
    }

    private void updateFacing(Vector2 newf) 
    {
        facing = newf;
    }
}

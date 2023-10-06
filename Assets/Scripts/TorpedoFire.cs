using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorpedoFire : MonoBehaviour
{
    private float fireSpeed = 4.0f;
    private float leftBound = -10;
    private float rightBound = 10;
    private NightingaleMovement movementScript;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.right * Time.deltaTime * fireSpeed);

        if(transform.position.x < leftBound || transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
        
        
    }
}

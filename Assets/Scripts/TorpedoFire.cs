using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorpedoFire : MonoBehaviour
{
    private float fireSpeed = 4.0f;
    private float fireInput;
    private bool isMoving = false;
    public GameObject player;
    private Vector3 offset = new Vector3(0, -5);

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
        fireInput = Input.GetAxis("Fire3");
        

        if(fireInput > 0f)
        {
           //transform.position = new Vector3(player.transform.x, player.transform.y - 15, player.transform.z);
           isMoving = true;
        }
        if (isMoving)
        {
            transform.Translate(Vector2.right * Time.deltaTime * fireSpeed);
        }
    }
}

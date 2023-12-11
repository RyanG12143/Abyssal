using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity;
        currentVelocity.x = 1;
        currentVelocity.y = 1;
        Vector3 newPos = new Vector3((currentVelocity.x * 0.00f), (currentVelocity.y * 3.60f), -599f);
        transform.position = Vector3.Slerp(transform.position, newPos, Time.deltaTime);
    }
}

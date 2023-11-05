using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentVertical : MonoBehaviour
{
    public float force = 5f;

    /*Matthew Brodbeck 11/01/2023
     * Forces the object in a vertical direction*/
    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
    }
}
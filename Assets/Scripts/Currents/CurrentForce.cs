using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentForce : MonoBehaviour
{
    public float force = 5f;

    /*Matthew Brodbck 11/01/2023
     * Forces the object in a horizontal direction*/
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
        }
        
    }
}

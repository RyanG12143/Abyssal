using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightingaleCurrent : MonoBehaviour
{
    private float currentForce = 10000f;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.StartsWith("Current"))
        {
            transform.position = new Vector2(other.transform.position.x - 0.5f, other.transform.position.y);
        }
    }
    */

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "CurrentRight")
        {
            other.GetComponent<Rigidbody2D>().AddForce(transform.up * currentForce);
        }
    }
}

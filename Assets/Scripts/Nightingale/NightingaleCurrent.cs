using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightingaleCurrent : MonoBehaviour
{
    private float currentForce = 0.05f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "CurrentRight")
        {
            transform.position = new Vector2(transform.position.x + currentForce, transform.position.y);
        }
    }

}

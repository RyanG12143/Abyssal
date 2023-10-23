using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNightingale : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().damage();
        }
    }

}

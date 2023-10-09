using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    void OnTriggerEnter2D(Collision2D other)
    {
        other.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

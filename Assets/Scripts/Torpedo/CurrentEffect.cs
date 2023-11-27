using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CurrentEffect : MonoBehaviour
{
    public float torpedoForce = 0.25f;

    

    /*Matthew Brodbeck 10/27/2023
     * Raises the torpedo when it touches the current*/
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CurrentVertical"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + torpedoForce);
            transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z * 90);
        }
        else
        {
            if (other.gameObject.CompareTag("CurrentDown"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - torpedoForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CurrentVertical"))
        {
            transform.position = other.transform.position;
            transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + 45);
        }
    }

}

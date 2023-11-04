using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrentEffect : MonoBehaviour
{
    private float currentForce = 0.05f;

    /*Matthew Brodbeck 10/27/2023
     * Raises the torpedo when it touches the current*/
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag.StartsWith("Current"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + currentForce);
        }
    }

}

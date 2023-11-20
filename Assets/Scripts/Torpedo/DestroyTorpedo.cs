using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTorpedo : MonoBehaviour
{

    public GameObject hitAnimation;

    /*Matthew Brodbeck 10/13/2023
     * Destroys the torpedo when it collides */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitAnimation, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }

   
}

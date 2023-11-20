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
        Destroy(gameObject);
    }

    private void OnDestroy(){
        Vector3 spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z -2);
        Instantiate(hitAnimation, spawnPos, gameObject.transform.rotation);
    }

   
}

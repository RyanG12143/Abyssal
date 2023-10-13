using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ignore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Falling Rocks") {
            Physics2D.IgnoreCollision(other, GetComponent<Collider2D>(), true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

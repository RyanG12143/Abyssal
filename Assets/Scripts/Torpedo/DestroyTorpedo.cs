using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTorpedo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroys the torpedo if it hits an object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

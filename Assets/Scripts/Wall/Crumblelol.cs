using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumblelol : MonoBehaviour
{
   public GameObject wallBit = new GameObject();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
    }
}

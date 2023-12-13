using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorpedoFire : MonoBehaviour
{
    public float fireSpeed = 5.0f;
    private float horizontalBound;
    private float verticalBound;
    
    private GameObject Nightingale = null;
    private GameObject mainCamera;
    
    
    private Vector2 direction;


    
    //Initializes the torpedo's direction
    void Start()
    {
       if(Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }


        if (mainCamera == null)
        {
            mainCamera = GameObject.Find("Main Camera");
        }

        

        //Has the bounds of the screen be based on the current camera
        horizontalBound = Mathf.Abs(mainCamera.transform.position.x) + 20;
        verticalBound = Mathf.Abs(mainCamera.transform.position.y) + 20;

        direction = new Vector2(1, 0);

        transform.localRotation = Nightingale.transform.rotation;

        
        

    }

    
    void FixedUpdate()
    {
        
        transform.Translate(direction * Time.deltaTime * fireSpeed);

        destroyTorpedo();
        
        
    }

    /*Matthew Brodbeck 10/11/2023
     * Sets the bounds of where the torpedo can go*/
    void destroyTorpedo()
    {
        if (transform.position.x < -horizontalBound || transform.position.x > horizontalBound
            || transform.position.y < -verticalBound || transform.position.y > verticalBound)
        {
            Destroy(gameObject);
        }
    }

  

}

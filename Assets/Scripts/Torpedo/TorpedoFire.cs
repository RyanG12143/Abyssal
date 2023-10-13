using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorpedoFire : MonoBehaviour
{
    private float fireSpeed = 4.0f;
    private float horizontalBound;
    private float verticalBound;
    private GameObject Nightingale = null;
    private GameObject mainCamera;
    
    private Vector2 direction;
    private Vector3 rotation;

    
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
        horizontalBound = Mathf.Abs(mainCamera.transform.position.x) + 10;
        verticalBound = Mathf.Abs(mainCamera.transform.position.y) + 5;


        direction = Nightingale.GetComponent<NightingaleMovement>().getFacing();
        rotation = Nightingale.GetComponent<NightingaleMovement>().getFacing();
        transform.eulerAngles = rotation;
        
        
 

    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the nightingale is initializes and find it if it isn't
        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        if (mainCamera == null)
        {
            mainCamera = GameObject.Find("Main Camera");
        }


        

        transform.Translate(direction * Time.deltaTime * fireSpeed);
        

        //Destroys the torpedo if it goes out of bounds
        if (transform.position.x < -horizontalBound || transform.position.x > horizontalBound
            || transform.position.y < -verticalBound || transform.position.y > verticalBound)
        {
            Destroy(gameObject);
        }

    }

    
}

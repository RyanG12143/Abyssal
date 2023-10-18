using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TorpedoFire : MonoBehaviour
{
    private float fireSpeed = 4.0f;
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
        horizontalBound = Mathf.Abs(mainCamera.transform.position.x) + 10;
        verticalBound = Mathf.Abs(mainCamera.transform.position.y) + 5;


        direction = Nightingale.GetComponent<NightingaleMovement>().getFacing();
        //float angle = Mathf.Atan2(direction.x + 90, direction.y + 90) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(direction * Time.deltaTime * fireSpeed);

        destroyTorpedo();
        
    }

    void destroyTorpedo()
    {
        if (transform.position.x < -horizontalBound || transform.position.x > horizontalBound
            || transform.position.y < -verticalBound || transform.position.y > verticalBound)
        {
            Destroy(gameObject);
        }
    }
}
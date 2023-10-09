using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorpedoFire : MonoBehaviour
{
    private float fireSpeed = 4.0f;
    private float leftBound = -10;
    private float rightBound = 10;
    public float verticalBound = 10;
    private GameObject Nightingale = null;
    private Vector2 direction;

    
    // Start is called before the first frame update
    void Start()
    {
       if(Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

       
        
       direction = Nightingale.GetComponent<NightingaleMovement>().getFacing();
 

    }

    // Update is called once per frame
    void Update()
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        /*if (Nightingale.GetComponent<NightingaleMovement>().getIsFacingRight())
        {
            transform.Translate(Vector2.right * Time.deltaTime * fireSpeed);
        }

        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * fireSpeed);
        }*/

        transform.Translate(direction * Time.deltaTime * fireSpeed);

        if (transform.position.x < leftBound || transform.position.x > rightBound
            || transform.position.y < -verticalBound || transform.position.y > verticalBound)
        {
            Destroy(gameObject);
        }

    }

    
}

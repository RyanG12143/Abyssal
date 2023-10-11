using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject torpedoPrefab;
    public Vector2 torpedoLocation;
    private float countdown = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Places the torpedo on the right side of the sub
        torpedoLocation = new Vector2(transform.position.x + 0.1f, transform.position.y);
        //countdown -= Time.deltaTime;

        //Fires the torpedo if you press left shift
        if (Input.GetKeyDown(KeyCode.LeftShift)) //&& countdown <=0)
        {
            
            Instantiate(torpedoPrefab, torpedoLocation, torpedoPrefab.transform.rotation);
            //countdown = 4;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject torpedoPrefab;
    public Vector2 torpedoLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        torpedoLocation = new Vector2(transform.position.x + 0.1f, transform.position.y);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            Instantiate(torpedoPrefab, torpedoLocation, torpedoPrefab.transform.rotation);
        }
    }
}

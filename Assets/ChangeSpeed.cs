using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    public GameObject Nightingale;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           Nightingale.GetComponent<NightingaleMovement>().changeSpeed(500);
            Nightingale.GetComponent<NightingaleMovement>().changeMaxSpeed(20); 
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorSound : MonoBehaviour
{
    public AudioSource Sound;
    public GameObject Nightingale;
    private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        }

        speed = Nightingale.GetComponent<NightingaleMovement>().getVelocity().magnitude;

 
        
        if((speed > 0.3) && (speed < 5.0))
        {
            Sound.pitch = (speed / 5f) + 0.4f;
            Sound.volume = (speed / 30f) + 0.05f;
        }
        else if (speed < 0.3)
        {
            Sound.pitch = 1;
            Sound.volume = 0;
        }
        else if (speed > 5.0)
        {
            Sound.pitch = 1.4f;
            Sound.volume = (5f / 30f) + 0.05f;
        }


    }

}

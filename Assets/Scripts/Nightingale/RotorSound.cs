using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorSound : MonoBehaviour
{
    public AudioSource Sound;
    public GameObject Nightingale;
    private float speed;
    private bool isSoundPlaying = false;
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
        
        if((speed > 0.3) && !isSoundPlaying)
        {
            StartCoroutine(playSound());
        }

        Debug.Log(speed);

    }

    IEnumerator playSound()
    {
        isSoundPlaying = true;

        Sound.Play();


        for (int i = 0; i < 100; i++)
        {
            Sound.pitch = (speed / 4f) + 0.3f;
            Sound.volume = (speed / 20f) + 0.3f;

            yield return new WaitForSeconds(0.01f);
            if (speed < 0.3)
            {
                break;
            }
        }

        Sound.Stop();

        isSoundPlaying = false;

        yield return null;
    }
}

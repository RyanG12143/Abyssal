using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoHitAnimation : MonoBehaviour
{

    public AudioSource soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().speed = 1.2f;
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Delete()
    {
        soundEffect.Play();
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
}

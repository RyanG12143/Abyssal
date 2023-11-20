using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoHitAnimation : MonoBehaviour
{
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
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }
}

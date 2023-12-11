using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funnyLine : MonoBehaviour
{
    public AudioSource squiddingMe;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playLine());
    }

    private void Update()
    {
        
    }

    IEnumerator playLine()
    {
        yield return new WaitForSeconds(55f);
        squiddingMe.Play();
    }
}

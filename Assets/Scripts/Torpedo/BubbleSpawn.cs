using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject bubbles;
    private bool bubblesSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (!bubblesSpawning)
        {
            StartCoroutine(SpawnBubble());
        }
    }
    IEnumerator SpawnBubble()
    {
        bubblesSpawning = true;
        Instantiate(bubbles, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(0.03f);
        bubblesSpawning = false;

    }
}

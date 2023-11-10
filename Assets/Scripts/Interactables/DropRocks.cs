using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRocks : MonoBehaviour
{
    public GameObject spawner;
    public GameObject container;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") /* && some bool to check player picked up crystal */)
        {
            if (spawner != null && container != null)
            {
                spawner.SetActive(true);
                container.SetActive(true);
            }
        }
    }
}

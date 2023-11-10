using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRocks : MonoBehaviour
{
    public GameObject spawner;
    public GameObject collider;
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
            if (spawner != null && collider != null)
            {
                spawner.SetActive(true);
                collider.SetActive(true);
            }
        }
    }
}

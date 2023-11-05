using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongExplosion : MonoBehaviour
{
    private bool isExploded;
    private GameObject package = null;

    // Start is called before the first frame update
    void Start()
    {
        if(package == null)
        {
            package = GameObject.Find("Explosive package");
        }

        isExploded = false;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void explode()
    {
        isExploded = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Torpedo") && !package.GetComponent<PickUpAble>().isPickedUp())
        {
            other.gameObject.SetActive(false);
            explode();
        }
    }

}

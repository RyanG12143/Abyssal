using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongExplosion : MonoBehaviour
{
    private bool isExploded;
    private GameObject package = null;
    private GameObject strongWall;

    // Start is called before the first frame update
    void Start()
    {
        if (package == null)
        {
            package = GameObject.Find("Explosive package");
        }

        if (strongWall == null)
        {
            
        }


        isExploded = false;
        gameObject.SetActive(true);
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

    public bool checkExplode()
    {
        return isExploded;
    }


}

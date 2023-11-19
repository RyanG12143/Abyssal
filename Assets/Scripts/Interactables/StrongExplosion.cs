using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongExplosion : MonoBehaviour
{
    private bool isExploded;
    private GameObject package = null;
    public GameObject wallToExplode;


    // Start is called before the first frame update
    void Start()
    {
        if (package == null)
        {
            package = GameObject.Find("Explosive package");
        }

        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Animator>().speed = 1f;


        isExploded = false;
        gameObject.SetActive(true);
    }


    IEnumerator explode()
    {
        isExploded = true;
        transform.position = wallToExplode.transform.position;
        transform.localScale = wallToExplode.transform.localScale * 5;
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().Play("Package Explode");
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        yield return new WaitForSeconds(1f);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Torpedo") && wallToExplode.GetComponent<BigWallGone>().isPlaced)
        {
            StartCoroutine(explode());
            Destroy(wallToExplode);
            Destroy(other.gameObject);

        }
    }

    public bool checkExplode()
    {
        return isExploded;
    }


}

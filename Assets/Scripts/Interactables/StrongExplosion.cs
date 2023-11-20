using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongExplosion : MonoBehaviour
{
    private bool isExploded;
    private GameObject package = null;
    
    public GameObject wallToExplode;
    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;
    public GameObject explosion4;
    public GameObject explosion5;
    public GameObject explosion6;
    public GameObject explosion7;
    public GameObject explosion8;
    public GameObject explosion9;
    public GameObject explosion10;


    // Start is called before the first frame update
    void Start()
    {
        if (package == null)
        {
            package = GameObject.Find("Explosive package");
        }

        explosion1.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion2.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion3.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion4.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion5.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion6.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion7.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion8.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion9.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        explosion10.transform.position = new Vector3(transform.position.x, transform.position.y, -2);

        explosion1.GetComponent<Animator>().enabled = false;
        explosion2.GetComponent<Animator>().enabled = false;
        explosion3.GetComponent<Animator>().enabled = false;
        explosion4.GetComponent<Animator>().enabled = false;
        explosion5.GetComponent<Animator>().enabled = false;
        explosion6.GetComponent<Animator>().enabled = false;
        explosion7.GetComponent<Animator>().enabled = false;
        explosion8.GetComponent<Animator>().enabled = false;
        explosion9.GetComponent<Animator>().enabled = false;
        explosion10.GetComponent<Animator>().enabled = false;

        explosion1.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion2.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion3.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion4.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion5.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion6.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion7.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion8.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion9.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        explosion10.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);

        explosion1.SetActive(false);
        explosion2.SetActive(false);
        explosion3.SetActive(false);
        explosion4.SetActive(false);
        explosion5.SetActive(false);
        explosion6.SetActive(false);
        explosion7.SetActive(false);
        explosion8.SetActive(false);
        explosion9.SetActive(false);
        explosion10.SetActive(false);


        isExploded = false;
        gameObject.SetActive(true);
    }


    IEnumerator explode()
    {
        
        isExploded = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        explosion1.SetActive(true);
        explosion2.SetActive(true);
        explosion3.SetActive(true);
        explosion4.SetActive(true);
        explosion5.SetActive(true);
        explosion6.SetActive(true);
        explosion7.SetActive(true);
        explosion8.SetActive(true);
        explosion9.SetActive(true);
        explosion10.SetActive(true);



        explosion1.transform.localScale = wallToExplode.transform.localScale * 2;
        
        explosion2.transform.position = new Vector2(wallToExplode.transform.position.x - 2, transform.position.y);
        explosion2.transform.localScale = wallToExplode.transform.localScale * 2;
        
        explosion3.transform.position = new Vector2(wallToExplode.transform.position.x - 1, transform.position.y);
        explosion3.transform.localScale = wallToExplode.transform.localScale * 2;
        
        explosion4.transform.position = new Vector2(wallToExplode.transform.position.x + 1, transform.position.y);
        explosion4.transform.localScale = wallToExplode.transform.localScale * 2;
        
        explosion5.transform.position = new Vector2(wallToExplode.transform.position.x + 2, transform.position.y);
        explosion5.transform.localScale = wallToExplode.transform.localScale * 2;

        explosion6.transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        explosion6.transform.localScale = wallToExplode.transform.localScale * 2;

        explosion7.transform.position = new Vector2(wallToExplode.transform.position.x - 2, transform.position.y - 1);
        explosion7.transform.localScale = wallToExplode.transform.localScale * 2;

        explosion8.transform.position = new Vector2(wallToExplode.transform.position.x - 1, transform.position.y - 1);
        explosion8.transform.localScale = wallToExplode.transform.localScale * 2;

        explosion9.transform.position = new Vector2(wallToExplode.transform.position.x + 1, transform.position.y - 1);
        explosion9.transform.localScale = wallToExplode.transform.localScale * 2;

        explosion10.transform.position = new Vector2(wallToExplode.transform.position.x + 2, transform.position.y - 1);
        explosion10.transform.localScale = wallToExplode.transform.localScale * 2;

        explosion1.GetComponent<Animator>().enabled = true;
        explosion1.GetComponent<Animator>().Play("Package Explode");

        explosion2.GetComponent<Animator>().enabled = true;
        explosion2.GetComponent<Animator>().Play("Package Explode");

        explosion3.GetComponent<Animator>().enabled = true;
        explosion3.GetComponent<Animator>().Play("Package Explode");

        explosion4.GetComponent<Animator>().enabled = true;
        explosion4.GetComponent<Animator>().Play("Package Explode");

        explosion5.GetComponent<Animator>().enabled = true;
        explosion5.GetComponent<Animator>().Play("Package Explode");

        explosion6.GetComponent<Animator>().enabled = true;
        explosion6.GetComponent<Animator>().Play("Package Explode");

        explosion7.GetComponent<Animator>().enabled = true;
        explosion7.GetComponent<Animator>().Play("Package Explode");

        explosion8.GetComponent<Animator>().enabled = true;
        explosion8.GetComponent<Animator>().Play("Package Explode");

        explosion9.GetComponent<Animator>().enabled = true;
        explosion9.GetComponent<Animator>().Play("Package Explode");

        explosion10.GetComponent<Animator>().enabled = true;
        explosion10.GetComponent<Animator>().Play("Package Explode");

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
        
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

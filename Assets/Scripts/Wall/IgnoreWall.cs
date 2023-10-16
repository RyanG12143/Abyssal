using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IgnoreWall : MonoBehaviour
{
    private bool happened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator wait()
    {

        yield return new WaitForSeconds(1);
        gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(5);
        Destroy(GetComponent<Rigidbody2D>());
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.tag == "Wall" && happened == false) {
           gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            StartCoroutine(wait());
            happened = true;  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

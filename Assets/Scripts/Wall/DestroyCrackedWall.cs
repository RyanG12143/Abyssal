using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrackedWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HiddenArea;
    private List<GameObject> children = new List<GameObject>();

    private void Start(){
        Rigidbody2D[] bodies;
        bodies = GetComponentsInChildren<Rigidbody2D>();
        foreach(Rigidbody2D body in bodies){
            children.Add(body.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Torpedo"))
        {

            foreach(GameObject child in children){
                Destroy(child);
            }

            Destroy(gameObject);
            if (HiddenArea != null)
            {
                Destroy(HiddenArea);
            }
        }
    }
}

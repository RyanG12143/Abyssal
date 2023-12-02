using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrackedWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HiddenArea;
    private List<GameObject> children = new List<GameObject>();

    private void Start(){
        RigidBody[] bodies;
        bodies = GetComponentsInChildren<RigidBody>();
        foreach(RigidBody body in bodies){
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
            Destroy(HiddenArea);
        }
    }
}

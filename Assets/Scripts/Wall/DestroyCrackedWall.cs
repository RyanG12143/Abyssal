using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrackedWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HiddenArea;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Torpedo"))
        {
            Destroy(gameObject);
            Destroy(HiddenArea);
        }
    }
}

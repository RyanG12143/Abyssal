using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGone : MonoBehaviour
{
    public GameObject ByeWall;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Torpedo")
            Destroy(ByeWall);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGone : MonoBehaviour
{
    public GameObject ByeWall;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Torpedo")
            Destroy(ByeWall);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGone : MonoBehaviour
{
    public GameObject ByeWall;

    public AudioSource completeSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Torpedo")
            Destroy(ByeWall);

        if (completeSound != null)
            completeSound.Play();
    }

}

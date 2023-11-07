using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool invinc = false;

    private List<GameObject> crystals = new List<GameObject>();
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!invinc)
            {
                this.gameObject.GetComponent<Health>().damage();
                invinc = true;
                StartCoroutine(i());
            }
        }
    }

    public void addCrystal(GameObject crystal)
    {
        crystals.Add(crystal);
    }

    public List<GameObject> getCrystals()
    {
        return crystals;
    }

    IEnumerator i()
    {

        yield return new WaitForSeconds(1);
        invinc = false;
    }
}

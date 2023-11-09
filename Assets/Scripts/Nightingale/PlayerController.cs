using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool invinc = false;

    private List<GameObject> crystals = new List<GameObject>();
    public GameObject gameOver;

    private void Awake()
    {
        //gameOver = GameObject.Find("Game Over");
        Health.GetInstance().addOnFail(failureState);
    }
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

        yield return new WaitForSeconds(2.5f);
        invinc = false;
    }

    public void failureState()
    {
        if(gameOver != null)
        {
            Time.timeScale = 0;
            gameOver.GetComponent<ShowHide>().Show();
            Destroy(gameObject);
        }
    }
}

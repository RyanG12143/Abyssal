using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool invinc = false;
    private GameObject gameOver;

    private void Awake()
    {
        gameOver = GameObject.Find("Game Over");
        this.gameObject.GetComponent<Health>().addOnFail(failureState);
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

    IEnumerator i()
    {
        yield return new WaitForSeconds(1);
        invinc = false;
    }

    public void failureState()
    {
        if(gameOver != null)
        {
            gameOver.GetComponent<ShowHide>().Show();
        }
    }
}

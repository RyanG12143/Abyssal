using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool invinc = false;
    public Sprite[] spriteList = new Sprite[3];
    public SpriteRenderer spriteRenderer;

    private List<GameObject> crystals = new List<GameObject>();
    public GameObject gameOver;

    private void Start()
    {
        Health.GetInstance().addOnFail(failureState);
        Health.GetInstance().addOnHealthChanged(onHealthChange);
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
            Debug.LogWarning("Timescale = 0");
            Time.timeScale = 0;
            gameOver.GetComponent<ShowHide>().Show();
        }
    }

    public void onHealthChange(int oldValue, int newValue)
    {
        if (newValue > 0)
        {
            spriteRenderer.sprite = spriteList[newValue - 1];
        }
    }
}

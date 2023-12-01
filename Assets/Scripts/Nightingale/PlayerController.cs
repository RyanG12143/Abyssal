using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }

    private bool invinc = false;
    public bool getInvince()
    {
        return invinc;
    }
    public Sprite[] spriteList = new Sprite[3];
    public SpriteRenderer spriteRenderer;

    private List<GameObject> crystals = new List<GameObject>();
    public GameObject gameOver;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Health.GetInstance().addOnFail(failureState);
        Health.GetInstance().addOnHealthChanged(onHealthChange);
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
        if (newValue < oldValue)
        {
            invinc = true;
            StartCoroutine(i());
        }
    }

}

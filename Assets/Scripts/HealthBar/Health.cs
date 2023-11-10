using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance;
    public static Health GetInstance() { return instance; }

    [SerializeField] private int maxHealth;
    private int currHealth;
    public int getCurrHealth() { return currHealth; }

    public AudioSource damageSound;

    public SpriteRenderer SR;

    private bool isDamageFXrunning = false;

    public AudioSource deathSound;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currHealth = maxHealth;
    }

    public void setFullHealth()
    {
        onHealthChanged(currHealth, maxHealth);
        currHealth = maxHealth;
    }

    public void damage()
    {
        onHealthChanged(currHealth , currHealth - 1);
        currHealth = currHealth - 1;

        if (!isDamageFXrunning) {
            StartCoroutine(damageFX());
        }
        if (currHealth < 1)
        {
            //deathSound.Play();
            onFail();
        }
    }

    public void kill()
    {
        onHealthChanged(currHealth, 0); 
        currHealth = 0;
        onFail();
    }

    public int getMax()
    {
        return maxHealth;
    }

    public delegate void ValueChanged(int oldValue, int newValue);
    private ValueChanged onHealthChanged;

    public void addOnHealthChanged(ValueChanged newHealthChanged)
    { 
        onHealthChanged += newHealthChanged;
    }

    IEnumerator damageFX()
    {
        isDamageFXrunning = true;

        damageSound.Play();

        Color current = SR.color;

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < 3; i++)
        {
            SR.color = Color.red;

            yield return new WaitForSeconds(0.25f);

            SR.color = current;

            yield return new WaitForSeconds(0.25f);
        }



        isDamageFXrunning = false;
    }

    public delegate void Failed();
    private Failed onFail;

    public void addOnFail(Failed newFail)
    {
        onFail += newFail;
    }

}

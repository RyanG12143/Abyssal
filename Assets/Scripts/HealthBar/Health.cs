using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currHealth;

    public AudioSource damageSound;

    public SpriteRenderer SR;

    private bool isDamageFXrunning = false;

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
        damageSound.Play();
        onHealthChanged(currHealth , currHealth - 1);
        currHealth = currHealth - 1;

        if (!isDamageFXrunning ) {
            StartCoroutine(damageFX());
        }
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

        Color current = SR.color;

        for (int i = 0; i < 3; i++)
        {
            SR.color = Color.red;

            yield return new WaitForSeconds(0.3f);

            SR.color = current;

            yield return new WaitForSeconds(0.3f);
        }


        isDamageFXrunning = false;
    }

}

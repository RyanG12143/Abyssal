using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    private int currHealth;

    void Start()
    {
        currHealth = maxHealth;
    }

    public void setFullHealth()
    {
        notifyOnHealthChanged(currHealth, maxHealth);
        currHealth = maxHealth;
    }

    public void damage()
    {
        notifyOnHealthChanged(currHealth , currHealth - 1);
        currHealth--;
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

    private void notifyOnHealthChanged(int previousHealth, int newHealth)
    {
        onHealthChanged(previousHealth, newHealth);
    }
}

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
        onHealthChanged(currHealth, maxHealth);
        currHealth = maxHealth;
    }

    public void damage()
    {
        onHealthChanged(currHealth , currHealth - 1);
        currHealth = currHealth - 1;
        if (currHealth < 1)
        {
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

    public delegate void Failed();
    private Failed onFail;

    public void addOnFail(Failed newFail)
    {
        onFail += newFail;
    }

}

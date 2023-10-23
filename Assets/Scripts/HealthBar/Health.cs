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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
        }
    }

    public void setFullHealth()
    {
        currHealth = maxHealth;
    }
    
    public delegate void ValueChanged(int oldValue, int newValue);

    private ValueChanged onHealthChanged;

    public void addOnHealthChanged(ValueChanged newHealthChanged)
    { 
        onHealthChanged += newHealthChanged;
    }

}

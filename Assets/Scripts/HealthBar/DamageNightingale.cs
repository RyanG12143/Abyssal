using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNightingale : MonoBehaviour
{
    public GameObject Healthbar;
    HealthBar health;
    private void OnCollisionEnter2D(Collision2D other)
    {
        health = Healthbar.GetComponent<HealthBar>();
        if (other.gameObject.tag == "DamageOnHit")
        health.LowerHealthImage();
    }

}

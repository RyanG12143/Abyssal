using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNightingale : MonoBehaviour
{
    public GameObject health;
    HealthBar bar;
    private void OnCollisionEnter2D(Collision2D other)
    {
        bar = health.getComponent<HealthBar>();
        if (other.gameObject.tag == "DamageOnHit")
        bar.ChangeHealthImage();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

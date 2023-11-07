using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallOpen : MonoBehaviour
{

    public GameObject nextWall;

    private GameObject Nightingale;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Nightingale == null)
        {
            Nightingale = GameObject.Find("Nightingale");
        };
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            List<GameObject> crystals = collision.gameObject.GetComponent<PlayerController>().getCrystals();
            if(crystals.Count > 0)
            {
                GameObject CrystalToDelete = crystals[0];
                crystals.RemoveAt(0);
                Destroy(CrystalToDelete);
                Destroy(gameObject);
            }
        }
    }

}

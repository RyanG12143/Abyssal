using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallOpen : MonoBehaviour
{

    public GameObject nextWall;

    public GameObject Wall1;
    public GameObject Wall2;
    public GameObject Wall3;

    private bool isDestroyingRunning = false;

    private GameObject Nightingale;

    // Start is called before the first frame update
    void Start()
    {
        Wall1.GetComponent<Animator>().enabled = false;
        Wall2.GetComponent<Animator>().enabled = false;
        Wall3.GetComponent<Animator>().enabled = false;
        Wall1.GetComponent<Animator>().speed = 2f;
        Wall2.GetComponent<Animator>().speed = 2f;
        Wall3.GetComponent<Animator>().speed = 2f;

        
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
                if (!isDestroyingRunning)
                {
                    StartCoroutine(Destroying());
                }
            }
        }
    }

    IEnumerator Destroying()
    {
        float timing = 0.25f;

        isDestroyingRunning = true;

        Wall1.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(timing);
        gameObject.GetComponent<BoxCollider2D>().size -= new Vector2(0, 1.5f);
        gameObject.GetComponent<BoxCollider2D>().offset -= new Vector2(0, 0.75f);
        Wall2.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(timing);
        gameObject.GetComponent<BoxCollider2D>().size -= new Vector2(0, 1.5f);
        gameObject.GetComponent<BoxCollider2D>().offset -= new Vector2(0, 0.75f);
        Destroy(Wall1);
        Wall3.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(timing);
        gameObject.GetComponent<BoxCollider2D>().size -= new Vector2(0, 1.5f);
        gameObject.GetComponent<BoxCollider2D>().offset -= new Vector2(0, 0.75f);
        Destroy(Wall2);
        yield return new WaitForSeconds(timing);
        gameObject.GetComponent<BoxCollider2D>().size -= new Vector2(0, 1.5f);
        gameObject.GetComponent<BoxCollider2D>().offset -= new Vector2(0, 0.75f);
        Destroy(Wall3);
        Destroy(gameObject);

        isDestroyingRunning = false;
    }

}

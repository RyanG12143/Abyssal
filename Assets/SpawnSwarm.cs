using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSwarm : MonoBehaviour
{
    public GameObject HatchetFishSwarm;
    public GameObject rockTrigger;
    public bool collide = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collide && collision.gameObject.CompareTag("Player"))
        {
            Vector2 spawnPos1 = transform.position + new Vector3(-1f, -1f, 0f);
           
            HatchetFishSwarm.SetActive(true);
            rockTrigger.SetActive(true);

            collide = true;
        }
    }
}

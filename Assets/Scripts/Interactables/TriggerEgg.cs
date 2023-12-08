using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerEgg : MonoBehaviour
{
    private float detectionRange = 5f;
    private GameObject player;
    private Amalgamation amalgamation;
    public GameObject spawnParasite;
    public GameObject parasiteCluster;
    public GameObject eggLight;

    // Animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        amalgamation = GameObject.FindGameObjectWithTag("Boss").GetComponent<Amalgamation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Torpedo")
        {
            animator.SetBool("eggBroken", true);
            spawnParasite.SetActive(true);
            parasiteCluster.SetActive(true);
            eggLight.SetActive(false);
            amalgamation.StartChase();
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
}

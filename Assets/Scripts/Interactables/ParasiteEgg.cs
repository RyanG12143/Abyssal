using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteEgg : MonoBehaviour
{
    private float detectionRange = 7f;
    private bool hatched = false;
    private GameObject player;
    public GameObject parasite;

    // Animator
    public Animator animator;

    //Audio
    public AudioSource eggCrack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInRange(detectionRange) && !hatched)
        {
            eggCrack.Play();
            animator.SetBool("hatched", true);
            Vector3 modify = new Vector3(0f, 1f, 0f);
            Instantiate(parasite, transform.position + modify, parasite.transform.rotation);
            hatched = true;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
}

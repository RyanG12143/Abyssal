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

    public GameObject deleteSong;
    public GameObject chaseSong;

    // Animator
    public Animator animator;

    //Audio
    public AudioSource eggCrack;
    public AudioSource parasiteScreech;

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
            eggCrack.Play();
            StartCoroutine(screechTimer(1f));
            StartCoroutine(songChange());   
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

    IEnumerator screechTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        parasiteScreech.Play();

    IEnumerator songChange()
    {
        for (int i = 0; i < 55; i++)
        {
            deleteSong.GetComponent<AudioSource>().volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(deleteSong.gameObject);

        yield return new WaitForSeconds(1f);

        Instantiate(chaseSong);

        yield return null;
    }
}

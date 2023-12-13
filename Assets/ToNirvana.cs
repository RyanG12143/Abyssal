using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ToNirvana : MonoBehaviour
{
    public float maxIntesnity;
    public Light2D globalLight;
    public GameObject player;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = (gameObject.transform.position - player.transform.position).magnitude;
        globalLight.intensity = maxIntesnity / dist;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}

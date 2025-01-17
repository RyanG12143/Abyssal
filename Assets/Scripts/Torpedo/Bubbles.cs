using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().speed = Random.Range(0.8f, 1.6f);
        transform.position = new Vector3(transform.position.x + Random.Range(-0.1f, 0.1f), transform.position.y + Random.Range(-0.1f, 0.1f), transform.position.z);
        float scaleRandom = Random.Range(0.3f, 0.8f);
        transform.localScale = new Vector3(scaleRandom, scaleRandom, scaleRandom);
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);

            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
}

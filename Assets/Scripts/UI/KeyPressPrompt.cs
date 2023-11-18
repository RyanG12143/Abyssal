using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressPrompt : MonoBehaviour
{
    // Start is called before the first frame update


    public KeyCode KeyCode1;
    public KeyCode KeyCode2;

    private bool inRange = false;

    private bool isFadeOutRunning = false;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange && !isFadeOutRunning)
        {
            if(Input.GetKeyDown(KeyCode1) || Input.GetKeyDown(KeyCode2)) {
            
                StartCoroutine(FadeOut());

            }

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            inRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            inRange = false;

        }
    }

    IEnumerator FadeOut()
    {
        isFadeOutRunning = true;

        Color current = new Color((gameObject.GetComponent<SpriteRenderer>().color.r), (gameObject.GetComponent<SpriteRenderer>().color.g), (gameObject.GetComponent<SpriteRenderer>().color.b), 1f);

        Color end = new Color((gameObject.GetComponent<SpriteRenderer>().color.r), (gameObject.GetComponent<SpriteRenderer>().color.g), (gameObject.GetComponent<SpriteRenderer>().color.b), 0f);

        for (int i = 0; i < 180; i++)
        {

            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(current, end, i / 180.0f);

            yield return new WaitForSeconds(.03f);

        }

        Destroy(gameObject);
    }




}

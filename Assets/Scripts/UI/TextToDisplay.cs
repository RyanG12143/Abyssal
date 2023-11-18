using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToDisplay : MonoBehaviour
{
    public string[] textToDisplay;
    public float timeToDisplay;
    public GameObject prevEvent;
    private bool triggered = false;


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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                triggered = true;
                EventHandler.getInstance().displayText(textToDisplay, timeToDisplay);
                if (prevEvent != null)
                {
                    Destroy(prevEvent);
                }
            }
        }
    }
}

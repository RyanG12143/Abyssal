using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndText : MonoBehaviour
{
    public string[] textToDisplay;
    public float timeToDisplay;
    public GameObject prevEvent;
    private bool triggered = false;



    // Start is called before the first frame update
    void Start()
    {
        triggered = true;
        EventHandler.getInstance().displayText(textToDisplay, timeToDisplay);
    }

    // Update is called once per frame
    void Update()
    {

    }

  


}

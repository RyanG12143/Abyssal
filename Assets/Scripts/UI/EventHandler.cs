using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private static EventHandler instance;
    public static EventHandler getInstance() { return instance; }

    public GameObject uiText;
    private bool TextOnDisplay;
    private Queue<string> textQueue = new Queue<string>();
    private float timeOnScreen;
    private float timeToDisplay;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if (TextOnDisplay)
        {
            if (timeOnScreen > timeToDisplay)
            {
                if (textQueue.Count > 0)
                {
                    uiText.GetComponent<TextMeshProUGUI>().SetText(textQueue.Dequeue());
                    timeOnScreen = 0;
                }
                else
                {
                    uiText.SetActive(false);
                    TextOnDisplay = false;
                }
            }
            timeOnScreen += Time.deltaTime;
        }
    }

    public void displayText(string[] text, float timeToDisplay)
    {
        if (TextOnDisplay)
        {
            textQueue.Clear();
        }

        timeOnScreen = 0;
        foreach (string s in text)
        {
            textQueue.Enqueue(s);
        }
        timeOnScreen = 0;
        uiText.GetComponent<TextMeshProUGUI>().SetText(textQueue.Dequeue());
        uiText.SetActive(true);
        this.timeToDisplay = timeToDisplay;
        TextOnDisplay = true;
        
    }
}

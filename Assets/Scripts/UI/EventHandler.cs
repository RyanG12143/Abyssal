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
    private Queue<string[]> textQueue = new Queue<string[]>();
    private Queue<float> timeQueue = new Queue<float>();

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
        if (textQueue.Count > 0 && !TextOnDisplay) 
        { 
            displayText(textQueue.Dequeue(), timeQueue.Dequeue());
        }
    }

    private IEnumerator timeTillFade(string[] text, float timeOnScreen)
    {
        
        foreach (string s in text)
        {
            uiText.GetComponent<TextMeshProUGUI>().SetText(s);
            uiText.SetActive(true);
            yield return new WaitForSeconds(timeOnScreen);
        }
        uiText.SetActive(false);
        TextOnDisplay = false;
    }

    public void displayText(string[] text, float timeOnScreen)
    {
        if (TextOnDisplay)
        {
            textQueue.Enqueue(text);
            timeQueue.Enqueue(timeOnScreen);
            return;
        }
        TextOnDisplay = true;
        StartCoroutine(timeTillFade(text, timeOnScreen));
    }
}

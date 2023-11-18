using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private static EventHandler instance;
    public static EventHandler getInstance() { return instance; }

    public GameObject uiText;

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
    }

    public void displayText(string[] text, float timeOnScreen)
    {
        StartCoroutine(timeTillFade(text, timeOnScreen));
    }
}

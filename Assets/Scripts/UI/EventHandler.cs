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
        yield return new WaitWhile(IsTextOnDispaly);

        TextOnDisplay = true;
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
        StartCoroutine(timeTillFade(text, timeOnScreen));
    }

    public bool IsTextOnDispaly()
    {
        return TextOnDisplay;
    }
}

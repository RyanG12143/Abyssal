using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public GameObject uiText;
    // Start is called before the first frame update
    void Start()
    {
        displayText("There was a mission before us, we should go deeper to try and find them, they may have left behind a trail. We can use our sonar to detect beacons(Space)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator timeTillFade()
    {
        yield return new WaitForSeconds(10);
        uiText.SetActive(false);
    }

    public void displayText(string text)
    {
        uiText.GetComponent<TextMeshProUGUI>().SetText(text);
        uiText.SetActive(true);
        StartCoroutine(timeTillFade());
    }
}

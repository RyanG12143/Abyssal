using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public float fadeSpeed;
    public GameObject blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeFromBlack()
    {
        blackScreen.SetActive(true);

        Color prevObjectColor = Color.black;
        prevObjectColor = new Color(prevObjectColor.r, prevObjectColor.g, prevObjectColor.b, 1);
        Color objectColor = Color.black;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        float fadeAmount = 0;

        while (blackScreen.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = fadeAmount + (fadeSpeed / 4 * Time.deltaTime);
            blackScreen.GetComponent<Image>().color = Color.Lerp(prevObjectColor, objectColor, fadeAmount);
            yield return null;
        }

        blackScreen.SetActive(false);
        yield return null;
    }
}

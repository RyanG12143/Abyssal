using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public float fadeSpeed;
    public int nextLevel;
    public GameObject blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Enter");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("Player Enter");

            StartCoroutine(FadeToBlack());
        }
    }

    public IEnumerator FadeToBlack()
    {
        blackScreen.SetActive(true);

        Color prevObjectColor = blackScreen.GetComponent<Image>().color;
        Color objectColor = blackScreen.GetComponent<Image>().color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        float fadeAmount = 0;

        while (blackScreen.GetComponent<Image>().color.a < .2)
        {
            fadeAmount = fadeAmount + (fadeSpeed / 4 * Time.deltaTime);
            blackScreen.GetComponent<Image>().color = Color.Lerp(prevObjectColor, objectColor, fadeAmount);
            yield return null;
        }
        while (blackScreen.GetComponent<Image>().color.a < .5)
        {
            fadeAmount = fadeAmount + (fadeSpeed / 2 * Time.deltaTime);
            blackScreen.GetComponent<Image>().color = Color.Lerp(prevObjectColor, objectColor, fadeAmount);
            yield return null;
        }
        while (blackScreen.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = fadeAmount + (fadeSpeed * Time.deltaTime);
            blackScreen.GetComponent<Image>().color = Color.Lerp(prevObjectColor, objectColor, fadeAmount);
            yield return null;
        }
        SceneManager.LoadScene(nextLevel);
        yield return null;
    }
}

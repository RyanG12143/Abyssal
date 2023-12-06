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

        Color objectColor = blackScreen.GetComponent<Image>().color;
        float fadeAmount;

        while (blackScreen.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = objectColor;
            yield return null;
        }
        SceneManager.LoadScene(nextLevel);
        yield return null;
    }
}

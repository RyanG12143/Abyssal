using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public static Oxygen instance;

    public Image bar;
    public Image mask;
    public Image container;
    private float originalSize;
    private float timeLimit = 30;
    private float timeLeft;

    public static Oxygen GetInstance() { return instance; }

    // Start is called before the first frame update
    void Awake()
    { 
        instance = this;

        originalSize = mask.rectTransform.rect.height;

        timeLeft = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.GetComponent<Image>().IsActive())
        {
            timeLeft -= Time.deltaTime;
            SetValue(timeLeft / timeLimit);
        }
        if (timeLeft <= 0 && Health.GetInstance().getCurrHealth() > 0)
        {
            // implement fail state
            Health.GetInstance().kill();
        }
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
            originalSize * value);
    }

    public void activateOxygen()
    {
        if (bar.enabled == false)
        {
            timeLeft = timeLimit;
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize);
        }

        bar.enabled = true;
        container.enabled = true;
    }

    public void deactivateOxygen()
    {
        bar.enabled = false;
        container.enabled = false;
    }
}

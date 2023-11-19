using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image bar;
    public Image mask;
    public Image container;
    private float originalSize;

    //private int count = 1;

    private float currHealth;
    private float maxHealth;
    // Start is called before the first frame update

    void Awake()
    {
        originalSize = mask.rectTransform.rect.yMax;
    }

    void Start()
    {
        Health.GetInstance().addOnHealthChanged(updateHealthBar);
        currHealth = Health.GetInstance().getMax();
        maxHealth = Health.GetInstance().getMax();

    }

    private void updateHealthBar(int oldVal, int newVal)
    {
        currHealth = newVal;
        if (currHealth >= 0)
        {
            Debug.Log(currHealth);
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * (currHealth/maxHealth));
        }
    }

}

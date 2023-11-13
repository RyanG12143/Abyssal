using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    //private bool invinc;
    public Sprite[] spriteList = new Sprite[4];
    //private int count = 1;

    private int currHealth;
    // Start is called before the first frame update
    void Start()
    {
        //state1 = this.gameObject.GetComponent<SpriteRenderer>();
        //LowerHealthImage();
        Health.GetInstance().addOnHealthChanged(updateHealthBar);
        currHealth = Health.GetInstance().getMax();
    }

    private void updateHealthBar(int oldVal, int newVal)
    {
        currHealth = newVal;
        if (currHealth >= 0)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = spriteList[currHealth];
        }
    }

}

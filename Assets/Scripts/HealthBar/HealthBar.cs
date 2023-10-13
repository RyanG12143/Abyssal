using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Sprite[] spriteList = new Sprite[4];
    private int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        //state1 = this.gameObject.GetComponent<SpriteRenderer>();
        //ChangeHealthImage();
    }
    public void ChangeHealthImage()
    {
        if (count > 2)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = spriteList[3];
            //call the fail state here
            return;
        }
            
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = spriteList[count];
        count++;
    }
    /* private void notifyOnHealthChanged(int previousHealth, int newHealth)
     {
         onHealthChanged
     }
     public void attachToPlayer(PlayerPrefs player)
     {
         player.addOnHealthChanged(updateHealthBar);
     }
     private void updateHealthBar(int oldV, int newV)
     {
         //do stufff to the health bar
     }*/
    // Update is called once per frame
    void Update()
    {

    }
}

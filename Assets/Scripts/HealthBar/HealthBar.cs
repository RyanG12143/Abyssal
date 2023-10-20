using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private bool invinc;
    public Sprite[] spriteList = new Sprite[4];
    private int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        //state1 = this.gameObject.GetComponent<SpriteRenderer>();
        //LowerHealthImage();
    }
    IEnumerator i()
    {

        yield return new WaitForSeconds(1);
        invinc = false;
    }
    public void LowerHealthImage()
    {
        if (!invinc) {
            invinc = true;
            StartCoroutine(i());
         if (count > 2)
                {
                    gameObject.GetComponent<UnityEngine.UI.Image>().sprite = spriteList[3];
                    //call the fail state here
                    return;
               }

                    gameObject.GetComponent<UnityEngine.UI.Image>().sprite = spriteList[count];
                    count++;
        }

       
            
        
    }
    public void RaiseHealthImage()
    {
        count--;
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = spriteList[count];
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

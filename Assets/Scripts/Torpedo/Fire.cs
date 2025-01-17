using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fire : MonoBehaviour
{
    
    private Vector2 torpedoLocation;
    private bool isCooldownActive = false;
    public GameObject torpedoPrefab;
    public AudioSource fireSound;
    public GameObject readyLight;

    void Start()
    {
        readyLight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Matthew Brodbeck 10/13/2023
        //Places the torpedo on the right side of the sub
        torpedoLocation = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.4f);
        
        


        //Matthew Brodbeck 10/15/2023
        //Fires the torpedo if you press left shift and the cooldown is over
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.LeftShift)) && !isCooldownActive)
        {
            fireSound.Play();
            Instantiate(torpedoPrefab, torpedoLocation, torpedoPrefab.transform.rotation);
            StartCoroutine(torpedoCooldown());
            
        }


    }

    /*Matthew Brodbeck 10/15/2023
     * Sets the cooldown for the torpedo*/
    IEnumerator torpedoCooldown()
    {
        readyLight.SetActive(false);
        isCooldownActive = true;
        yield return new WaitForSeconds(2f);
        isCooldownActive = false;
        readyLight.SetActive(true);
        
    }

    private void FixedUpdate()
    {
        readyLight.transform.localPosition = new Vector2(0, -0.25f);
        
    }

}

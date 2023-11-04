using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject torpedoPrefab;
    private Vector2 torpedoLocation;
    public bool isCooldownActive = false;

    

    // Update is called once per frame
    void Update()
    {
        //Matthew Brodbeck 10/13/2023
        //Places the torpedo on the right side of the sub
        torpedoLocation = new Vector2(transform.position.x + 0.1f, transform.position.y);
        

        //Matthew Brodbeck 10/15/2023
        //Fires the torpedo if you press left shift and the cooldown is over
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCooldownActive)
        {
            
            Instantiate(torpedoPrefab, torpedoLocation, torpedoPrefab.transform.rotation);
            StartCoroutine(torpedoCooldown());
            
        }
    }

    /*Matthew Brodbeck 10/15/2023
     * Sets the cooldown for the torpedo*/
    IEnumerator torpedoCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(2);
        isCooldownActive = false;
        
    }
}

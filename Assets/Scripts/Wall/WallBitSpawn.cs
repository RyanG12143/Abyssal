using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBitSpawn : MonoBehaviour
{

    public GameObject wallBit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void OnDestroy(){
        if (!this.gameObject.scene.isLoaded) return;
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(wallBit, gameObject.transform.position, gameObject.transform.rotation);
    }

}

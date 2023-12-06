using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSize : MonoBehaviour
{
    public Camera Camera;
    public GameObject hidden;
    public GameObject Nightingale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("test");
            Destroy(gameObject);
            Destroy(hidden);
            Nightingale.GetComponent<NightingaleMovement>().changeSpeed(500);
            Nightingale.GetComponent<NightingaleMovement>().changeMaxSpeed(20);
            Camera.GetComponent<CameraController>().InverseCamera();
        }
            
    }
}

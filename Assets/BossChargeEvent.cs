using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeEvent : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject TargetPosition;
    public GameObject nightingale;
    public int speed = 2;
    bool camera_move_enabled = false;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MainCamera.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MainCamera.GetComponent<CameraController>().changeCamera();
        }
       /* if (other.gameObject.tag == "Player")
        {
            MainCamera.GetComponent<Camera>().enabled = false;
            SecondCamera.enabled = true;
            
                //{
                    MainCamera.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z -10),ref velocity, speed * Time.deltaTime);
            //MainCamera.transform.rotation = Quaternion.Lerp(transform.rotation, TargetPosition.transform.rotation, speed * Time.deltaTime);
            MainCamera.GetComponent<CameraController>().setCameraSize(24);
                //}
        }*/
            
    }
}

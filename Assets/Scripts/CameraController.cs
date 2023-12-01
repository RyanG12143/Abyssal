using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Speed of the camera (would not recommend messing with this)
    private float FollowSpeed = 5f;

    // The transform of the Nightingale
    private Transform target;

    // Nightingale
    private GameObject Nightingale = null;

    // Current Nightingale Velocity
    private Vector2 currentVelocity;

    static public float defaultCameraSize = 4.703125f;

    private float cameraSize;

    private bool isCameraChanging = false;

    private bool switchCamera = false;
    public GameObject TargetPosition;



    private void Start()
    {
        cameraSize = defaultCameraSize;

        gameObject.GetComponent<Camera>().orthographicSize = cameraSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraSizeChange(24);
        if (!switchCamera)
        {
            if (Nightingale == null)
            {
                Nightingale = GameObject.Find("Nightingale");
            }

            target = Nightingale.transform;

            currentVelocity = Nightingale.GetComponent<NightingaleMovement>().getVelocity();

            // These two lines are what effect camera movement
            Vector3 newPos = new Vector3(target.position.x + (currentVelocity.x * 1.20f), target.position.y + (currentVelocity.y * 0.60f), -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
        else
        {
            if (Nightingale == null)
            {
                Nightingale = GameObject.Find("Nightingale");
            }
            //Vector3 middle = new Vector3(Nightingale.transform.position.x - TargetPosition.position.x, Nightingale.transform.position.y - TargetPosition.position.y, Nightingale.transform.position.z);
            target = TargetPosition.transform;

            currentVelocity = Nightingale.GetComponent<NightingaleMovement>().getVelocity();

            // These two lines are what effect camera movement
            Vector3 newPos = new Vector3(target.position.x + (currentVelocity.x * 1.20f), target.position.y + (currentVelocity.y * 0.30f), -90f);
            transform.position = Vector3.Lerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
            
        }

    }
    public void changeCamera() 
    {
        Debug.Log("test");
        switchCamera = true;
            //{
            //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), ref currentVelocity, speed * Time.deltaTime);
            //MainCamera.transform.rotation = Quaternion.Lerp(transform.rotation, TargetPosition.transform.rotation, speed * Time.deltaTime);
            //setCameraSize(24);
            //}

    }

    public float getCameraSize()
    {
        return cameraSize;
    }

    public void setCameraSize(float newSize)
    {
        if (!isCameraChanging)
        {
            StartCoroutine(cameraSizeChange(newSize));
        }
    }

    public void resetCameraSize()
    {
        if (!isCameraChanging)
        {
            StartCoroutine(cameraSizeChange(defaultCameraSize));
        }
    }

    IEnumerator cameraSizeChange(float newSize)
    {
        isCameraChanging = true;


        if (newSize > cameraSize)
        {
            for (int i = 1; i < 102; i++)
            {
                gameObject.GetComponent<Camera>().orthographicSize = (cameraSize + ((newSize - cameraSize) * i / 100));

                yield return new WaitForSeconds(0.01f);
            }
        }
        else if(newSize < cameraSize)
        {
            for (int i = 1; i < 102; i++)
            {
                gameObject.GetComponent<Camera>().orthographicSize = (cameraSize - ((cameraSize - newSize) * i / 100));
                
                yield return new WaitForSeconds(0.01f);
            }
        }



        cameraSize = newSize;

        isCameraChanging = false;

        yield return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Speed of the camera (would not recommend messing with this)
    private float FollowSpeed = 10f;

    // The transform of the Nightingale
    private Transform target;

    // Nightingale
    private GameObject Nightingale = null;

    // Current Nightingale Velocity
    private Vector2 currentVelocity;

    static private float defaultCameraSize = 7;

    private float cameraSize;

    private bool isCameraChanging = false;


    private void Start()
    {
        cameraSize = defaultCameraSize;

        gameObject.GetComponent<Camera>().orthographicSize = cameraSize;
    }

    // Update is called once per frame
    void FixedUpdate()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeCameraSize : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        //camera.GetComponent<PixelPerfectCamera>().SetAssetsPixelsPerUnit(24);
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
        }
            
    }
}

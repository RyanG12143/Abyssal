using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSize : MonoBehaviour
{
    public Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera.GetComponent<PixelPerfectCamera>();
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

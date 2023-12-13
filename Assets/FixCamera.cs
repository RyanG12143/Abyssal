using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject BossBlocker;

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
            BossBlocker.SetActive(true);
            Destroy(gameObject);
            MainCamera.GetComponent<CameraController>().changeCamera();
        }

    }
}

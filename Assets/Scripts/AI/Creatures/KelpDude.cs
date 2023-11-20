using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.Rendering.ReloadAttribute;

public class KelpDude : MonoBehaviour
{

    private bool hide = false;
    private Vector3 startingPos;
    private Vector3 hidePos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = gameObject.transform.position;
        hidePos = new Vector3(startingPos.x + 2f,startingPos.y, startingPos.z);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hide)
        {
            gameObject.transform.position = Vector3.Slerp(startingPos, hidePos, 2.5f * Time.deltaTime);
        }
        else
        {
            gameObject.transform.position = Vector3.Slerp(hidePos, startingPos, 2.5f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hide = false;
        }
    }


}

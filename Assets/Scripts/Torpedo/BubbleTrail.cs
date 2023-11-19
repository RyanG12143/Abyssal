using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrail : MonoBehaviour
{

    [SerializeField] private GameObject torpedoPrefab;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = torpedoPrefab.transform.position;

        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction * Time.deltaTime * torpedoPrefab.GetComponent<TorpedoFire>().fireSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HELL : MonoBehaviour
{
    private Image imageComponent;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
    }
    void Update()
    {
        imageComponent.color = Random.value < 0.5f ? Color.black : Color.white;
    }
}

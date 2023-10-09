using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowHide : MonoBehaviour
{
    [Header("POOP!")]
    public GameObject targetObject;
    public float speed = 0.5f;

    public UnityEvent onShow;
    
    public void Show()
    {
        targetObject.SetActive(true);
        onShow.Invoke();
    }

    public void Hide()
    {
        targetObject.SetActive(false);
    }
}

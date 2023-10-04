using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour
{
    public Vector2 direction;
    // Update is called once per frame
    void Update()
    {
        faceMouse();
    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        direction.Normalize();
        transform.right = direction;
        notifyOnDirectionChanged(direction);
    }

    public delegate void ValueChanged(Vector2 newValue);
    private ValueChanged onDirectionChanged;

    public void addOnDirectionChanged(ValueChanged newDirectionChanged)
    {
        onDirectionChanged += newDirectionChanged;
    }

    private void notifyOnDirectionChanged(Vector2 newDirection) 
    { 
        onDirectionChanged(newDirection);
    }
}

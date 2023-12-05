using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // Import this namespace for Light2D
using UnityEngine.Rendering.Universal;

public class LightFader : MonoBehaviour
{
    public float fadeSpeed = 1.0f; // Adjust this to control the speed of the fade
    private Light2D myLight;
    private float targetIntensity;

    void Start()
    {
        myLight = GetComponent<Light2D>();
        targetIntensity = myLight.intensity;
    }

    void Update()
    {
        // Fading in and out using Mathf.PingPong
        float lerpValue = Mathf.PingPong(Time.time * fadeSpeed, 1.0f);
        myLight.intensity = Mathf.Lerp(0, targetIntensity, lerpValue);
    }
}
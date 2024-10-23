using UnityEngine;
using System.Collections;

public class Speedometer : MonoBehaviour
{
    public GameObject needle;
    private float startPosition = 220f, endPosition = -42f;
    private float desiredPosition;
    public VelocityTools velocityTools;
    public float vehicleSpeed;

    // Ze glitch effect parameters
    public float glitchDuration = 0.1f; 
    public float glitchInterval = 5f;   
    public float maxDisplacement = 10f;  
    public float maxScale = 1.2f;        

    private Vector3 originalPosition;  
    private Vector3 originalScale;     

    void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;

        StartCoroutine(GlitchRoutine());
    }

    void Update()
    {
        // Get the current speed from VelocityTools and update the needle
        vehicleSpeed = velocityTools.GetForwardVelocity().magnitude * 3.6f;  // Convert m/s to km/h
        vehicleSpeed = Mathf.Clamp(vehicleSpeed, 0, 55);  // Clamp to 55 max speed
        UpdateNeedle();
    }

    public void UpdateNeedle()
    {
        desiredPosition = startPosition - endPosition;

        // Normalize the speed to the 0-1 range for needle rotation
        float temp = vehicleSpeed / 55f;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPosition));
    }

    IEnumerator GlitchRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(glitchInterval);

            // Start the glitch effect
            StartCoroutine(Glitch());

            yield return new WaitForSeconds(glitchDuration);
        }
    }

    IEnumerator Glitch()
    {
        float randomX = Random.Range(-maxDisplacement, maxDisplacement);
        float randomY = Random.Range(-maxDisplacement, maxDisplacement);
        transform.localPosition = originalPosition + new Vector3(randomX, randomY, 0);

        float randomScale = Random.Range(1f, maxScale);
        transform.localScale = originalScale * randomScale;

        yield return new WaitForSeconds(glitchDuration);

        transform.localPosition = originalPosition;
        transform.localScale = originalScale;
    }
}

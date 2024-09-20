using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChildEffect : MonoBehaviour
{
    public static event Action OnSlowMotion;
    public static event Action OnChildHit;

    [SerializeField] float slowDownFactor = 0.2f;
    [SerializeField] float slowDownTime = 2f;

    private void Update()
    {
        // Revert time back to normal after the slow down.
        Time.timeScale += (1f / slowDownTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime += (1f / slowDownTime) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime = Mathf.Clamp(0.02f, 0f, 1f);
        //Debug.Log(string.Format("TimeScale: {0} DeltaTime: {1}", Time.timeScale, Time.fixedDeltaTime));

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Slow motion activated");
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.time * 0.02f;
            if(OnSlowMotion != null) { OnSlowMotion(); }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Child hit");
            if (OnChildHit != null) { OnChildHit(); }
        }
    }
}

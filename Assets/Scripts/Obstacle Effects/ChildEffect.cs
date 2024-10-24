using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChildEffect : MonoBehaviour
{
    public static event Action OnSlowMotion;

    // int is used as the scene number that has to be loaded.
    public static event Action OnChildHit;

    [SerializeField] float slowDownFactor = 0.2f;
    [SerializeField] float slowDownTime = 2f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        AudioClip tireSkidSound = Resources.Load<AudioClip>("tire skid");

        if (tireSkidSound != null)
        {
            audioSource.clip = tireSkidSound;
        }
    }

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
        if (other.gameObject.CompareTag("Player"))
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            //Flip the visibility of the models within the child prefab.
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                // Reverses the state of the child game object.
                child.SetActive(!child.activeSelf);
                //Debug.Log(string.Format("Setting child {0} to state: {1}", i, child.activeSelf));
            }

            Debug.Log("Slow motion activated");
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.time * 0.02f;
            if (OnSlowMotion != null) { OnSlowMotion(); }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Incase slow motion isn't back to normal when the player collides with the child, it is set to normal here.
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;

            Debug.Log("Child hit");
            if (OnChildHit != null) { OnChildHit(); }
        }
    }
}

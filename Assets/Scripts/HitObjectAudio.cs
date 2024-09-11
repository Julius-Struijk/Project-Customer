using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectAudio : MonoBehaviour
{
    AudioSource audioPlayer;
    VelocityTools velocityTools;

    [SerializeField] AudioClip[] hitAudio;
    [SerializeField] float pitchRange;
    [SerializeField] float minimumHitSpeed;


    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        velocityTools = GetComponent<VelocityTools>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If an interactable object has been collided with above a certain speed then the sound is played.
        if(collision.collider.CompareTag("InteractableObject") && (velocityTools.GetForwardVelocity().z > minimumHitSpeed || velocityTools.GetForwardVelocity().z < -minimumHitSpeed))
        {
            if (audioPlayer != null && hitAudio != null && hitAudio.Length > 0)
            {
                AudioClip sample = hitAudio[Random.Range(0, hitAudio.Length)];
                audioPlayer.pitch = 1 + (Random.value * 2 - 1) * pitchRange;
                audioPlayer.PlayOneShot(sample);

                Debug.Log("Hit object at speed: " + velocityTools.GetForwardVelocity().z);
            }
        }
    }
}

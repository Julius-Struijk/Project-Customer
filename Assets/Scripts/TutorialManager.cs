using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public Image tutorialImage;
    public float fadeDuration = 1f;
    private bool tutorialActive = true;
    private bool canSkip = false;
    public float minDisplayTime = 2f;

    public GameObject player; // Reference to the player object
    public MonoBehaviour movementScript; // Reference to the player's movement script
    public MonoBehaviour cameraScript; // Reference to the camera look script

    void Start()
    {
        // Disable player controls (movement and camera) while the tutorial is active
        DisablePlayerControls();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(FadeImageIn());
    }

    void Update()
    {
        if (tutorialActive && canSkip && Input.anyKey)
        {
            StartCoroutine(FadeImageOut());
        }
    }

    IEnumerator FadeImageIn()
    {
        tutorialImage.gameObject.SetActive(true);
        Color color = tutorialImage.color;
        color.a = 0f;
        tutorialImage.color = color;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            tutorialImage.color = color;
            yield return null;
        }

        color.a = 1f;
        tutorialImage.color = color;

        yield return new WaitForSeconds(minDisplayTime);
        canSkip = true;
    }

    IEnumerator FadeImageOut()
    {
        tutorialActive = false;
        Color color = tutorialImage.color;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            tutorialImage.color = color;
            yield return null;
        }

        color.a = 0f;
        tutorialImage.color = color;
        tutorialImage.gameObject.SetActive(false);
        Destroy(tutorialImage.gameObject);

        // Lock the cursor and hide it again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable player controls after the tutorial is over
        EnablePlayerControls();
    }

    // Disables the player's movement and camera control
    void DisablePlayerControls()
    {
        if (movementScript != null) movementScript.enabled = false;
        if (cameraScript != null) cameraScript.enabled = false;
    }

    // Enables the player's movement and camera control
    void EnablePlayerControls()
    {
        if (movementScript != null) movementScript.enabled = true;
        if (cameraScript != null) cameraScript.enabled = true;
    }
}

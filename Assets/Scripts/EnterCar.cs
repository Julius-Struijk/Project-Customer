using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnterCar : MonoBehaviour
{
    public static event Action<int> OnEnterCar;
    [SerializeField] private AudioClip EnterCarSound;
    [SerializeField] private Image fadeImage; // Reference to the UI Image used for fading.

    private void OnCollisionEnter(Collision collision)
    {
        // Switch scenes after player collides with the car.
        if (collision.collider.CompareTag("Car") && OnEnterCar != null)
        {
            AudioManager.instance.PlayAudioClip(EnterCarSound, transform, 1f);
            StartCoroutine(FadeToBlackAndSwitchScene());
        }
    }

    private IEnumerator FadeToBlackAndSwitchScene()
    {
        // Fade to black over 1.5 seconds.
        float fadeDuration = 1.5f;
        float fadeAmount = 0;

        // Gradually increase the alpha of the image.
        while (fadeAmount < 1)
        {
            fadeAmount += Time.deltaTime / fadeDuration;
            SetFadeAlpha(fadeAmount);
            yield return null;
        }

        // Wait for the sound to finish playing.
        yield return new WaitForSeconds(EnterCarSound.length - fadeDuration);

        // Switch to the new scene.
        OnEnterCar(2);
    }

    private void SetFadeAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = Mathf.Clamp01(alpha); 
        fadeImage.color = color;
    }
}

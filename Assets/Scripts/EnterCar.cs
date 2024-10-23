using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnterCar : MonoBehaviour
{
    public static event Action<int> OnEnterCar;
    [SerializeField] private AudioClip EnterCarSound;
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Car") && OnEnterCar != null)
        {
            if (fadeImage != null)
            {
                fadeImage.gameObject.SetActive(true);
            }

            AudioManager.instance.PlayAudioClip(EnterCarSound, transform, 1f);
            StartCoroutine(FadeToBlackAndSwitchScene());
        }
    }

    private IEnumerator FadeToBlackAndSwitchScene()
    {
        float fadeDuration = 1.5f;
        float fadeAmount = 0;

        while (fadeAmount < 1)
        {
            fadeAmount += Time.deltaTime / fadeDuration;
            SetFadeAlpha(fadeAmount);
            yield return null;
        }

        yield return new WaitForSeconds(EnterCarSound.length - fadeDuration);
        OnEnterCar(2);
    }

    private void SetFadeAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = Mathf.Clamp01(alpha);
        fadeImage.color = color;
    }
}

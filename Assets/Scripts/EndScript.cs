using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSCript : MonoBehaviour
{
    [SerializeField] GameObject[] textObjects;
    [SerializeField] float initialDelay = 2.0f;
    [SerializeField] float textDisplayInterval = 1.5f;
    [SerializeField] float textDisplayDuration = 3f;

    private void Start()
    {
        StartCoroutine(DisplayTextSequence());

    }

    IEnumerator DisplayTextSequence()
    {
        yield return new WaitForSeconds(initialDelay);

        foreach (GameObject textObject in textObjects)
        {
            textObject.SetActive(true);
            yield return new WaitForSeconds(textDisplayDuration);
            textObject.SetActive(false);

            yield return new WaitForSeconds(textDisplayInterval);
        }

        SceneManager.LoadScene(0);
    }
}

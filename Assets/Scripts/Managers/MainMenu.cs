using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer cinematicVideo;
    public AudioSource mainMenuMusic;
    public string gameSceneName;

    private bool isVideoPlaying = false;

    void Start()
    {
        if (!Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        cinematicVideo.Stop();
        cinematicVideo.loopPointReached += OnCinematicEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isVideoPlaying)
            {
                SkipCinematic();

            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 0f;

        if (mainMenuMusic != null)
        {
            mainMenuMusic.Stop();
        }

        if (cinematicVideo != null)
        {
            isVideoPlaying = true;
            cinematicVideo.Play();
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    public void QuitGame()
    {
        Console.WriteLine("Outta here");
        Application.Quit();
    }

    private void OnCinematicEnd(VideoPlayer vp)
    {
        isVideoPlaying = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void SkipCinematic()
    {
        if (cinematicVideo.isPlaying)
        {
            isVideoPlaying = false;
            cinematicVideo.Stop();
            Time.timeScale = 1f;
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private void OnDestroy()
    {
        // Removing the video from the function so it doesn't subscribe to the function multiple times if the player reenteres the main menu.
        cinematicVideo.loopPointReached -= OnCinematicEnd;
    }
}

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
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        cinematicVideo.Stop();
        cinematicVideo.loopPointReached += OnCinematicEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isVideoPlaying)
            {
                SkipCinematic();
            }
        }
    }

    public void StartGame()
    {
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
        SceneManager.LoadScene(gameSceneName);
    }

    public void SkipCinematic()
    {
        if (cinematicVideo.isPlaying)
        {
            isVideoPlaying = false;
            cinematicVideo.Stop();
            SceneManager.LoadScene(gameSceneName);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    public string mainMenuSceneName;

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void ResumeGameFromButton()
    {
        ResumeGame();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Console.WriteLine("Outta here");
        Application.Quit();
    }
}

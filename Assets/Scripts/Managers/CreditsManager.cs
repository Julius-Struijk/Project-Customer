using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public GameObject[] creditSections; // Array of credit sections (each section is a GameObject)
    public float sectionDisplayTime = 3f; // Time each section stays visible
    public float sectionPauseTime = 1f; // Time between the display of sections

    public GameObject mainMenu; // The main menu UI
    public GameObject creditsMenu; // The credits UI container

    private void Start()
    {
        // Ensure credits sections are invisible at the start
        foreach (GameObject section in creditSections)
        {
            section.SetActive(false);
        }

        // Hide the credits container initially
        creditsMenu.SetActive(false);
    }

    private void Update()
    {
        // Skip credits if buttons are pressed.
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) 
        {
            if(creditsMenu.activeSelf && !mainMenu.activeSelf) { EndCredits(); }
        }
    }

    // This method will be called when the button to view credits is pressed
    public void StartCredits()
    {
        // Hide the main menu
        mainMenu.SetActive(false);

        // Show the credits menu container
        creditsMenu.SetActive(true);

        // Start the coroutine to show the credits one by one
        StartCoroutine(ShowCredits());
    }

    IEnumerator ShowCredits()
    {
        foreach (GameObject section in creditSections)
        {
            // Enable the current section
            section.SetActive(true);

            // Wait for the display time
            yield return new WaitForSeconds(sectionDisplayTime);

            // Disable the current section
            section.SetActive(false);

            // Wait for the pause time before showing the next section
            yield return new WaitForSeconds(sectionPauseTime);
        }

        // After all credits have been shown, return to the main menu
        EndCredits();
    }

    void EndCredits()
    {
        // Hide the credits menu
        creditsMenu.SetActive(false);

        // Show the main menu again
        mainMenu.SetActive(true);
    }
}

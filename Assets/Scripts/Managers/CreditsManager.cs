using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public GameObject[] creditSections;
    public float sectionDisplayTime = 3f;
    public float sectionPauseTime = 1f;

    public GameObject mainMenu;
    public GameObject creditsMenu;

    private void Start()
    {
        foreach (GameObject section in creditSections)
        {
            section.SetActive(false);
        }

        creditsMenu.SetActive(false);
    }

    private void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) 
        {
            if(creditsMenu.activeSelf && !mainMenu.activeSelf) { EndCredits(); }
        }
    }

    public void StartCredits()
    {

        mainMenu.SetActive(false);


        creditsMenu.SetActive(true);


        StartCoroutine(ShowCredits());
    }

    IEnumerator ShowCredits()
    {
        foreach (GameObject section in creditSections)
        {

            section.SetActive(true);
            yield return new WaitForSeconds(sectionDisplayTime);

            section.SetActive(false);
            yield return new WaitForSeconds(sectionPauseTime);
        }

        EndCredits();
    }

    void EndCredits()
    {
        creditsMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
}

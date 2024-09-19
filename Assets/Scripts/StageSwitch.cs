using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSwitch : MonoBehaviour
{
    [SerializeField] UDictionary<GameObject, GameObject> modeObjectPairs;
    int stageCounter = 0;
    GameObject regularObject;
    GameObject neonObject;

    private void Start()
    {
        ScoreBar.OnScoreStageChange += IncreaseStage;
    }

    void IncreaseStage(string stageText, int stageNumber)
    {

        // Change stage based on score.
        if (stageNumber < modeObjectPairs.Count && stageNumber >= 0)
        {
            // Assign the objects for the stage.
            regularObject = modeObjectPairs.Keys[stageNumber];
            neonObject = modeObjectPairs.Values[stageNumber];
            stageCounter++;

            Debug.Log("Going up to stage " + stageNumber);
            regularObject.SetActive(false);
            neonObject.SetActive(true);
        }
    }

    void DecreaseStage()
    {
        if (Input.GetKeyDown(KeyCode.E) && stageCounter > 0)
        {
            stageCounter--;
            regularObject = modeObjectPairs.Keys[stageCounter];
            neonObject = modeObjectPairs.Values[stageCounter];

            Debug.Log("Going down to stage " + stageCounter);
            neonObject.SetActive(false);
            regularObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        ScoreBar.OnScoreStageChange -= IncreaseStage;
    }
}

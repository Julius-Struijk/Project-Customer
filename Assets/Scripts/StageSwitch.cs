using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageSwitch : MonoBehaviour
{
    public static event Action OnVisualStageChange;

    [SerializeField] UDictionary<GameObject, GameObject> modeObjectPairs;
    int stageCounter = 0;
    GameObject regularObject;
    GameObject neonObject;

    private void Start()
    {
        ScoreBar.OnScoreStageChange += IncreaseDrugStage;
        ChildEffect.OnSlowMotion += DrugStageToZero;
    }

    void IncreaseDrugStage(string stageText, int stageNumber)
    {

        // Change stage based on score.
        if (stageNumber < modeObjectPairs.Count && stageNumber >= 0)
        {
            // Trigger blink to hide the stage transition.
            if (OnVisualStageChange != null) { OnVisualStageChange(); }

            // Assign the objects for the stage.
            regularObject = modeObjectPairs.Keys[stageNumber];
            neonObject = modeObjectPairs.Values[stageNumber];
            stageCounter++;

            Debug.Log("Going up to stage " + stageNumber);
            regularObject.SetActive(false);
            neonObject.SetActive(true);

        }
    }

    void DrugStageToZero()
    {
        while(stageCounter > 0)
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
        ScoreBar.OnScoreStageChange -= IncreaseDrugStage;
        ChildEffect.OnSlowMotion -= DrugStageToZero;
    }
}

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
            if (regularObject != null)
            {
                // Create an exception for terrain where the visibility is disabled instead of the active status, so they can keep being generated in the background.
                if (regularObject.CompareTag("RegTerrain") && neonObject.CompareTag("NeonTerrain"))
                {
                    // Find all of the children terrain and disable/enable their visibility accordingly.
                    SetTerrainChildren(regularObject, false);
                    SetTerrainChildren(neonObject, true);
                }
                else
                {
                    regularObject.SetActive(false);
                    neonObject.SetActive(true);
                }
            }
            else { neonObject.SetActive(true); }

        }
    }

    void DrugStageToZero()
    {
        while (stageCounter > 0)
        {
            stageCounter--;
            regularObject = modeObjectPairs.Keys[stageCounter];
            neonObject = modeObjectPairs.Values[stageCounter];

            Debug.Log("Going down to stage " + stageCounter);
            if (regularObject != null)
            {
                // Create an exception for terrain where the visibility is disabled instead of the active status, so they can keep being generated in the background.
                if (regularObject.CompareTag("RegTerrain") && neonObject.CompareTag("NeonTerrain"))
                {
                    // Find all of the children terrain and disable/enable their visibility accordingly.
                    SetTerrainChildren(neonObject, false);
                    SetTerrainChildren(regularObject, true);
                }
                else
                {
                    neonObject.SetActive(false);
                    regularObject.SetActive(true);
                }
            }
            else { neonObject.SetActive(false); }
        }
    }

    void SetTerrainChildren(GameObject terrainParent, bool visibility) 
    {
        for (int i = 0; i < terrainParent.transform.childCount; i++)
        {
            GameObject child = terrainParent.transform.GetChild(i).gameObject;
            Terrain childTerrain = child.GetComponent<Terrain>();
            childTerrain.enabled = visibility;
            //Debug.Log(string.Format("Child {0} of {1} terrain set to {2}", terrainParent, i, visibility));
        }
    }

    private void OnDestroy()
    {
        ScoreBar.OnScoreStageChange -= IncreaseDrugStage;
        ChildEffect.OnSlowMotion -= DrugStageToZero;
    }
}

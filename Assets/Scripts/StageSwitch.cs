using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSwitch : MonoBehaviour
{
    [SerializeField] UDictionary<GameObject, GameObject> modeObjectPairs;
    int stageCounter = 0;
    GameObject regularObject;
    GameObject neonObject;

    // Update is called once per frame
    void Update()
    {

        // Change stage based on score or distance.
        // Temporary increase stage implementation
        if (Input.GetKeyDown(KeyCode.Q) && stageCounter < modeObjectPairs.Count)
        {
            // Assign the objects for the stage.
            regularObject = modeObjectPairs.Keys[stageCounter];
            neonObject = modeObjectPairs.Values[stageCounter];
            stageCounter++;

            Debug.Log("Going up to stage " + stageCounter);
            regularObject.SetActive(false);
            neonObject.SetActive(true);
        }
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
}

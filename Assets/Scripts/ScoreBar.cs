using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreBar : MonoBehaviour
{
    // Using a custom dictionary to have it show up in the editor.
    [SerializeField] UDictionary<int, string> scoreThresholds;

    public static event Action<string> OnScoreStageChange;

    float maxScore;
    float totalScore = 0;
    int scoreStageCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxScore = scoreThresholds.Keys[scoreStageCounter];
        ManageScore.OnScoreChange += ScoreUpdate;
    }

    void ScoreUpdate(int score)
    {
        // If the score is at 0 it signifies the start of a new stage.
        if (totalScore == 0 && OnScoreStageChange != null)
        {
            Debug.Log("Updateing stage UI to: " + scoreThresholds.Values[scoreStageCounter]);
            OnScoreStageChange(scoreThresholds.Values[scoreStageCounter]);
        }

        totalScore += score;
        Debug.Log(string.Format("Score: {0} Max score: {1}", totalScore, maxScore));

        transform.localScale = new Vector3(totalScore / maxScore, 1, 1);

        if (totalScore >= maxScore && scoreStageCounter != scoreThresholds.Count - 1)
        {
            scoreStageCounter++;
            maxScore = scoreThresholds.Keys[scoreStageCounter];
            totalScore = 0;
        }
    }

    private void OnDestroy()
    {
        ManageScore.OnScoreChange -= ScoreUpdate;
    }
}

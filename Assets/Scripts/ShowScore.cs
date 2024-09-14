using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int totalScore = 0;

    private void Awake()
    {
        ManageScore.OnScoreChange += ScoreUpdate;
    }

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void ScoreUpdate(int score)
    {
        if(scoreText != null)
        {
            totalScore += score;
            scoreText.text = "Score: " + totalScore;
        }
    }

    private void OnDestroy()
    {
        ManageScore.OnScoreChange -= ScoreUpdate;
    }
}

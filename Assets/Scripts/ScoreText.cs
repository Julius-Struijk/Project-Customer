using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        ScoreBar.OnScoreStageChange += StageUpdate;
    }

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void StageUpdate(string stageText, int stageNumber)
    {
        if (scoreText != null)
        {
            scoreText.text = stageText;
        }
    }

    private void OnDestroy()
    {
        ScoreBar.OnScoreStageChange -= StageUpdate;
    }
}

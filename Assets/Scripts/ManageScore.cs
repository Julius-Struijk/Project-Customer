using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManageScore : MonoBehaviour
{
    public static event Action<int> OnScoreChange;

    public static void ScoreChanged(int score)
    {
        OnScoreChange(score);
    }
}

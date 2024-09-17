using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManageScore : MonoBehaviour
{
    public static event Action<int> OnScoreChange;

    // I have the score change delegate on the game managaer so that each object that changes the score doesn't need it's own delegate and can instead be funneled into only one through this public method.
    public static void ScoreChanged(int score)
    {
        if(OnScoreChange != null)
        {
            OnScoreChange(score);
        }
    }
}

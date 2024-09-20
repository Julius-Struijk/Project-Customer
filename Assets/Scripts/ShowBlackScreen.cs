using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBlackScreen : MonoBehaviour
{
    CanvasRenderer canvas;

    private void Start()
    {
        canvas = GetComponent<CanvasRenderer>();
        canvas.SetAlpha(0);
        ChildEffect.OnChildHit += Blackout;
    }

    void Blink()
    {

    }
    void Blackout()
    {
        Debug.Log("Activating blackout");
        canvas.SetAlpha(255);
    }

    private void OnDestroy()
    {
        ChildEffect.OnChildHit -= Blackout;
    }
}

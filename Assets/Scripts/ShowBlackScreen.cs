using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowBlackScreen : MonoBehaviour
{
    // Delegate for the scene switch is on the blackout to ensure it happens before the scene switches.
    public static event Action<int> OnBlackout;

    CanvasRenderer canvas;
    [SerializeField] float blinkSpeed = 4f;
    [SerializeField] int maxBlinkStrength = 250;
    bool blinkingUp = false;
    bool blinkingDown = false;
    float blinkAlpha = 0f;

    private void Start()
    {
        canvas = GetComponent<CanvasRenderer>();
        canvas.SetAlpha(0);

        StageSwitch.OnVisualStageChange += StartBlink;
        ChildEffect.OnChildHit += Blackout;
    }

    private void Update()
    {
        // Increasing the transparency of the blink effect.
        if (blinkingUp)
        {
            blinkAlpha += blinkSpeed;
            if (blinkAlpha > maxBlinkStrength)
            {
                blinkingDown = true;
                blinkingUp = false;
            }
        }
        // Decreasing it after the max value has been reached.
        else if (blinkingDown)
        {
            blinkAlpha -= blinkSpeed;

            if (blinkAlpha < 0) { blinkingDown = false; }
        }

        if (Mathf.Clamp(blinkAlpha, 0, maxBlinkStrength) != 0)
        {
            //Debug.Log(string.Format("Setting alpha to: {0}", Mathf.Clamp(blinkAlpha, 0, maxBlinkStrength)));
            canvas.SetAlpha(Mathf.Clamp(blinkAlpha, 0, maxBlinkStrength));
        }
    }

    void StartBlink()
    {
        //Debug.Log("Blinking.");
        blinkingUp = true;
    }

    void Blackout()
    {
        //Debug.Log("Activating blackout");
        canvas.SetAlpha(255);

        if(OnBlackout != null) { OnBlackout(0); }
    }

    private void OnDestroy()
    {
        StageSwitch.OnVisualStageChange -= StartBlink;
        ChildEffect.OnChildHit -= Blackout;
    }
}

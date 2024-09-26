using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{

    [SerializeField] int coinScore = 1;
    [SerializeField] private AudioClip CoinSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ManageScore.ScoreChanged(coinScore);
            AudioManager.instance.PlayAudioClip(CoinSound, transform, .3f);
            Destroy(gameObject);
        }
    }
}

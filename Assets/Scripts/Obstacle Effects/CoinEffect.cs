using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{

    [SerializeField] int coinScore = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ManageScore.ScoreChanged(coinScore);
            Destroy(gameObject);
        }
    }
}

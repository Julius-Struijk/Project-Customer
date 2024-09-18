using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Destroying object: " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}

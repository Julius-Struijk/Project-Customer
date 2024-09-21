using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnterCar : MonoBehaviour
{
    public static event Action<int> OnEnterCar;

    private void OnCollisionEnter(Collision collision)
    {
        // Switch scenes after player collidese with the car.
        if(collision.collider.CompareTag("Car") && OnEnterCar != null)
        {
            OnEnterCar(2);
        }
    }
}

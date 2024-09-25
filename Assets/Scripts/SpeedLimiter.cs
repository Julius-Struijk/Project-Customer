using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedLimiter : MonoBehaviour
{
    public static event Action OnVelocityZero;

    public Rigidbody rb { get; private set; }
    [SerializeField] float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));

        if(Approx(rb.velocity.x, 0) && Approx(rb.velocity.y, 0) && Approx(rb.velocity.z, 0))
        {
            if(OnVelocityZero != null) { 
                OnVelocityZero();
                Debug.Log("VELOCITY IS ZERO. TIME TO DISABLE BRAKING!");
            }
        }
    }

    // If the value is close enough to the required value, it'll return true.
    static bool Approx(float a, float b, float epsilon = 0.0001f)
    {
        return Mathf.Abs(a - b) < epsilon;
    }
}

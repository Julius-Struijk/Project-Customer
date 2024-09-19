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

        if(rb.velocity == new Vector3(0, 0, 0))
        {
            if(OnVelocityZero != null) { OnVelocityZero(); }
        }
    }
}

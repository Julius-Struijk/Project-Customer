using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelDrive : MonoBehaviour
{
    [SerializeField] int torque = 200;
    WheelCollider wc;

    private void Awake()
    {
        // Disable brakes once velocity has reached zero.
        SpeedLimiter.OnVelocityZero += DisableBraking;
    }
    // Start is called before the first frame update
    void Start()
    {
        wc = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardMovement = Input.GetAxis("Vertical");
        Move(forwardMovement);
    }

    void Move(float accel)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        float thrustTorque = accel * torque;
        // If thrust has been changed to reverse but hasn't reached max torque, we turn on the brakes for a fast decellaration.
        if (thrustTorque < 0 && thrustTorque != -torque) { wc.brakeTorque = -thrustTorque; }

        wc.motorTorque = thrustTorque;
        if (thrustTorque > 0)
        {
            DisableBraking();
        }
    }

    void DisableBraking()
    {
        wc.brakeTorque = 0;
    }

    private void OnDestroy()
    {
        SpeedLimiter.OnVelocityZero -= DisableBraking;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVChange : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float fovChangeSpeed = 1;
    VelocityTools velocityTools;
    Camera carCamera;
    float baseFOV;

    // Start is called before the first frame update
    void Start()
    {
        velocityTools = target.GetComponent<VelocityTools>();
        carCamera = GetComponent<Camera>();
        baseFOV = carCamera.fieldOfView;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null && velocityTools != null)
        {
            Vector3 forwardVelocity = velocityTools.GetForwardVelocity();
            if (forwardVelocity != new Vector3(0, 0, 0))
            {
                //Debug.Log(string.Format("Forward velocity from camera length is: {0}", forwardVelocity.magnitude));
                //Debug.Log(string.Format("FOV change amount is: {0}", fovChangeSpeed * forwardVelocity.magnitude));
                carCamera.fieldOfView = baseFOV + (forwardVelocity.magnitude * fovChangeSpeed);
            }
        }
    }
}

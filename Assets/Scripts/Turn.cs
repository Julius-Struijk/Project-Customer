using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1;
    Vector3 turnVector;
    int currentTurnDirection = 1;
    VelocityTools velocityTools;

    // Start is called before the first frame update
    void Start()
    {
        velocityTools = GetComponent<VelocityTools>();
    }

    void FixedUpdate()
    {
        if(velocityTools != null)
        {
            Vector3 forwardVelocity = velocityTools.GetForwardVelocity();
            if (forwardVelocity != new Vector3(0, 0, 0))
            {

                turnVector = new Vector3(0, Input.GetAxis("Horizontal"), 0) * (turnSpeed * forwardVelocity.magnitude);
                //Debug.Log(string.Format("Forward velocity length is: {0}", forwardVelocity.magnitude));

                // Reversing the turn direction if moving backwards
                if (Input.GetAxis("Vertical") < 0) { currentTurnDirection = -1; }
                else if (Input.GetAxis("Vertical") > 0) { currentTurnDirection = 1; }
                // This delayed rotation allows the turn direction to remain the same if the up and down arrow keys aren't being pressed.
                transform.Rotate(turnVector * currentTurnDirection);
            }
        }
    }
}

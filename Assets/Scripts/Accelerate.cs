using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : MonoBehaviour
{
    public float speed = 1;
    public float groundDrag = 0.98f;
    [SerializeField] float maxSpeed;

    Rigidbody rb;
    VelocityTools velocityTools;
    Vector3 moveVector;
    Vector3 prevVelocity;
    Vector3 velocityChangeCheck;
    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocityTools = GetComponent<VelocityTools>();
    }

    // Update is called once per frame
    void Update()
    {
        // Could add camera relative controls by using camera.forward. Make sure to negate the tilt of the camera so the vector doesn't go downward
        moveVector = transform.forward * Input.GetAxis("Vertical") * speed;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            Debug.Log(string.Format("New velocity {0} VS prev velocity {1}", rb.velocity, prevVelocity));
            //Debug.Log(string.Format("Close to zero? {0}", Approx(rb.velocity.z, 0)));

            // Kind of hacky fix weird issue where forward velocity is completely removed when going over the connection between two road segments.
            // Also prevents all other sudden changes because it also seems to affect all other axes.
            if(rb.velocity != prevVelocity)
            {
                // Only update the previous velocity if it is different but before the current velocity is changed due to issues.
                prevVelocity = rb.velocity;
                if (rb.velocity.y - velocityChangeCheck.y > 0.3f)
                {
                    //rb.Sleep();
                    //rb.AddForce(prevVelocity - rb.velocity, ForceMode.Acceleration);
                    rb.velocity = velocityChangeCheck;
                    Debug.Log("Fixing velocity issue. Set velocity to: " + rb.velocity);
                }
                else { velocityChangeCheck = rb.velocity; }
            }
            else { Debug.Log("No movement between frames."); }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(moveVector * groundDrag);
            }

            // Adding a counteracting force that prevents sideways movement while grounded.
            if(rb.velocity.x != 0) 
            {
                rb.AddForce(-velocityTools.GetRightVelocity(), ForceMode.VelocityChange);
                rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.z, -maxSpeed, maxSpeed));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.8f) { grounded = true; }
        else { Debug.Log("Grounded not set to true."); }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    static bool Approx(float a, float b, float epsilon = 0.0001f)
    {
        return Mathf.Abs(a - b) < epsilon;
    }
}

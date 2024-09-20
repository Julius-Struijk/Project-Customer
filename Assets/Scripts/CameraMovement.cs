using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Define a struct for storing camera route data
    [System.Serializable]
    public struct CameraRoute
    {
        public Vector3 pointA;
        public Vector3 pointB;
        public Quaternion rotation; // One rotation for both point A and B
    }

    public CameraRoute[] routes; // List of routes (A -> B)
    public float speed = 5f; // Speed of camera movement

    private int currentRoute = 0; // Keeps track of the current route
    private Vector3 currentTarget; // Current target position

    void Start()
    {
        // Initialize the first route's point A as starting position
        transform.position = routes[currentRoute].pointA;
        transform.rotation = routes[currentRoute].rotation;

        // Set initial target to point B of the first route
        currentTarget = routes[currentRoute].pointB;
    }

    void Update()
    {
        // Move the camera towards the current target position
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Check if the camera has reached its target (point B of the current route)
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            // Move to the next route
            currentRoute = (currentRoute + 1) % routes.Length; // Loop through routes

            // Teleport to the next route's point A and set target to point B
            transform.position = routes[currentRoute].pointA;

            // Instantly set the camera's rotation to the new route's rotation (sharp cut)
            transform.rotation = routes[currentRoute].rotation;

            // Set new target to the next route's point B
            currentTarget = routes[currentRoute].pointB;
        }
    }
}
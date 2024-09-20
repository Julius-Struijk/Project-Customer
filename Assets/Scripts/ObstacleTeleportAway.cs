using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTeleportAway : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 5f;
    public Vector3 teleportCoordinates;

    private bool hasTeleported = false;

    void Update()
    {
        //check the distance to the player and it can only teleport once
        if (!hasTeleported)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            //if the player close enough, teleport it
            if (distanceToPlayer <= triggerDistance)
            {
                TeleportObject();
            }
        }
    }

    void TeleportObject()
    {
        transform.position = teleportCoordinates;

        hasTeleported = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    Rigidbody rb;
    RaycastHit hitInfo;
    RaycastHit prevHitInfo;
    Vector3 prevCarPosition;
    Vector3 spawnRaycastPosition;
    Vector3 removeRaycastPosition;
    public Vector3 directionOfMovement { private set; get; }

    [SerializeField] int distance = 120;
    int removeDistance;
    [SerializeField] List<GameObject> roadVariants;
    int spawnOverlap = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prevCarPosition = rb.position;
        removeDistance = distance * 2;
    }

    void FixedUpdate()
    {

        directionOfMovement = rb.position - prevCarPosition;

        if (directionOfMovement != new Vector3(0, 0, 0))
        {
            // Switching which direction roads are generated in depending on the movement direction of the player.
            if (directionOfMovement.z > 0.1f)
            {
                // Spawning a new road when the raycast doesn't detect a road.
                spawnRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
                // Removing roads when they get too far from the player via a raycast that checks if there is a road past a certain distance.
                removeRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - removeDistance);

                SpawnRoad(spawnRaycastPosition, directionOfMovement);
                DeleteRoad(removeRaycastPosition);
            }
            else
            {
                spawnRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
                removeRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + removeDistance);

                SpawnRoad(spawnRaycastPosition, directionOfMovement);
                DeleteRoad(removeRaycastPosition);
            }
        }

        prevCarPosition = rb.position;
    }

    void SpawnRoad(Vector3 raycastPosition, Vector3 movementDirection)
    {
        if (Physics.Raycast(raycastPosition, transform.up * -1, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("Road"))
            {
                //Debug.Log(string.Format("There is a road infront of the player at: {0}.", hitInfo.transform.position));
                prevHitInfo = hitInfo;
            }
        }
        else
        {
            Debug.Log(string.Format("Didn't find anything at {0}.", raycastPosition));
            GameObject roadToSpawn = roadVariants[Random.Range(0, roadVariants.Count)];
            if (prevHitInfo.transform != null)
            {
                // Changing the z position of where the road is spawned depending on the movement direction.
                float roadZposition = prevHitInfo.transform.position.z - spawnOverlap + roadToSpawn.transform.localScale.z;
                if(movementDirection.z < -0.1f) { roadZposition = prevHitInfo.transform.position.z + spawnOverlap - roadToSpawn.transform.localScale.z; }

                Vector3 spawnPosition = new Vector3(prevHitInfo.transform.position.x, prevHitInfo.transform.position.y, roadZposition);
                Instantiate(roadToSpawn, spawnPosition, prevHitInfo.transform.rotation);
                Debug.Log(string.Format("Spawned new road at {0}.", spawnPosition));
            }
            else { Debug.Log("Hit info is null."); }
        }
    }

    void DeleteRoad(Vector3 raycastPosition)
    {
        if (Physics.Raycast(raycastPosition, transform.up * -1, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("Road"))
            {
                Destroy(hitInfo.collider.gameObject);
                Debug.Log(string.Format("Deleted road behind the player at: {0}. Raycast position is: {1}", hitInfo.transform.position, raycastPosition));
            }
        }
    }
}

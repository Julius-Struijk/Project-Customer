using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    Rigidbody rb;
    RaycastHit hitInfo;
    RaycastHit prevHitInfo;
    [SerializeField] int distance = 150;
    [SerializeField] List<GameObject> roadVariants;
    int spawnOverlap = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        // Spawning a new road when the raycast doesn't detect a road.
        Vector3 spawnRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);

        // Removing roads when they get too far from the player via a raycast that checks if there is a road past a certain distance.
        Vector3 removeRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);


        Debug.Log(string.Format("Rotation: {0}", transform.rotation.y));
        // Switching which direction roads are generated in depending on the rotation of the player.
        if ((transform.rotation.y < 0.5 && transform.rotation.y > -0.5))
        {
            SpawnRoad(spawnRaycastPosition);
            DeleteRoad(removeRaycastPosition);
        }
        //else
        //{
        //    SpawnRoad(removeRaycastPosition);
        //    DeleteRoad(spawnRaycastPosition);
        //}





    }

    void SpawnRoad(Vector3 raycastPosition)
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
            Vector3 spawnPosition = new Vector3(prevHitInfo.transform.position.x, prevHitInfo.transform.position.y, prevHitInfo.transform.position.z - spawnOverlap + roadToSpawn.transform.localScale.z);
            Instantiate(roadToSpawn, spawnPosition, prevHitInfo.transform.rotation);
            Debug.Log(string.Format("Spawned new road at {0}.", spawnPosition));
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

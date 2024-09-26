using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadGenerator : MonoBehaviour
{
    Rigidbody rb;
    RaycastHit hitInfo;
    RaycastHit prevHitInfo;
    Vector3 prevCarPosition;
    Vector3 spawnRaycastPosition;
    Vector3 removeRaycastPosition;
    Vector3 prevSpawnPosition;

    GameObject objectToSpawn;
    string objectTag;
    Terrain terrain;

    public static event Action<GameObject, Vector3> OnRoadSpawn;

    public Vector3 directionOfMovement { private set; get; }

    [SerializeField] int distance = 120;
    int removeDistance;
    [SerializeField] List<GameObject> objectVariants;
    [SerializeField] GameObject objectParent;
    int spawnOverlap = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prevCarPosition = rb.position;
        removeDistance = distance * 2;

        // Get the tag of the object that is being spawned for specific use cases depending on the object.
        objectToSpawn = objectVariants[UnityEngine.Random.Range(0, objectVariants.Count)];
        objectTag = objectToSpawn.tag;
        Debug.Log("Object tag is: " + objectTag);
    }

    void FixedUpdate()
    {

        directionOfMovement = rb.position - prevCarPosition;

        if (directionOfMovement != new Vector3(0, 0, 0))
        {
            // Switching which direction objects are generated in depending on the movement direction of the player.
            if (directionOfMovement.z > 0.1f)
            {
                // Spawning a new object when the raycast doesn't detect a object.
                spawnRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance);
                // Removing objects when they get too far from the player via a raycast that checks if there is a object past a certain distance.
                removeRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - removeDistance);

                SpawnObject(spawnRaycastPosition, directionOfMovement);
                DeleteObject(removeRaycastPosition);
            }
            else
            {
                spawnRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance);
                removeRaycastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + removeDistance);

                SpawnObject(spawnRaycastPosition, directionOfMovement);
                DeleteObject(removeRaycastPosition);
            }
        }

        prevCarPosition = rb.position;
    }

    void SpawnObject(Vector3 raycastPosition, Vector3 movementDirection)
    {

        if (Physics.Raycast(raycastPosition, transform.up * -1, out hitInfo) && hitInfo.collider.CompareTag(objectTag))
        {
            //Debug.Log(string.Format("There is a {0} infront of the player at: {1}.", objectTag, hitInfo.transform.position));
            prevHitInfo = hitInfo;
        }
        else
        {
            //Debug.Log(string.Format("Didn't find anything at {0} for {1}.", raycastPosition, objectTag));
            objectToSpawn = objectVariants[UnityEngine.Random.Range(0, objectVariants.Count)];
            if (prevHitInfo.transform != null)
            {
                // Terrain spawning is based of the size of the terrain instead of the scale.
                float roadZposition;
                if (objectTag == "RegTerrain" || objectTag == "NeonTerrain") {
                    terrain = objectToSpawn.GetComponent<Terrain>();
                    roadZposition = prevHitInfo.transform.position.z + terrain.terrainData.size.z;
                }
                else {
                    MeshRenderer mr = objectToSpawn.GetComponent<MeshRenderer>();
                    roadZposition = prevHitInfo.transform.position.z - spawnOverlap + mr.bounds.size.z;
                }

                // Changing the z position of where the object is spawned depending on the movement direction.
                if (movementDirection.z < -0.1f) {
                    
                    if (objectTag == "RegTerrain" || objectTag == "NeonTerrain")
                    {
                        terrain = objectToSpawn.GetComponent<Terrain>();
                        roadZposition = prevHitInfo.transform.position.z - terrain.terrainData.size.z;
                    }
                    else
                    {
                        MeshRenderer mr = objectToSpawn.GetComponent<MeshRenderer>();
                        roadZposition = prevHitInfo.transform.position.z + spawnOverlap - mr.bounds.size.z;
                    }
                }

                Vector3 spawnPosition = new Vector3(prevHitInfo.transform.position.x, prevHitInfo.transform.position.y, roadZposition);

                // Won't spawn the object if it's a duplicate in the same position as the previous road piece.
                if(prevSpawnPosition != null && prevSpawnPosition != spawnPosition)
                {
                    if(objectParent == null) { Instantiate(objectToSpawn, spawnPosition, prevHitInfo.transform.rotation); }
                    else 
                    {
                        Debug.Log("Setting terrain visibility during stage change.");
                        // Set the terrain visibility to what the previous terrain was set to.
                        Terrain prevTerrain = prevHitInfo.collider.gameObject.GetComponent<Terrain>();
                        terrain.enabled = prevTerrain.enabled;
                        Instantiate(objectToSpawn, spawnPosition, prevHitInfo.transform.rotation, objectParent.transform); 
                    }
                    Debug.Log(string.Format("Spawned new {0} at {1} to {2}.", objectTag, spawnPosition, objectParent));
                    prevSpawnPosition = spawnPosition;

                    if(objectTag == "Road")
                    {
                        //After spawning a road piece, the delegate will be fired and there's a chance a obstacle will be spawned as well.
                        if (OnRoadSpawn != null) { OnRoadSpawn(objectToSpawn, spawnPosition); }
                    }
                }
                //else { Debug.Log(string.Format("Duplicate {0} not spawned.", objectTag)); }
            }
            else { Debug.Log(string.Format("Hit info is null for {0}.", objectTag)); }
        }
    }

    void DeleteObject(Vector3 raycastPosition)
    {
        if (Physics.Raycast(raycastPosition, transform.up * -1, out hitInfo))
        {
            if (hitInfo.collider.CompareTag(objectTag))
            {
                Destroy(hitInfo.collider.gameObject);
                Debug.Log(string.Format("Deleted spawned {0} behind the player at: {1}. Raycast position is: {2}", objectTag, hitInfo.transform.position, raycastPosition));
            }
        }
    }
}

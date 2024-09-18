using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{

    [SerializeField] int spawnChancePercentage = 50;
    [SerializeField] List<GameObject> obstacles;
    
    float verticalBuffer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        RoadGenerator.OnRoadSpawn += ObstacleSpawn;
    }

    void ObstacleSpawn(GameObject road, Vector3 roadPosition)
    {
        //RNG check to see if obstacle spawns.
        if(Random.Range(1, 100) <= spawnChancePercentage) 
        {
            GameObject obstacleToSpawn = obstacles[Random.Range(0, obstacles.Count)];

            // Changing which scale is added based on the obstacle's rotation.
            float spawnX = Random.Range(roadPosition.x - road.transform.localScale.x / 2 + obstacleToSpawn.transform.localScale.x, roadPosition.x + road.transform.localScale.x / 2 - obstacleToSpawn.transform.localScale.x);

            float spawnY;
            if (obstacleToSpawn.transform.rotation.z >= 0.5f) { spawnY = roadPosition.y + obstacleToSpawn.transform.localScale.x + verticalBuffer; }
            else {  spawnY = roadPosition.y + obstacleToSpawn.transform.localScale.y + verticalBuffer; }

            float spawnZ = Random.Range(roadPosition.z - road.transform.localScale.z / 2, roadPosition.z + road.transform.localScale.z / 2);

            Vector3 spawnPosition = new Vector3(spawnX , spawnY, spawnZ);
            Instantiate(obstacleToSpawn, spawnPosition, obstacleToSpawn.transform.rotation);
            Debug.Log("Spawned new obstacle at: " + spawnPosition);
        }
    }

    private void OnDestroy()
    {
        RoadGenerator.OnRoadSpawn -= ObstacleSpawn;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{

    [SerializeField] int spawnChancePercentage = 50;
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] UDictionary<GameObject, int> obstaclesToBeAdded;
    
    float verticalBuffer = 0.5f;
    float horizontalBuffer = 4f;

    // Start is called before the first frame update
    void Start()
    {
        RoadGenerator.OnRoadSpawn += ObstacleSpawn;
        ScoreBar.OnScoreStageChange += AddObstacles;
    }

    void ObstacleSpawn(MeshRenderer road, Vector3 roadPosition)
    {
        //RNG check to see if obstacle spawns.
        if(Random.Range(1, 100) <= spawnChancePercentage) 
        {
            GameObject obstacleToSpawn = obstacles[Random.Range(0, obstacles.Count)];

            // Changing which scale is added based on the obstacle's rotation.
            float spawnX = Random.Range(roadPosition.x - road.bounds.size.x / 2 + obstacleToSpawn.transform.localScale.x + horizontalBuffer, roadPosition.x + road.bounds.size.x / 2 - obstacleToSpawn.transform.localScale.x - horizontalBuffer);

            float spawnY;
            if (obstacleToSpawn.transform.rotation.z >= 0.5f) { spawnY = roadPosition.y + obstacleToSpawn.transform.localScale.x + verticalBuffer; }
            else {  spawnY = roadPosition.y + obstacleToSpawn.transform.localScale.y + verticalBuffer; }

            float spawnZ = Random.Range(roadPosition.z - road.bounds.size.z / 2, roadPosition.z + road.bounds.size.z / 2);

            Vector3 spawnPosition = new Vector3(spawnX , spawnY, spawnZ);
            Instantiate(obstacleToSpawn, spawnPosition, obstacleToSpawn.transform.rotation);
            Debug.Log(string.Format("Spawned new obstacle {0} at: {1}", obstacleToSpawn.name, spawnPosition));
        }
    }

    void AddObstacles(string stageName, int stageNumber)
    {
        if(obstaclesToBeAdded != null)
        {
            //foreach (KeyValuePair<int, GameObject> obstacle in obstaclesToBeAdded)
            for (int i = obstaclesToBeAdded.Keys.Count - 1; i >= 0; i--)
            {
                GameObject spawnObstacle = obstaclesToBeAdded.Keys[i];
                int spawnStage = obstaclesToBeAdded.Values[i];

                // Ends the loop if the spawnstage of the obstacle is higher than the current stage number.
                if (spawnStage > stageNumber) { break; }
                else
                {
                    Debug.Log(string.Format("Adding {0} to the obstacle pool at stage {1}", spawnObstacle.name, spawnStage));
                    obstacles.Add(spawnObstacle);
                    // Removing the obstacle from the to be spawned list since it's now in the main list.
                    obstaclesToBeAdded.Remove(spawnObstacle);
                }
            }
        }
    }

    private void OnDestroy()
    {
        RoadGenerator.OnRoadSpawn -= ObstacleSpawn;
        ScoreBar.OnScoreStageChange -= AddObstacles;
    }
}

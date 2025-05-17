using System;
using UnityEngine;
using Random = System.Random;

public class GroundtileController : MonoBehaviour
{

    private GroundSpawner _groundSpawner;
    private int _randomSpawnPoint;

    public GameObject[] obstaclePrefabs;
    public GameObject platformPrefab;
    public Transform[] obstacleSpawnPoints;
    public Transform[] platformSpawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
        SpawnPlatform();
    }

    //looks for what to spawn next tile
    private void OnTriggerExit(Collider other)
    {
        _groundSpawner.SpawnTile();

        Destroy(gameObject, 5f); //destorys tile after 5 seconmds
    }

    // Update is called once per frame
    void Update()
    {

    }

    //spawns one of 2 obstacles on a random spawn point set on the tile
    public void SpawnObstacle() 
    {
        _randomSpawnPoint = UnityEngine.Random.Range(0, obstacleSpawnPoints.Length); //chooses one of available spawn points
        int spawnObsPrefab = UnityEngine.Random.Range(0, obstaclePrefabs.Length); //chooses a prefab to spawn

        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(obstaclePrefabs[spawnObsPrefab], obstacleSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
    }

    //
    public void SpawnPlatform()
    {
        /*
        int chanceOfSpawn = UnityEngine.Random.Range(0, 10); // Random number between 0 and 9
        if (chanceOfSpawn == 0)  // 1 out of 10 chance
        {
            Instantiate(platformPrefab, platformSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
        }
        */
        
        int chanceOfSpawn = UnityEngine.Random.Range(0, 10); 
        _randomSpawnPoint = UnityEngine.Random.Range(0, platformSpawnPoints.Length);

        if (chanceOfSpawn == 0)
        {
            GameObject platform = Instantiate(platformPrefab, platformSpawnPoints[_randomSpawnPoint].transform.position,
                Quaternion.identity, transform);

            // Get the PlatformController component from the spawned platform
            PlatformController platformControllerInstance = platform.GetComponent<PlatformController>();

            if (platformControllerInstance != null)
            {
                platformControllerInstance.SpawnPickup(); // Call the function that spawns pickups
            }
        }
    }

    // ADDITIONAL CODEEE
    //void OnObjectDestroy()
    //{
    //    if (ScoreManager.Instance != null)
    //    {
    //        ScoreManager.Instance.IncrementScore(1);
    //    }
    //}
}

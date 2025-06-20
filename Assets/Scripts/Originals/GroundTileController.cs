using System;
using UnityEngine;
using UnityEngine.Serialization;
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
        Debug.Log("Tile Spawn");

        _groundSpawner.tileNum++; // Increase count for tracking

        if (_groundSpawner.tileNum < 5) // First 5 tiles should be GroundTiles
        {
            Debug.Log("Spawning GroundTile");
            _groundSpawner.SpawnTile();
        }
        else // Switch to BossTiles
        {
            Debug.Log("Switching to BossTiles");
            _groundSpawner.tileNum = 0; // Reset counter before spawning boss tiles
            _groundSpawner.SpawnBossTile();
        }

        Destroy(gameObject, 5f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //spawns one of 2 obstacles on a random spawn point set on the tile
    public void SpawnObstacle() 
    {
        /*
        _randomSpawnPoint = UnityEngine.Random.Range(0, obstacleSpawnPoints.Length); //chooses one of available spawn points
        int spawnObsPrefab = UnityEngine.Random.Range(0, obstaclePrefabs.Length); //chooses a prefab to spawn

        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(obstaclePrefabs[spawnObsPrefab], obstacleSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
        */
        
        foreach (Transform spawnPoint in obstacleSpawnPoints) // Loop through all spawn points
        {
            int spawnObsPrefab = UnityEngine.Random.Range(0, obstaclePrefabs.Length); // Choose a random obstacle prefab
            Instantiate(obstaclePrefabs[spawnObsPrefab], spawnPoint.position, Quaternion.identity, transform);
        }

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


        foreach (Transform spawnPoint in platformSpawnPoints) // Loop through platform spawn points
        {
            Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity, transform);
        }
        */
        
        _randomSpawnPoint = UnityEngine.Random.Range(0, platformSpawnPoints.Length); //chooses one of available spawn points

        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(platformPrefab, obstacleSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);

        // Optional chance-based spawn (keeps original idea)
        int chanceOfSpawn = UnityEngine.Random.Range(0, 10);

        if (chanceOfSpawn == 0) // 1 in 10 chance to spawn a pickup
        {
            GameObject platform = Instantiate(platformPrefab, platformSpawnPoints[UnityEngine.Random.Range(0, platformSpawnPoints.Length)].position, Quaternion.identity, transform);

            // Ensure it has the PlatformController component
            PlatformController platformControllerInstance = platform.GetComponent<PlatformController>();

            if (platformControllerInstance != null)
            {
                platformControllerInstance.SpawnPickup(); // Call the function to spawn pickups
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

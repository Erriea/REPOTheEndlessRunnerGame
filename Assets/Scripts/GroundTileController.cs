using System;
using UnityEngine;
using Random = System.Random;

public class Groundtile : MonoBehaviour
{
    private GroundSpawner _groundSpawner;
    private int _randomSpawnPoint;

    public GameObject[] obstaclePrefabs;
    public GameObject platformPrefab;
    public Transform[] obstacleSpawnPoints;
    public Transform[] platformSpawnPoints;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
        SpawnPlatform();
    }

    private void OnTriggerExit(Collider other)
    {
        _groundSpawner.SpawnTile();
        
        Destroy(gameObject, 2f); //destorys tile after 5 seconmds
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    //spawns one of 2 obstacles on a random spawn point set on the tile
    private void SpawnObstacle() 
    {
        _randomSpawnPoint = UnityEngine.Random.Range(0, obstacleSpawnPoints.Length); //chooses one of available spawn points
        int spawnObsPrefab = UnityEngine.Random.Range(0, obstaclePrefabs.Length); //chooses a prefab to spawn
        
        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(obstaclePrefabs[spawnObsPrefab], obstacleSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
    }

    //
    private void SpawnPlatform()  // used to be SpawnGround but im not sure if thatw as a typoor n0t
    {
        int chanceOfSpawn = UnityEngine.Random.Range(0, 10); // Random number between 0 and 9
        _randomSpawnPoint = UnityEngine.Random.Range(0, platformSpawnPoints.Length); //chooses one of available spawn points
        if (chanceOfSpawn == 0)  // 1 out of 10 chance
        {
            Instantiate(platformPrefab, platformSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
        }
        
    }
}

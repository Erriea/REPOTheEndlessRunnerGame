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
    public Transform[] platformSpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
        SpawnGround();
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
    public void SpawnObstacle() 
    {
        _randomSpawnPoint = UnityEngine.Random.Range(0, obstacleSpawnPoints.Length); //chooses one of available spawn points
        int spawnObsPrefab = UnityEngine.Random.Range(0, obstaclePrefabs.Length); //chooses a prefab to spawn

        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(obstaclePrefabs[spawnObsPrefab], obstacleSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
    }

    //
    public void SpawnGround()
    {
        int chanceOfSpawn = UnityEngine.Random.Range(0, 20); // Random number between 0 and 2
        if (chanceOfSpawn == 0)  // 1 out of 20 chance
        {
            Instantiate(platformPrefab, obstacleSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
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

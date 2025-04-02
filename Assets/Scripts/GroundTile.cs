using System;
using UnityEngine;
using Random = System.Random;

public class Groundtile : MonoBehaviour
{
    private GroundSpawner _groundSpawner;

    public GameObject[] obstaclePrefabs;
    public Transform[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
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

    public void SpawnObstacle()
    {
        int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length); //chooses one of available spawn points
        int spawnObsPrefab = UnityEngine.Random.Range(0, obstaclePrefabs.Length);
        
        Instantiate(obstaclePrefabs[spawnObsPrefab], spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity, transform);
    }
}

using UnityEngine;

public class StartTileController : MonoBehaviour
{
    private GroundSpawner _groundSpawner;
    private int _randomSpawnPoint;
    
    public GameObject[] obstaclePrefabs;
    public Transform[] obstacleSpawnPoints;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
    }

    private void OnTriggerExit(Collider other)
    {
        _groundSpawner.SpawnTile();

        Destroy(gameObject, 5f); //destorys tile after 5 seconmds
    }
    
    //spawns one of 2 obstacles on a random spawn point set on the tile
    public void SpawnObstacle() 
    {
        _randomSpawnPoint = UnityEngine.Random.Range(0, obstacleSpawnPoints.Length); //chooses one of available spawn points
        int spawnObsPrefab = UnityEngine.Random.Range(0, obstaclePrefabs.Length); //chooses a prefab to spawn

        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(obstaclePrefabs[spawnObsPrefab], obstacleSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
    }
}

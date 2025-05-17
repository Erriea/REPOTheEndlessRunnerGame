using UnityEngine;

public class BossTileController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GroundSpawner _groundSpawner;
    private int _randomSpawnPoint;

    //gameobject is for the asst
    public GameObject[] bossObsPrefabs;
    public GameObject bossPrefab;
    //transform is for position of SP
    public Transform[] bossObsSpawnPoints;
    public Transform[] bossSpawnPoints;
    
    void Awake()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        //SpawnBoss();
        SpawnBossObstacle();
    }

    //looks for what to spawn next tile
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("just wanna see when this is triggered");
        _groundSpawner.SpawnEnemyTile();
        
        Destroy(gameObject, 2f); //destorys tile after 2 seconmds
        Debug.Log("enemyTile gone");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnBossObstacle() 
    {
        _randomSpawnPoint = UnityEngine.Random.Range(0, bossObsSpawnPoints.Length); //chooses one of available spawn points
        int spawnEnemyObsPrefab = UnityEngine.Random.Range(0, bossObsPrefabs.Length); //chooses a prefab to spawn
        
        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(bossObsPrefabs[spawnEnemyObsPrefab], bossObsPrefabs[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
    }

    //
    private void SpawnBoss() // spawn enemy
    {
        
        _randomSpawnPoint = UnityEngine.Random.Range(0,bossSpawnPoints.Length); //chooses one of available spawn points
        
        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(bossPrefab, bossSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);

    }

}

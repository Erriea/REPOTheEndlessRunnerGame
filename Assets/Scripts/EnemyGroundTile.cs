using UnityEngine;

//DONT WORK ON THIS CLASS ILL FIX IT FOR PART 2
public class EnemyGroundTile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GroundSpawner _groundSpawner;
    private int _randomSpawnPoint;

    public GameObject[] enemyObsPrefabs;
    public GameObject enemyPrefab;
    public Transform[] enemyObsSpawnPoints;
    public Transform[] enemySpawnPoints;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnEnemy();
        SpawnEnemyObstacle();
    }

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
    
    private void SpawnEnemyObstacle() 
    {
        _randomSpawnPoint = UnityEngine.Random.Range(0, enemyObsSpawnPoints.Length); //chooses one of available spawn points
        int spawnEnemyObsPrefab = UnityEngine.Random.Range(0, enemyObsPrefabs.Length); //chooses a prefab to spawn
        
        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(enemyObsPrefabs[spawnEnemyObsPrefab], enemyObsPrefabs[_randomSpawnPoint].transform.position, Quaternion.identity, transform);
    }

    //
    private void SpawnEnemy() // spawn enemy
    {
        
        _randomSpawnPoint = UnityEngine.Random.Range(0, enemySpawnPoints.Length); //chooses one of available spawn points
        
        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(enemyPrefab, enemySpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);

    }

}

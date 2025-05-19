using UnityEngine;

public class BossTileController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GroundSpawner _groundSpawner;
    private int _randomSpawnPoint;

    //gameobject is for the asst
    public GameObject bossObsPrefab;
    public GameObject bossPrefab;
    //transform is for position of SP
    public Transform[] bossObsSpawnPoints;
    public Transform[] bossSpawnPoints;
    
    void Awake()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnBoss();
        SpawnBossObstacle();
    }

    //looks for what to spawn next tile
    private void OnTriggerExit(Collider other)
    {
        
        
        if(_groundSpawner.tileNum < 4)
        {
            _groundSpawner.SpawnBossTile();
        }
        else
        {
            _groundSpawner.tileNum = 0;
            Debug.Log("back to ground spawn");
            _groundSpawner.SpawnTile();
            
        }

        Destroy(gameObject, 5f); //destorys tile after 2 seconmds
        Debug.Log("enemyTile gone");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnBossObstacle() 
    {
        foreach (Transform spawnPoint in bossObsSpawnPoints) // Loop through all spawn points
        {
            Instantiate(bossObsPrefab, spawnPoint.position, Quaternion.identity, transform);
        }

        //spawn in the random obstacle prefab in the random spawn point
        //Instantiate(bossObsPrefab, bossObsSpawnPoints[].transform.position, Quaternion.identity, transform);
    }

    //
    private void SpawnBoss() // spawn enemy
    {
        _randomSpawnPoint = UnityEngine.Random.Range(0, bossSpawnPoints.Length); //chooses one of available spawn points
        Instantiate(bossPrefab, bossSpawnPoints[_randomSpawnPoint].transform.position, Quaternion.identity, transform);

    }
    

}

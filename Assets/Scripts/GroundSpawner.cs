using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTilePrefab;

    private Vector3 _nextSpawnPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 15; i++) //10 is for how many tiles at start
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        //spawns prefab
        GameObject tempGround = Instantiate(groundTilePrefab, _nextSpawnPoint, Quaternion.identity);
        //tells where to spawn next tile
        _nextSpawnPoint = tempGround.transform.GetChild(1).position; 
    }
}

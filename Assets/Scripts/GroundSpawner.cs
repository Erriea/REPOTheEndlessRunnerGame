using System;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject startTilePrefab;
    [SerializeField] GameObject groundTilePrefab;
    [SerializeField] GameObject bossTilePrefab;

    private Vector3 _nextSpawnPoint;
    
    // stuff for the timer of spaning the boss tile
    //private float _gameTime = 0f;
    //private bool _spawningBossTiles = false;
    //private bool _spawningTiles = false;
    //private int _bossTileCount = 0;
    
    void Start()
    {
        //starting tiles
        
        for (int i = 0; i < 15; i++) //10 is for how many tiles at start
        {
            if (i <= 6)
            {
                SpawnStartTile();
            }
            else
            {
                SpawnTile();
                //_spawningTiles = true;
            }
            
        }
    }



    //SPAWNS THE START TILE
    public void SpawnStartTile()
    {
        GameObject tempGround = Instantiate(startTilePrefab, _nextSpawnPoint, Quaternion.identity);
        _nextSpawnPoint = tempGround.transform.GetChild(1).position;
        Debug.Log("start has started");
    }
    
    //SPAWNS THE GAME TILES
    public void SpawnTile()
    {
        //if (_spawningTiles)
        //{
        //    //spawns prefab
        //    GameObject tempGround = Instantiate(groundTilePrefab, _nextSpawnPoint, Quaternion.identity);
        //    //tells where to spawn next tile
        //    _nextSpawnPoint = tempGround.transform.GetChild(1).position; 
        //}
        //else
        //{
        //    Debug.Log("not spawning tile");
        //    
        //}
        
        //spawns prefab
        GameObject tempGround = Instantiate(groundTilePrefab, _nextSpawnPoint, Quaternion.identity);
        //tells where to spawn next tile
        _nextSpawnPoint = tempGround.transform.GetChild(1).position; 
    }

    
    public void SpawnEnemyTile()
    {
        GameObject tempGround = Instantiate(groundTilePrefab, _nextSpawnPoint, Quaternion.identity);
        //tells where to spawn next tile
        _nextSpawnPoint = tempGround.transform.GetChild(1).position; 
    }
}

//void Update()
//{
        
    //STUFF COMMENTED LINKS TO BOSS THINGY THAT DOESNT WORK YET
    //DONT DELETE ANOF OF IT PLEASE
    ////checks the timer for this thiung
    //_gameTime += Time.deltaTime; 
//
    //// counts 60 seconds and then spawns the boss tile
    //if (_gameTime >= 60f && !_spawningBossTiles)
    //{
    //    _spawningBossTiles = true;
    //    _spawningTiles = false;
    //}
//
    //// spawning boss tiles
    //if (_spawningBossTiles)
    //{
    //    if (_bossTileCount < 3) // Spawn 3 boss tiles
    //    {
    //        SpawnEnemyTile();
    //        Debug.Log("spawns enemytile");
    //        _bossTileCount++; //update counter
    //    }
    //    else
    //    {
    //        _spawningBossTiles = false; // ends boss
    //        _spawningTiles = true; //resets normal tile respawn
    //        _gameTime = 0f;  // resets time
    //        _bossTileCount = 0; //resets tile count
    //    }
    //}
//}

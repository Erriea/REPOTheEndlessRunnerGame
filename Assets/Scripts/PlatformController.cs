using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;
public class PlatformController : MonoBehaviour
{
    private GroundSpawner _groundSpawner;
    private int _randomSpawnPoint;

    public GameObject[] pickupPrefabs;
    public Transform pickupSpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPickup()
    {
        
        //_randomSpawnPoint = UnityEngine.Random.Range(0, pickupSpawnPoint.Length); //chooses one of available spawn points
        int spawnPickUpPrefab = UnityEngine.Random.Range(0, pickupPrefabs.Length); //chooses a prefab to spawn

        //spawn in the random obstacle prefab in the random spawn point
        Instantiate(pickupPrefabs[spawnPickUpPrefab], pickupSpawnPoint.transform.position, Quaternion.identity, transform);

        /*// 1-in-5 chance to spawn something (0-based RNG → 0,1,2,3,4)
        if (UnityEngine.Random.Range(0, 5) != 0) return;

        // choose a prefab index 0..pickupPrefabs.Length-1
        int i = UnityEngine.Random.Range(0, pickupPrefabs.Length);

        Instantiate(
            pickupPrefabs[i],
            pickupSpawnPoint.position,
            Quaternion.identity /* no rotation 
        );
        //Instantiate(pickupPrefab, pickupSpawnPoint.transform.position, Quaternion.identity, transform);
        */

    }


}

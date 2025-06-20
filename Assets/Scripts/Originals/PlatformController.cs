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
        
        

        // 1-in-5 chance to spawn something (0-based RNG → 0,1,2,3,4)
        if (UnityEngine.Random.Range(0, 2) != 0) return;

        // choose a prefab index 0..pickupPrefabs.Length-1
        int i = UnityEngine.Random.Range(0, pickupPrefabs.Length);

        Instantiate(
            pickupPrefabs[i],
            pickupSpawnPoint.position,
            Quaternion.identity 
        );
        //Instantiate(pickupPrefab, pickupSpawnPoint.transform.position, Quaternion.identity, transform);
        

    }


}

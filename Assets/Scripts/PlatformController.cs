using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;
public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GroundSpawner groundSpawner;

    
    public GameObject pickupPrefab;
    public Transform pickupSpawnPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();

    }

    // Update is called once per frame
    void Update()
    {
        SpawnPickup();

    }

    private void SpawnPickup()
    {
        int chanceOfSpawn = UnityEngine.Random.Range(0, 5); // Random number between 0 and 4
        //if (chanceOfSpawn == 0)  // 1 out of 10 chance
        //{
        //    Instantiate(pickupPrefab, pickupSpawnPoint.transform.position, Quaternion.identity, transform);
        //}
        
        //Instantiate(pickupPrefab, pickupSpawnPoint.transform.position, Quaternion.identity, transform);
        Instantiate(pickupPrefab, pickupSpawnPoint.transform.position, Quaternion.identity); //revoves transform to avoid distortion
    }

}

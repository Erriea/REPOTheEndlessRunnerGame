using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;
public class PlatformController : MonoBehaviour
{
    private GroundSpawner _groundSpawner;
    
    public GameObject pickupPrefab;
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
        int chanceOfSpawn = UnityEngine.Random.Range(0, 5); // Random number between 0 and 4
        if (chanceOfSpawn != 0)  // 1 out of 10 chance
        {
            Instantiate(pickupPrefab, pickupSpawnPoint.transform.position, Quaternion.identity); //revoves transform to avoid distortion
        }
        
        
        
        
        //Instantiate(pickupPrefab, pickupSpawnPoint.transform.position, Quaternion.identity, transform);
        
    }
    

}

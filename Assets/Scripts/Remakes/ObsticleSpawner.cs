using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Remakes
{
    //POPULATER SEGMENTS WITH THE OBSTICLE PREFABS
    public class ObsticleSpawner : MonoBehaviour
    {
        public static ObsticleSpawner Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void PopulateSegment(GameObject segment)
        {
            if (TheGameManager.Instance == null)
            {
                Debug.LogWarning("GameManager not found, cannot populate segment");
                return;
            }

            var gm = TheGameManager.Instance;

            // Go through each child spawn point in this segment
            foreach (var t in segment.GetComponentsInChildren<Transform>(true))
            {
                GameObject prefab = null;
                Quaternion rot = t.rotation;

                switch (t.tag)
                {
                    case "TreeSpawn":          prefab = gm.treePrefab; break;
                    case "RockSpawn":          prefab = gm.rockPrefab; break;
                    case "LogSpawn":           prefab = gm.logPrefab; break;

                    case "ScorePickUpSpawn":   prefab = gm.scorePickupPrefab; rot = prefab.transform.rotation; break;
                    case "JumpPickUpSpawn":    prefab = gm.jumpPickupPrefab; rot = prefab.transform.rotation; break;
                    case "InvsPickUpSpawn":    prefab = gm.invinciblePickupPrefab; rot = prefab.transform.rotation; break;

                    case "IceSpawn":           prefab = gm.icePrefab; break;
                    case "Boss1Spawn":         prefab = gm.boss1Prefab; break;
                }

                if (prefab != null)
                {
                    Instantiate(prefab, t.position, rot, segment.transform);
                }
            }
        }
        
        /*
        void Start()
        {
            //run the logica as soon as TheGameManager is confirmed in scene
            StartCoroutine(SpawnWhenReady());
        }
        
        //SPAWN WHEN SEGMENT CREATED
        IEnumerator SpawnWhenReady()
        {
            // wait until GameManager singleton is ready
            yield return new WaitUntil(() => TheGameManager.Instance != null);

            //GAMEMANAGER
            //summon here so you dont have to write out whole thing each time
            var gm = TheGameManager.Instance;
            
            //NOW SAFE TO SPAWN-----------------------------
             In this code we will:
             * spawn all of the prefabs that have spawn points set up in this segment
             * set a bool for the prefab to check if it needs a fancy rotation
             * add all of the boss prefabs to a list and hide them until later
             
            
            //SUMMON THE NORMAL OBSTICLES
            Spawn("TreeSpawn", gm.treePrefab);
            Spawn("RockSpawn", gm.rockPrefab);
            Spawn("LogSpawn", gm.logPrefab);
            
            //SUMMON THE PICK UPS
            //check what isPickup is refering to
            Spawn("ScorePickUpSpawn", gm.scorePickupPrefab, isPickup: true);
            Spawn("JumpPickUpSpawn", gm.jumpPickupPrefab, isPickup: true);
            Spawn("InvsPickUpSpawn", gm.invinciblePickupPrefab, isPickup: true);
            
            //SUMMON THE BOSS1 PREFABS
            //Method(tagName, where initialised in gm)
            Spawn("IceSpawn", gm.icePrefab);
            Spawn("Boss1Spawn", gm.boss1Prefab);
        }

        //HERE WE DO THE POPULATING OF THE SEGMENT
        
         * find prefab tag
         * find prefabs prefab
         * check if its a pickup
         * regsiter the list
         
        void Spawn(string tag, GameObject prefab, bool isPickup = false)

        {
            //if theres no prefabs, stop running
            if (prefab == null)
                return;
            
            //not just the prefabs but the children too
            foreach (var t in GetComponentsInChildren<Transform>())
            {
                if (!t.CompareTag(tag))
                    continue;

                //SET ROTATION FOR PICK UPS
                Quaternion rot = isPickup
                    ? prefab.transform.rotation
                    : t.rotation;
                
                //SUMMON PREFAB AS INSTANCE
                //does this add the rot stuff for pickups to all the stuff? well find out
                var instance = Instantiate(prefab, t.position, rot, transform);

                Instantiate(prefab, t.position, rot, transform);
            }
        }*/
    }
}

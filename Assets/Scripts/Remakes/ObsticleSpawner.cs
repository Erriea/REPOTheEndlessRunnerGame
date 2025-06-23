using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Remakes
{
    //POPULATER SEGMENTS WITH THE OBSTICLE PREFABS
    public class ObsticleSpawner : MonoBehaviour
    {
        //LIST TO STORE BOSSE PREFABS THAT MOVE POSITION
        public List<GameObject> bosses1 = new List<GameObject>();
        public List<GameObject> ices   = new List<GameObject>();

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
            /* In this code we will:
             * spawn all of the prefabs that have spawn points set up in this segment
             * set a bool for the prefab to check if it needs a fancy rotation
             * add all of the boss prefabs to a list and hide them until later
             */
            
            //SUMMON THE NORMAL OBSTICLES
            Spawn("TreeSpawn", gm.treePrefab);
            Spawn("RockSpawn", gm.rockPrefab);
            Spawn("LogSpawn",  gm.logPrefab);
            
            //SUMMON THE PICK UPS
            //check what isPickup is refering to
            Spawn("ScorePickUpSpawn", gm.scorePickupPrefab, isPickup: true);
            Spawn("JumpPickUpSpawn",  gm.jumpPickupPrefab, isPickup: true);
            Spawn("InvsPickUpSpawn",  gm.invinciblePickupPrefab, isPickup: true);
            
            //SUMMON THE BOSS1 PREFABS
            //Method(tagName, where initialised in gm, where it stored in list)
            Spawn("IceSpawn",   gm.icePrefab,   registerList: ices);
            Spawn("Boss1Spawn", gm.boss1Prefab, registerList: bosses1);
            
            /* OLD WAY WITH TAGS JUST INCASE NEW WAY FAILS ME
            SpawnObstacleWithTag("TreeSpawn", TheGameManager.Instance.treePrefab);
            SpawnObstacleWithTag("RockSpawn", TheGameManager.Instance.rockPrefab);
            SpawnObstacleWithTag("LogSpawn", TheGameManager.Instance.logPrefab);

            SpawnObstacleWithTag("ScorePickUpSpawn", TheGameManager.Instance.scorePickupPrefab);
            SpawnObstacleWithTag("JumpPickUpSpawn", TheGameManager.Instance.jumpPickupPrefab);
            SpawnObstacleWithTag("InvsPickUpSpawn", TheGameManager.Instance.invinciblePickupPrefab);

            SpawnObstacleWithTag("IceSpawn", TheGameManager.Instance.icePrefab);
            SpawnObstacleWithTag("Boss1Spawn", TheGameManager.Instance.boss1Prefab);
            */
        }

        //HERE WE DO THE POPULATING OF THE SEGMENT
        /*
         * find prefab tag
         * find prefabs prefab
         * check if its a pickup
         * regsiter the list
         */
        void Spawn(string tag, GameObject prefab, bool isPickup = false, List<GameObject> registerList = null)
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

                //REGISTER AND HIDE BOSS STUFF
                if (registerList != null)
                {
                    registerList.Add(instance);

                    var hider = instance.GetComponent<OffScreenHider>();
                    if (hider != null)
                        hider.Hide();
                    else
                    {
                        Debug.LogWarning($"No {nameof(OffScreenHider)} found on {t.name}");
                    }
                }
            }
        }
    }
}

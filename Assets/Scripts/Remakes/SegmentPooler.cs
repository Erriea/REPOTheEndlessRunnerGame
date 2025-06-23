using System.Collections.Generic;
using UnityEngine;

namespace Remakes
{
    //FOR OPTIMISING SEGEMNT SPAWNING COZ I GOT A DATA LEAK
    //if game breaks after i add this DELETE
    public class SegmentPooler : MonoBehaviour
    {
        //MAKE MY BOI AN INSTANCE
        public static SegmentPooler Instance;
        
        //where we insert segements
        [SerializeField] GameObject[] segmentPrefab;
        [SerializeField] int poolSize = 3; //amount of segments
        
        /*
         * using a List not array coz lists are dynamic
         * arrays are the static ones that dont like chnage
         * populate lists with for loops
         */
        private List<GameObject> _pooledSegments = new List<GameObject>();

        void Awake()
        {
            Instance = this;
            
            //FILL POOL WITH WATER
            for (int i = 0; i < poolSize; i++)
            {
                foreach (GameObject prefab in segmentPrefab)
                {
                    //make prefab
                    //there is an error here
                    GameObject obj = Instantiate(prefab);
                    //make it not appear
                    obj.SetActive(false);
                    //add it to list
                    _pooledSegments.Add(obj);
                }
            }
        }
        
        //METHOD TO SUMMON MY SEGMENT ARMY (ONE AT A TIME)
        public GameObject GetRandomSegment()
        {
            //find segments not currently active
            List<GameObject> inactiveSegments = _pooledSegments.FindAll(obj => !obj.activeInHierarchy);
            
            //lets know if theres not available segments
            if (inactiveSegments.Count == 0)
            {
                Debug.LogWarning("Pool is empty!");
                return null;
            }
            
            //choose random one of available segments
            int index = Random.Range(0, inactiveSegments.Count);
            return inactiveSegments[index];
        }
        
        //RESET POOL
        public void ResetSegmentPool()
        {
            foreach (GameObject seg in _pooledSegments)
            {
                seg.SetActive(false);
            }
        }

    }
}

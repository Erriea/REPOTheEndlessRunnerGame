using System.Collections;
using UnityEngine;
//CONTROL TIMING FOR SEGMENT APPEAR AND DISAPPEARANCE
namespace Remakes
{
    public class SegmentController : MonoBehaviour
    {
        //SEGMENT ARRAY
        //if add more segments add here
        public GameObject[] segmentPrefab;

        //VARIABLES FOR SEGMENT PLACEMENT
        [SerializeField] private int zPos = 50;
        [SerializeField] bool createSegment = false;
        [SerializeField] int segmentNum;
        
        void Update()
        {
            //IF SEGMENT IS BEING CREATED, DONT MAKE ANOTHER UNTIL CURRENT ON IS DONE
            if (createSegment == false)
            {
                createSegment = true;
                StartCoroutine(SegmentGenerator());
            }
        }

        //COROUTINE TO SPAWN IN RANDOM NEW TILE EVERY 3 SECONDS
        IEnumerator SegmentGenerator()
        {
            //GENERATE RANDOM TILE TO PLACE NEXT
            segmentNum = Random.Range(0, segmentPrefab.Length);
            
            //GENERATE TILE IN GAME 50 Z SPACES FROM PREVIOUS TILE
            //Create instance of <objectName>.<x,y,z>,Quaternion.identity
            Instantiate(segmentPrefab[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);
            
            //increase zPos
            zPos += 50;
            
            //wait 3s before next segment spawn
            yield return new WaitForSeconds(3f);
            createSegment = false;
        }
    }
}

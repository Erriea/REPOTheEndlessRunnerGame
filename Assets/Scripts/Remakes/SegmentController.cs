using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CONTROL TIMING FOR SEGMENT APPEAR AND DISAPPEARANCE
namespace Remakes
{
    
    public class SegmentController : MonoBehaviour
    {
        //VARIABLES FOR SEGMENT PLACEMENT
        //start at spawn
        [SerializeField] private int zPos = 0;


        void Start()
        {
            StartCoroutine(SpawnSegmentLoop());
        }
        
        //NEW SUMMONER
        IEnumerator SpawnSegmentLoop()
        {
            while (true)
            {
                //finds random segment
                GameObject newSegment = SegmentPooler.Instance.GetRandomSegment();
                
                //continues if random segment exists
                if (newSegment != null)
                {
                    //moves segment into place
                    newSegment.transform.position = new Vector3(0, 0, zPos);
                    newSegment.SetActive(true);
                    zPos += 50;
                    
                    //deactivates segment
                    StartCoroutine(DisableAfterSeconds(newSegment, 60f));
                }

                yield return new WaitForSeconds(1f); // Wait between spawns
            }
        }

        //DESUMMONER
        IEnumerator DisableAfterSeconds(GameObject segment, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            segment.SetActive(false);
        }
        
        //RESET
        public void ResetZPosition()
        {
            zPos = 0;
        }

    }
    
}

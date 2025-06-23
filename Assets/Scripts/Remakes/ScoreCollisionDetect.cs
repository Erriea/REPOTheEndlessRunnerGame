using UnityEngine;
//CHECKS FOR COLLISION THAT MEAN THE PLAYER HAS PASSED AN OBSTICLE
namespace Remakes
{
    public class ScoreCollisionDetect : MonoBehaviour
    {
        //WHEN COLLISION IS TRIGGERED
        void OnTriggerEnter(Collider other)//collider refered to here is the players collider
        {
            Debug.Log("Score triggered");
            // Check if the thing that triggered this is the player
            if (!other.CompareTag("Player"))
                return;

            //CHECK IF CONDITIONS ARE MET
            var gm = TheGameManager.Instance;
            if (gm.isScorePUActive == true)
                LevelInfo.ScoreCount += 2;
            else if (gm.isScorePUActive == false)
                LevelInfo.ScoreCount += 1;
            
            //UPDATE SCORE ON UI
            gm.levelInfo.UpdateScoreUI();
        }
    }
}

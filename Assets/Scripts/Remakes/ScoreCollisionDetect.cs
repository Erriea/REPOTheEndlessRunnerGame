using UnityEngine;
//CHECKS FOR COLLISION THAT MEAN THE PLAYER HAS PASSED AN OBSTICLE
namespace Remakes
{
    public class ScoreCollisionDetect : MonoBehaviour
    {
        [SerializeField] GameObject thePlayer;

        //WHEN COLLISION IS TRIGGERED
        void OnTriggerEnter(Collider other)//collider refered to here is the players collider
        {
            Debug.Log("Score collision detected");

            //if double score active -> activates method
            if (DoubleScorePickUp.IsDSPickUpActive)
            {
                //StartCoroutine(TheGameManager.instance.DoubleScorePickUpActivate());
                LevelInfo.ScoreCount = LevelInfo.ScoreCount + 2;
                Debug.Log("Score pickup method called in ScoreCollisionDetect.");
            }
            else //score increase as usual
            {
                Debug.Log("Player hit a score trigger");
                LevelInfo.ScoreCount = LevelInfo.ScoreCount + 1;
            }

        }
    }
}

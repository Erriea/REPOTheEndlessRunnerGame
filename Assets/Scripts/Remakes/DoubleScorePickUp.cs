using UnityEngine;

//WHAT HAPPENED AFTER HITTING THIS PICKUP IN TERMS OF GAMEPLAY EFFECTS
namespace Remakes
{
    public class DoubleScorePickUp : MonoBehaviour
    {
        //WHEN IS TRIGGERED
        void OnTriggerEnter(Collider other) //collider refers to player collider
        {
            //PLAY AUDIO
            AudioManager.Instance.pickUpSFX.Play();
            
            //CHECK CONDITIONS FOR ACTIVATION
            if (!other.CompareTag("Player") || !TheGameManager.Instance._isRunnerScene)
                return;
            
            //INFORM GAME MANAGER OF ACTIVATION
            var gm = TheGameManager.Instance;
            gm.isPickUpActive    = true;
            gm.isScorePUActive   = true;
            gm.DeactivatePickUps();

            //UPDATE UI
            gm.levelInfo.UpdatePickUpUI();
            
            //LAUNCH DOUBLE SCORE ROUTINE
            gm.StartCoroutine(gm.DoubleScorePickUpActivate());
            
            //DISABLE PICKUP
            //gameObject.SetActive(false);
        }
    }
}

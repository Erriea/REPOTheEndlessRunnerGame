using UnityEngine;

namespace Remakes
{
    public class InvinsPickUp : MonoBehaviour
    {
        //WHEN IS TRIGGERED
        void OnTriggerEnter(Collider other) //collider refers to player collider
        {
            //PLAY AUDIO
            AudioManager.Instance.pickUpSFX.Play();
            
            //CHECK CONDITIONS FOR ACTIVATION
            if (!other.CompareTag("Player") || !TheGameManager.Instance._isRunnerScene)
                return;
            
            //INFORM GAME MANAGER OF PICK UP AND DEACTIVATE OTHERS
            var gm = TheGameManager.Instance;
            gm.isPickUpActive  = true;
            gm.isInvsPUActive  = true;
            gm.DeactivatePickUps();
            
            //UPDATE UI
            gm.levelInfo.UpdatePickUpUI();
            
            //SET PICKUP EFFECTS ACTIVE
            PlayerControllerRemake.IsInvincible = true;
            
            //SET GAMEMANAGER COROUTINE
            gm.StartInvincibility();
            Debug.Log("Invs PickUp routine called");
            
            //DEACTIVATE PICK UP
            //this.gameObject.SetActive(false);
        }
            
        
    }
}

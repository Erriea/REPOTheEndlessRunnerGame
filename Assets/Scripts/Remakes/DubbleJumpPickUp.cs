using UnityEngine;

namespace Remakes
{
    public class DubbleJumpPickUp : MonoBehaviour
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
            gm.isJumpPUActive  = true;
            gm.DeactivatePickUps();
            
            //UPDATE UI
            gm.levelInfo.UpdatePickUpUI();
            
            //SET GAMEMANAGER COROUTINE
            gm.StartCoroutine(gm.DoubleJumpPickUpActivate());
            Debug.Log("Jump PickUp routine called");
            
            //DISABLE PICKUP
            //this.gameObject.SetActive(false);
        }
    }
}

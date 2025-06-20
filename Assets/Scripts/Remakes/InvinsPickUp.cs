using UnityEngine;

namespace Remakes
{
    public class InvinsPickUp : MonoBehaviour
    {
        //INSTATIATE FX
        [SerializeField] AudioSource pickUpFX;
        
        //TO CHECK IF PICKUP IS ACTIVE
        public static bool IsInvPickUpActive = false;
    
        //WHEN IS TRIGGERED
        void OnTriggerEnter(Collider other) //collider refers to player collider
        {
            //INFORM GAME MANAGER OF PICK UP AND DEACTIVATE OTHERS
            TheGameManager.Instance.isPickUpActive = true;
            TheGameManager.Instance.DeactivatePickUps();
            
            //PLAY AUDIO ONCE
            pickUpFX.Play();
            IsInvPickUpActive = true;
            TheGameManager.Instance.StartInvincibility();
            Debug.Log("Inv PickUp Activated");
            
            //DEACTIVATE PICK UP
            this.gameObject.SetActive(false);
        }
            
        
    }
}

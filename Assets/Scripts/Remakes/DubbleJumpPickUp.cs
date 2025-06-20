using UnityEngine;

namespace Remakes
{
    public class DubbleJumpPickUp : MonoBehaviour
    {
        //INSTATIATE FX
        [SerializeField] AudioSource pickUpFX;
    
        //TO CHECK IF PICKUP IS ACTIVE
        public static bool IsDJPickUpActive = false;
    
        //WHEN IS TRIGGERED
        void OnTriggerEnter(Collider other) //collider refers to player collider
        {
            //INFORM GAME MANAGER OF PICK UP AND DEACTIVATE OTHERS
            TheGameManager.Instance.isPickUpActive = true;
            TheGameManager.Instance.DeactivatePickUps();
            
            //PLAY AUDIO ONCE
            pickUpFX.Play();
            IsDJPickUpActive = true;
            TheGameManager.Instance.StartCoroutine(TheGameManager.Instance.DoubleJumpPickUpActivate());
            Debug.Log("Jump PickUp Activated");
            
            //DISABLE PICKUP
            this.gameObject.SetActive(false);
        }
    }
}

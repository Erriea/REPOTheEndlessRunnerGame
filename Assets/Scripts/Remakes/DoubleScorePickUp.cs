using UnityEngine;

//WHAT HAPPENED AFTER HITTING THIS PICKUP IN TERMS OF GAMEPLAY EFFECTS
namespace Remakes
{
    public class DoubleScorePickUp : MonoBehaviour
    {
        //INSTATIATE FX
        [SerializeField] AudioSource pickUpFX;
    
        //TO CHECK IF PICKUP IS ACTIVE
        public static bool IsDSPickUpActive = false;
    
        //WHEN IS TRIGGERED
        void OnTriggerEnter(Collider other) //collider refers to player collider
        {
            //INFORM GAME MANAGER OF PICK UP AND DEACTIVATE OTHERS
            TheGameManager.Instance.isPickUpActive = true;
            TheGameManager.Instance.DeactivatePickUps();
            
            //PLAY AUDIO ONCE
            pickUpFX.Play();
            //SET PICKUP EFFECTS ACTIVE
            IsDSPickUpActive = true;
            StartCoroutine(TheGameManager.Instance.DoubleScorePickUpActivate());
            Debug.Log("Score PickUp Activated");
            
            //DISABLE PICKUP
            this.gameObject.SetActive(false);
        }
    }
}

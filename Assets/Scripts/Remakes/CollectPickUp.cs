using UnityEngine;
//CONTROLS WHAT HAPPENS WHEN A PICK UP IS TRIGGERED

//COIN COLLIDER HAS 'IS TRIGGER' CHECKED
namespace Remakes
{
    public class NewMonoBehaviourScript : MonoBehaviour
    {
        [SerializeField] AudioSource coinFX;

        //WHEN COIN IS TRIGGERED
        void OnTriggerEnter(Collider other)
        {
            //PLAY AUDIO ONCE
            coinFX.Play();
            this.gameObject.SetActive(false);
        }
    }
}

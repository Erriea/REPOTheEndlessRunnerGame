using UnityEngine;

namespace Remakes
{
    //MANAGE GAME AUDIO IN BETWEEN SCENESE
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        //BGM
        [SerializeField] public AudioSource gameplayBGM;
        [SerializeField] public AudioSource menuBGM;
        
        //SFX
        [SerializeField] public AudioSource buttonSFX;
        [SerializeField] public AudioSource deathSFX;
        [SerializeField] public AudioSource jumpSFX;
        [SerializeField] public AudioSource pickUpSFX;
        //[SerializeField] AudioSource boss1SFX;
        //[SerializeField] AudioSource boss2SFX;
        //[SerializeField] AudioSource bulletsSFX;
        
        //SINGLETON FOR AUDIO CONTROLS
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
        
    }
}

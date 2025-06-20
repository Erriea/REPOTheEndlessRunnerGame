using System.Collections;
using UnityEngine;

namespace Remakes
{
    public class TheGameManager : MonoBehaviour
    {
        public static TheGameManager Instance;
        
        private Coroutine _invincibleRoutine;

        // ADD LEVELINFO
        [SerializeField] LevelInfo levelInfo;

        //ADD BACKGROUND MUSIC FOR GAMEPLAY
        [SerializeField] public AudioSource gameplayBGM;

        //PICK UP INFO
        [SerializeField] GameObject pickUpScore;
        [SerializeField] GameObject pickUpJump;
        [SerializeField] GameObject pickUpInv;
        public bool isPickUpActive = false;


        //SINGLETON FOR GAME MANAGER
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

        void Start()
        {
            //START GAME MUSIC
            //Note: needs to be linked to played being alive or not
            //also idk if it will loop so well find out i guess
            //also need to find a way to make it stop halfway through or something
            gameplayBGM.Play();
        }

        void Update()
        {
            //UPDATE UI
            //check player live
            if (PlayerControllerRemake.IsAlive)
            {
                //continually update score
                // the amount it is updated by should go up and down elsewhere
                levelInfo.UpdateScoreUI();

                //check if pickupUI need changing
                if (isPickUpActive)
                {
                    levelInfo.UpdatePickUpUI();
                }
            }

        }

        //DEACTIVATE PICK UPS
        public void DeactivatePickUps()
        {
            pickUpScore.SetActive(false);
            pickUpJump.SetActive(false);
            pickUpInv.SetActive(false);
        }

        //REACTIVATE PICK UPS
        public void ReactivatePickUps()
        {
            pickUpScore.SetActive(true);
            pickUpJump.SetActive(true);
            pickUpInv.SetActive(true);
        }
        
        //STUFF FOR INVINCIBILITY
        public void StartInvincibility()
        {
            if (_invincibleRoutine != null)
                StopCoroutine(_invincibleRoutine);

            _invincibleRoutine = StartCoroutine(InvinsibilityPickUpActivate());
        }

        public void StopInvincibilityEarly()
        {
            if (_invincibleRoutine != null)
                StopCoroutine(_invincibleRoutine);

            PlayerControllerRemake.IsInvincible = false;
            InvinsPickUp.IsInvPickUpActive = false;
            isPickUpActive = false;
            ReactivatePickUps();
            Debug.Log("Invincibility ended early due to death collision.");
        }


        
        //FOR DOUBLE SCORE PICK UP
        public IEnumerator DoubleScorePickUpActivate()
        {
            //LevelInfo.ScoreCount = LevelInfo.ScoreCount + 2;
            Debug.Log("Score PickUp Coroutine Activated");

            //lasts 10 seconds
            yield return new WaitForSeconds(10f);
            isPickUpActive = false;
            DoubleScorePickUp.IsDSPickUpActive = false;
            DubbleJumpPickUp.IsDJPickUpActive = false;
            InvinsPickUp.IsInvPickUpActive = false;
            ReactivatePickUps();
        }

        //FOR DOUBLE JUMP PICK UP
        public IEnumerator DoubleJumpPickUpActivate()
        {
            Debug.Log("Jump PickUp Method Activated");
            PlayerControllerRemake.AllowDoubleJump = true;
            
            //lasts 8 seconds
            yield return new WaitForSeconds(8f);
            isPickUpActive = false;
            DoubleScorePickUp.IsDSPickUpActive = false;
            DubbleJumpPickUp.IsDJPickUpActive = false;
            InvinsPickUp.IsInvPickUpActive = false;
            ReactivatePickUps();
        }
        
        public IEnumerator InvinsibilityPickUpActivate()
        {
            Debug.Log("Inv PickUp Method Activated");
            PlayerControllerRemake.IsInvincible = true;
            
            //lasts 5 seconds
            yield return new WaitForSeconds(5f);
            PlayerControllerRemake.IsInvincible = false;
            isPickUpActive = false;
            DoubleScorePickUp.IsDSPickUpActive = false;
            DubbleJumpPickUp.IsDJPickUpActive = false;
            InvinsPickUp.IsInvPickUpActive = false;
            ReactivatePickUps();
            
            Debug.Log("Invincibility expired");
        }
            
            
            /*
            //FOR INVINSIBILITY PICK UP
            //check if you should chnage to a coroutine later
            public static void InvinsibilityPickUpActivate()
            {
                Debug.Log("Inv PickUp Method Activated");
            }
            */
    }
}

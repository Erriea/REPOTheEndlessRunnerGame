using UnityEngine;

//EVERYTHING THAT RELATES TO THE GAME UI
//its the renamed UIManager
namespace Remakes
{
    public class LevelInfo : MonoBehaviour
    {
        
        //INSTANTIATE TEXT FIELDS
        [SerializeField] GameObject scoreDisplay;
        [SerializeField] GameObject pickUpDisplay;
        //[SerializeField] GameObject pickUpTimerDisplay;
        //[SerializeField] GameObject levelDisplay;
        //[SerializeField] GameObject bossDisplay;
        
        //INSTANTIATE BG IMAGE
        [SerializeField] GameObject PickUpBack;
        
        //INSTANTIATE SCORE COUNTER
        //static so can interact with other scripts
        //amount gotten from collectPickUp
        public static int ScoreCount = 0;
        //public static int ScoreIncrease;
        
        void Update()
        {
            /*
            if (PlayerControllerRemake.IsAlive == true)
            {
                //UPDATE UI
                scoreDisplay.GetComponent<TMPro.TMP_Text>().text = "SCORE: " + ScoreCount;
            }
            */
        }
        
        public void UpdateScoreUI()
        {
            scoreDisplay.GetComponent<TMPro.TMP_Text>().text = "SCORE: " + ScoreCount;
        }
        
        //FIND PICK UP TRIGGERED AND UPDATE UI
        //need to add in a timer somehow
        public void UpdatePickUpUI()
        {
            if (TheGameManager.Instance.isPickUpActive)
            {
                if (DoubleScorePickUp.IsDSPickUpActive)
                {
                    pickUpDisplay.GetComponent<TMPro.TMP_Text>().text = "Double Score";
                }
                else if (DubbleJumpPickUp.IsDJPickUpActive)
                {
                    pickUpDisplay.GetComponent<TMPro.TMP_Text>().text = "Double Jump";
                }
                else if (InvinsPickUp.IsInvPickUpActive)
                {
                    pickUpDisplay.GetComponent<TMPro.TMP_Text>().text = "Invincibility";
                }
                pickUpDisplay.SetActive(true);
                PickUpBack.SetActive(true);
            }
            else
            {
                pickUpDisplay.SetActive(false);
                PickUpBack.SetActive(false);
            }
        }
        
        //LEVEL UI  
        private void UpdateLevelUI()
        {
            
        }
        
        //BOSS UI  
        private void UpdateBossUI()
        {
            
        }
    }
}

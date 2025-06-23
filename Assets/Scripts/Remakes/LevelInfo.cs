using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

//EVERYTHING THAT RELATES TO THE GAME UI
//its the renamed UIManager
namespace Remakes
{
    public class LevelInfo : MonoBehaviour
    {
        //INFO DISPLAYS
        [SerializeField] GameObject scoreDisplay;
        [SerializeField] GameObject pickUpDisplay;
        [SerializeField] GameObject pickUpTimerDisplay;
        [SerializeField] public GameObject levelDisplay;
        [SerializeField] public GameObject bossDisplay;
        
        //UI BACKGROUNDS
        [SerializeField] GameObject pickUpBack;
        [SerializeField] GameObject timerBack;
        [SerializeField] public GameObject levelBack;
        [SerializeField] public GameObject bossBack;
        
        //RUNNER TRANSITION STUFF
        [SerializeField] public GameObject fadeOutAnim;
        [SerializeField] public GameObject gameOverText;
        
        //PAUSE UI COMPONENTS
        [SerializeField] GameObject pauseMenuUI;
        [SerializeField] GameObject youSureUI;
        
        //DATA COLLECTION
        [SerializeField] public GameObject enterUsernameUI;
        [SerializeField] public GameObject inputUsername;
        [SerializeField] public GameObject yourScore;
        
        //VARIABLES
        //static so can interact with other scripts
        //amount gotten from collectPickUp
        public static int ScoreCount = 0;
        public static bool IsPause = false; //check if paused
        public static string UsernameInput = "";
        public static int TotalScore = 0;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //CHECK FOR PAUSE
                if (!TheGameManager.Instance._isRunnerScene) return;
                if (IsPause) return;

                PauseGame();
            }
        }
        
        //SCORE UI---------------------------------------------
        public void UpdateScoreUI()
        {
            scoreDisplay.GetComponent<TMPro.TMP_Text>().text = "SCORE: " + ScoreCount;
            TotalScore = ScoreCount;
        }
        
        //PICK UP UI -------------------------------------------------------------------
        //FIND PICK UP TRIGGERED AND UPDATE UI
        public void UpdatePickUpUI()
        {
            if (TheGameManager.Instance.isPickUpActive)
            {
                if (TheGameManager.Instance.isScorePUActive)
                {
                    pickUpDisplay.GetComponent<TMPro.TMP_Text>().text = "Double Score";
                }
                if (TheGameManager.Instance.isJumpPUActive)
                {
                    pickUpDisplay.GetComponent<TMPro.TMP_Text>().text = "Double Jump";
                }
                if (TheGameManager.Instance.isInvsPUActive)
                {
                    pickUpDisplay.GetComponent<TMPro.TMP_Text>().text = "Invincibility";
                }
                pickUpDisplay.SetActive(true);
                pickUpBack.SetActive(true);
            }
        }
        
        //DEACTIVATE PICK UP UI
        public void DisablePickUpUI()
        {
            pickUpDisplay.SetActive(false);
            pickUpBack.SetActive(false);
        }
        
        //TIMER UI ----------------------------------------------------------------
        //START COUNTER METHOD
        public void StartPickUpCountDown(int duration)
        {
            //make visible
            pickUpTimerDisplay.SetActive(true);
            timerBack.SetActive(true);
            Debug.Log("Starting pick up timer");
            StartCoroutine(PickUpTimer(duration));
        }
        
        //SETS THE TIMING FOR PICKUP
        private IEnumerator PickUpTimer(int duration)
        {
            //update text
            TMPro.TMP_Text timerText = pickUpTimerDisplay.GetComponent<TMPro.TMP_Text>();

            while (duration > 0)
            {
                timerText.text = duration.ToString();
                yield return new WaitForSeconds(1f);
                duration--;
            }
        }
        
        //DEACTIVATE TIMER UI
        public void DisableTimerUI()
        {
            //make invisible
            pickUpTimerDisplay.SetActive(false);
            timerBack.SetActive(false);
        }
        
        //LEVEL UI -----------------------------------------------------------------
        private void UpdateLevelUI()
        {
            
        }
        
        //BOSS UI ---------------------------------------------------------------------- 
        private void UpdateBossUI()
        {
            
        }
        
        //PAUSE GAMEPLAY AND ACTIVATE SCREEN -------------------------------------------
        //happens when esc click during gameplay
        void PauseGame()
        {
            //add pause screen
            pauseMenuUI.SetActive(true);
            // Freeze gameplay
            Time.timeScale = 0f;
            //freeze music
            AudioManager.Instance.gameplayBGM.Pause();
            //game is paused
            IsPause = true;
        }

        //RESUME GAMEPLAY AND DEACTIVATE SCREEN
        //happens when play game is clicked on pause screen
        public void ResumeGame()
        {
            AudioManager.Instance.buttonSFX.Play();
            //takes screen away
            pauseMenuUI.SetActive(false);
            // Resume gameplay
            Time.timeScale = 1f; 
            //resume music
            AudioManager.Instance.gameplayBGM.UnPause();
            //game is no longer paused
            IsPause = false;
        }

        //ENABLE YOU SURE SCREEN
        //happens after main menu clicked on pause
        public void YouSure()
        {
            AudioManager.Instance.buttonSFX.Play();
            youSureUI.SetActive(true);
            pauseMenuUI.SetActive(false);
        }

        //ENABLE PAUSE SCREEN
        //happenes after no is clicked for you sure?
        public void NotSure()
        {
            AudioManager.Instance.buttonSFX.Play();
            youSureUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        
        //LOAD MAIN MENU SCENE
        //happenes after yes is clicked for you sure?
        public void LoadMainMenu()
        {
            AudioManager.Instance.buttonSFX.Play();
            StartCoroutine(PauseToMenuTransition());
            Debug.Log("Loading main menu routine");
        }

        //LOAD MAIN MENU AFTER USERNAME INPUT
        public void EndGame()
        {
            AudioManager.Instance.buttonSFX.Play();
            StartCoroutine(EndGameToMenuTransition());
            Debug.Log("Loading main menu");
            //username and score should be taken at this point
        }
        
        IEnumerator PauseToMenuTransition()
        {
            Debug.Log("Running main menu routine");
            //fade
            fadeOutAnim.SetActive(true);
            
            //unfreeze game
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1f; 
            
            //GO TO MAIN MENU & STOP BGM
            yield return new WaitForSecondsRealtime(3f);
            AudioManager.Instance.gameplayBGM.Stop();
            //TheGameManager.Instance.menuBGM.Start();
            SceneManager.LoadScene(0);
        }

        IEnumerator EndGameToMenuTransition()
        {
            //fade
            fadeOutAnim.SetActive(true);
            enterUsernameUI.SetActive(false);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }
        
        //RESET ALL VALUES FOR LOOP
        public void ResetUI()
        {
            ScoreCount = 0;
            IsPause = false; //check if paused
            UsernameInput = "";
            TotalScore = 0;
        }
    }
}

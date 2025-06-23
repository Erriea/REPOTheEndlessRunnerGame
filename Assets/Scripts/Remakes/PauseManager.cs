using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Remakes
{
    //MANAGES PAUSE FUNCTION OF GAME
    public class PauseManager : MonoBehaviour
    {
        public static bool IsPause = false;
        
        //PAUSE UI COMPONENTS
        [SerializeField] GameObject pauseMenuUI;
        [SerializeField] GameObject youSureUI;
        [SerializeField] GameObject fadeOut;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!TheGameManager.Instance._isRunnerScene) return;
                
                //check if game is already pause
                if (IsPause)
                    return;
                PauseGame();
            }
        }

        //PAUSE GAMEPLAY AND ACTIVATE SCREEN
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
            youSureUI.SetActive(true);
            pauseMenuUI.SetActive(false);
        }

        //ENABLE PAUSE SCREEN
        //happenes after no is clicked for you sure?
        public void NotSure()
        {
            youSureUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
        
        //LOAD MAIN MENU SCENE
        //happenes after yes is clicked for you sure?
        public void LoadMainMenu()
        {
            StartCoroutine(MainMenuTransition());
            Debug.Log("Loading main menu routine");
        }
        
        IEnumerator MainMenuTransition()
        {
            Debug.Log("Running main menu routine");
            //fade
            fadeOut.SetActive(true);
            
            //unfreeze game
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1f; 
            
            //GO TO MAIN MENU & STOP BGM
            yield return new WaitForSecondsRealtime(3f);
            AudioManager.Instance.gameplayBGM.Stop();
            //TheGameManager.Instance.menuBGM.Start();
            SceneManager.LoadScene(0);
        }
    }
}

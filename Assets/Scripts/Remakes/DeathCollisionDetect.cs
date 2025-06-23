using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

//ATTACHED TO OBSTICLES TO DECTECT PLAYER COLLISION THAT CAUSES DEATH
namespace Remakes
{
    public class DeathCollisionDetect : MonoBehaviour
    {
        // ADD LEVELINFO
        public LevelInfo levelInfo;
        
        /*
        void Awake()
        {
            if (TheGameManager.Instance.thePlayer == null)
                TheGameManager.Instance.thePlayer = GameObject.FindGameObjectWithTag("Player");
            
            if (TheGameManager.Instance.mainCamera == null)
                TheGameManager.Instance.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            
            //MAKE SURE DEATH IS LINKED TO UI
            if (levelInfo == null && TheGameManager.Instance != null)
            {
                levelInfo = TheGameManager.Instance.levelInfo;
            }
            
            if (levelInfo.fadeOutAnim == null)
                levelInfo.fadeOutAnim = GameObject.Find("FadeOut");
    
            if (levelInfo.gameOverText == null)
                levelInfo.gameOverText = GameObject.Find("GameOverText");
        }
        */
        
        
        void OnEnable()
        {
            if (levelInfo == null)
            {
                levelInfo = TheGameManager.Instance != null ? TheGameManager.Instance.levelInfo : FindObjectOfType<LevelInfo>();
            }

            if (levelInfo != null)
            {
                if (levelInfo.fadeOutAnim == null)
                    levelInfo.fadeOutAnim = GameObject.Find("FadeOut");

                if (levelInfo.gameOverText == null)
                    levelInfo.gameOverText = GameObject.Find("GameOverText");
            }

        }


        //WHEN COLLISION IS TRIGGERED
        void OnTriggerEnter(Collider other)//collider refered to here is the players collider
        {
            Debug.Log("Player hit the death zone");
            //CHECK FOR INVINSIBILITY
            if (TheGameManager.Instance.isInvsPUActive)
            {
                Debug.Log("Player hit death zone but is invincible — ignored.");
                TheGameManager.Instance.StopInvincibilityEarly();
                return;
            }
            else
            {
                //TRIGGER END
                StartCoroutine(CollisionEnd());
            }
        }
        
        private void EndGame()
        {
            //GO TO MAIN MENU & STOP BGM
            AudioManager.Instance.gameplayBGM.Stop();
            SceneManager.LoadScene(0);
        }
        
        //CUTSCENE TYPE THING FOR DEATH
        IEnumerator CollisionEnd()
        {
            //KILLS PLAYER
            Debug.Log("Game over activated");
            PlayerControllerRemake.IsAlive = false;
            //can add animation here if theres time
            AudioManager.Instance.deathSFX.Play();
            TheGameManager.isAlive = false;
            TheGameManager.Instance.thePlayer.GetComponent<PlayerControllerRemake>().enabled = false;

            if (LevelInfo.IsPause == true)
            {
                yield return new WaitForSeconds(1f);
                levelInfo.fadeOutAnim.SetActive(true);
            }
            else
            {
                //GAME OVER TRASITION
                //camera wobble
                TheGameManager.Instance.mainCamera.GetComponent<Animator>().Play("CollisionCam");
                yield return new WaitForSeconds(1f);
                //fade
                levelInfo.fadeOutAnim.SetActive(true);
                levelInfo.gameOverText.SetActive(true);
            
                yield return new WaitForSeconds(3f);
            
                //make space to enter username
                levelInfo.enterUsernameUI.SetActive(true);
                AudioManager.Instance.gameplayBGM.Stop();
                levelInfo.yourScore.GetComponent<TMPro.TMP_Text>().text = "SCORE: " + LevelInfo.TotalScore;
            }
        }

    }
}

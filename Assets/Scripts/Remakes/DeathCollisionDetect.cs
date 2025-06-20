using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//ATTACHED TO OBSTICLES TO DECTECT PLAYER COLLISION THAT CAUSES DEATH
namespace Remakes
{
    public class DeathCollisionDetect : MonoBehaviour
    {
        [SerializeField] GameObject thePlayer;
        [SerializeField] GameObject mainCam;
        [SerializeField] GameObject fadeOut;
        
        [SerializeField] AudioSource deathFX;

        //WHEN COLLISION IS TRIGGERED
        void OnTriggerEnter(Collider other)//collider refered to here is the players collider
        {
            if (PlayerControllerRemake.IsInvincible)
            {
                Debug.Log("Player hit death zone but is invincible — ignored.");
                TheGameManager.Instance.StopInvincibilityEarly();
                return;
            }
            else
            {
                StartCoroutine(CollisionEnd());
            }
        }

        IEnumerator CollisionEnd()
        {
            //KILLS PLAYER
            Debug.Log("Player hit the death zone");
            PlayerControllerRemake.IsAlive = false;
            deathFX.Play();
            thePlayer.GetComponent<PlayerControllerRemake>().enabled = false;
            
            //TRIGGER FADE OUT
            mainCam.GetComponent<Animator>().Play("CollisionCam");
            yield return new WaitForSeconds(1f);
            fadeOut.SetActive(true);
            
            //GO TO MAIN MENU & STOP BGM
            yield return new WaitForSeconds(2f);
            TheGameManager.Instance.gameplayBGM.Stop();
            SceneManager.LoadScene(0);
        }
    }
}

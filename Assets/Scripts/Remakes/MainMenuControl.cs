using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//MANAGE MAIN MENU AND STARTING GAME
namespace Remakes
{
    public class MainMenuControl : MonoBehaviour
    {
        [SerializeField] GameObject fadeOut;
        
        void Start()
        {
            //START MENU MUSIC
            AudioManager.Instance.menuBGM.Play();
        }

        public void StartGame()
        {
            StartCoroutine(StartButton());
        }

        IEnumerator StartButton()
        {
            AudioManager.Instance.buttonSFX.Play();
            fadeOut.SetActive(true);
            yield return new WaitForSeconds(1f);
            AudioManager.Instance.menuBGM.Stop();
            SceneManager.LoadScene(1);
            //enable the GamemanagerObject for the scene
            //gameObject.SetActive(true);
        }
        
        public void ViewLeaderBoard()
        {
            StartCoroutine(LeaderBoardButton());
        }

        IEnumerator LeaderBoardButton()
        {
            AudioManager.Instance.buttonSFX.Play();
            fadeOut.SetActive(true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(2);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//MANAGE MAIN MENU AND STARTING GAME
namespace Remakes
{
    public class MainMenuControl : MonoBehaviour
    {
        //INSTATIATE FX
        [SerializeField] AudioSource buttonFX;
        
        [SerializeField] GameObject fadeOut;
        
        void Start()
        {
        
        }
        
        void Update()
        {
        
        }

        public void StartGame()
        {
            StartCoroutine(StartButton());
        }

        IEnumerator StartButton()
        {
            buttonFX.Play();
            fadeOut.SetActive(true);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(1);
        }
    }
}

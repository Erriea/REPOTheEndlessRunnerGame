using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton - can access from all scripts
    public static GameManager Instance { get; private set; }
    public GameObject gameOverPanel;
    //private bool _gameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    void Start()
    {
        
        gameOverPanel.SetActive(false);
        
    }

    public void GameOver()
    {
        ScoreManager.Instance.StopScoring();
        ScoreManager.Instance.UpdateScoreUI();
        gameOverPanel.SetActive(true);
        
    }
    
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   

}

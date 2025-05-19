using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton - can access from all scripts
    public static GameManager Instance { get; private set; }
    public GameObject gameOverPanel;

    [SerializeField] private Button returnButton;
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
        ScoreManager.Instance.SaveHighScore();
        ScoreManager.Instance.UpdateScoreUI();
        gameOverPanel.SetActive(true);
        returnButton.onClick.AddListener(OnReturnButtonClicked);

    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnReturnButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
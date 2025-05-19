using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Awake()
    {
        // If you didn't drag it in, find by tag:
        if (highScoreText == null)
            highScoreText = GameObject.FindWithTag("HighScoreText")
                ?.GetComponent<TextMeshProUGUI>();

        int best = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + best.ToString();
    }
    
    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

}

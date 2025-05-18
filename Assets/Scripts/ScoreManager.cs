using System.Collections;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance{get; private set;}
    public int playerScore { get; private set; }
    public TextMeshProUGUI scoreText; // Assign in Inspector
    public TextMeshProUGUI endScoreText;
    private float scoreMultiplier = 1f;
    public bool isAlive = true;

    
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
        }
    }

    private void Start()
    {
        StartScoring();
    }

   
    private void Update()
    {
        
    }

   
    private Coroutine scoreCoroutine;

    public void StartScoring()
    {
        isAlive = true;
        scoreCoroutine = StartCoroutine(IncrementScore());
    }

    public void StopScoring()
    {
        Debug.Log("Stopping Score Coroutine!");
        isAlive = false;
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
            scoreCoroutine = null;
        }
    } 

    public void StartMultiplier(float multiplier, float multiplierDuration)
    {
        StartCoroutine(MultiplyScoreCoroutine(multiplier, multiplierDuration));
    }


    private IEnumerator IncrementScore()
    {
        while (isAlive)
        {
            //Debug.Log("[ScoreManager] coroutine STARTED");
            yield return new WaitForSeconds(1f);
            playerScore += (int)(1 * scoreMultiplier);
            //Debug.Log("[ScoreManager] tick → " + playerScore);
            UpdateScoreUI();
        }

    }

    private IEnumerator MultiplyScoreCoroutine(float multiplier, float multiplierDuration)
    {
        scoreMultiplier = multiplier;
        yield return new WaitForSeconds(multiplierDuration);
        scoreMultiplier = 1f;
    }

    
    public void UpdateScoreUI()
    {

        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore.ToString();
        }
        
        if (endScoreText != null)
        {
            endScoreText.text = "Score:" + playerScore.ToString();
        }
    }

   
}

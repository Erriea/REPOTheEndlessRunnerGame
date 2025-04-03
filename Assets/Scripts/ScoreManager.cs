using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   public static ScoreManager Instance{get; private set;}
       public int playerScore { get; private set; }
       public TextMeshProUGUI scoreText; // Assign in Inspector
       public bool isAlive = true;
   
       private void Start()
       {
           StartCoroutine(IncrementScore());
       }
   
       private void Update()
       {
           if (!isAlive)
           {
               StopAllCoroutines();
           }
       }
   
   
   
       private System.Collections.IEnumerator IncrementScore()
       {
           while (isAlive)
           {
               yield return new WaitForSeconds(1f);
               playerScore += 1;
               UpdateScoreUI();
           }
   
       }
   
       private void UpdateScoreUI()
       {
           if (scoreText != null)
           {
               scoreText.text = "Score: " + playerScore.ToString();
           }
       }
   
   
   
   
   
       //[SerializeField] private TextMeshProUGUI scoreText;
       //private int _score = 0;
       //// Start is called once before the first execution of Update after the MonoBehaviour is created
       //void Start()
       //{
       //}
       //// Update is called once per frame
       //void Update()
       //{
       //}
}

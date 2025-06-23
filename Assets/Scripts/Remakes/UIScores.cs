using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScores : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField;
    //[SerializeField] private TMP_InputField scoreInputField;
    [SerializeField] private Button enterInfoButton;


    private void Start()
    {
        enterInfoButton.onClick.AddListener(PostScoreHandler);
    }

    private void PostScoreHandler()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.playerName = playerNameInputField.text;
        //scoreData.score = int.Parse(scoreInputField.text);
        
        string json = JsonUtility.ToJson(scoreData);
        Debug.Log(json);    
        Remakes.DatabaseManager.Instance.Post(Remakes.TheGameManager.Instance.PostScoreEndpoint, json);
    }
}

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int score;
}



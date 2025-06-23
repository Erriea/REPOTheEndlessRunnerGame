using SimpleJSON;
using TMPro;
using UnityEngine;

public class UILeaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTextMesh;
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
   
   

    private void Start()
    {
        FetchData();
    }

    private void OnEnable()
    {
        Remakes.DatabaseManager.OnDatabaseUpdate += FetchData;
    }
   
    private void OnDisable()
    {
        Remakes.DatabaseManager.OnDatabaseUpdate -= FetchData;
    }

    private void FetchData()
    {
        Remakes.DatabaseManager.Instance.Get(Remakes.TheGameManager.Instance.ScoresEndpoint, ProcessScoresJSON);
    }

    private void ProcessScoresJSON(string json)
    {

      
        JSONNode data = SimpleJSON.JSON.Parse(json);
        nameTextMesh.text = "";
        scoreTextMesh.text = "";
      
        foreach (JSONNode entry in data)
        {
            string playerName = entry["player_name"] == null
                ? entry["playerName"]
                : entry["player_name"];
         
            nameTextMesh.text += playerName + "\n";
            scoreTextMesh.text += entry["score"] + "\n";
        }
    }
}

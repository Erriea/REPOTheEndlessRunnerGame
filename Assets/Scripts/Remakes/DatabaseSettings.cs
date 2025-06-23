using UnityEngine;

[CreateAssetMenu(fileName = "NewDatabaseSettings", menuName = "SO/DatabaseSettings", order = 0)]
public class DatabaseSettings : ScriptableObject
{
    public string databaseURL = "http://localhost:3000/";
    public string scoresEndpoint = "scores";
    public string postScoreEndpoint = "score";
}


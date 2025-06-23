using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Remakes
{
    public class DatabaseManager : MonoBehaviour
    {
        public static DatabaseManager Instance { get; private set; }

    public delegate void DatabaseUpdateDelegate();
    public static event DatabaseUpdateDelegate OnDatabaseUpdate;

    // Web communication
    public void Post(string endpoint, string json)
    {
        StartCoroutine(PostData(endpoint, json));
    }

    public void Get(string endpoint, Action<string> callback = null)
    {
        StartCoroutine(GetData(endpoint, callback));
    }

    // Runner-specific fields
    [SerializeField] public LevelInfo levelInfo; // Only exists in Runner scene
    public int playerHighscore = 0;

    private bool _isRunnerScene;
    private bool _isLeaderboardScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Listen for scene load events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Prevent memory leak
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _isRunnerScene = scene.buildIndex == 1;
        _isLeaderboardScene = scene.buildIndex == 2;

        if (_isRunnerScene)
        {
            // Automatically find LevelInfo in the scene
            levelInfo = FindObjectOfType<LevelInfo>();
        }
    }

    private IEnumerator PostData(string endpoint, string json)
    {
        string URL = TheGameManager.Instance.DatabaseURL + endpoint;

        using (UnityWebRequest www = new UnityWebRequest(URL, "POST"))
        {
            byte[] body = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(body);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Object added to database");
                Debug.Log(www.downloadHandler.text);
                OnDatabaseUpdate?.Invoke();
            }
        }
    }

    private IEnumerator GetData(string endpoint, Action<string> callback)
    {
        string URL = TheGameManager.Instance.DatabaseURL + endpoint;

        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
                callback?.Invoke(null);
            }
            else
            {
                Debug.Log("Data received from database");
                //Debug.Log(www.downloadHandler.text);
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }
    }
}

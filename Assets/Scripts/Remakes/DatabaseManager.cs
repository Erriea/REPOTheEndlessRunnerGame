using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Remakes
{
    public class DatabaseManager : MonoBehaviour
    {
        public static DatabaseManager Instance;
        
        // ADD LEVELINFO
        // this will fill in the serialized field automatically when when the runner scene if loaded
        //LevelInfo is only in the runner scene and that is where the score is stored
        //also thats where the input field that takes the playes username is. Havent coded anything to get that
        [SerializeField] public LevelInfo levelInfo;

        //the highscore is assigned in the DeathCollisionDetect script so it can only be used after a run has taken place
        //you might want to check when you should reset it coz obviously this script only gets
        //set up once so wont start at 0 again until you restart the game
        public int playerHighscore = 0;
        private bool _isRunnerScene;
        private bool _isLeaderboardScene;
        
        void Awake()
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

        //runs when you load a new scene
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //if you are in runner scene this will happen be set true
            _isRunnerScene = (scene.buildIndex == 1);
            //if you are in leaderboard scene this will be true
            _isLeaderboardScene = (scene.buildIndex == 2);
            //this will be helpfull if u only want stuff to run when you in a certain scene
        }
    }
}

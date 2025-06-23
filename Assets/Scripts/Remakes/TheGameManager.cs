using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


//MANAGES ANYTHING RELATED TO PROJECT AS A WHOLE THAT CAN BE ACCESS ANYWHERE AND NOT DELETE BETWEEN SCENES
namespace Remakes
{
    public class TheGameManager : MonoBehaviour
    {
        public static TheGameManager Instance;
        
        //THE PLAYER
        //making at a spawned instance
        [SerializeField] GameObject playerPrefab;
        [HideInInspector] public GameObject thePlayer;
        public static bool isAlive = true;
        
        //THE CAMERA
        public GameObject mainCamera;
        private Vector3 _defaultCamPos;
        private Quaternion _defaultCamRot;
        private Vector3 targetCamPos;
        private Quaternion targetCamRot;
        public float camLerpSpeed = 2.5f;
        private bool shouldLerpCam = false;

        // ADD LEVELINFO
        [SerializeField] public LevelInfo levelInfo;

        //OBSTICLE PREFABS
        [SerializeField] public GameObject treePrefab;
        [SerializeField] public GameObject rockPrefab;
        [SerializeField] public GameObject logPrefab;

        //PICK UP PREFABS
        [SerializeField] public GameObject scorePickupPrefab;
        [SerializeField] public GameObject jumpPickupPrefab;
        [SerializeField] public GameObject invinciblePickupPrefab;

        //BOSS 1 STUFF
        [SerializeField] public GameObject icePrefab;
        [SerializeField] public GameObject boss1Prefab;
        
        //BOSS2 STUFF
        [SerializeField] private GameObject boss2Prefab;
        private GameObject boss2Instance;
        public bool isBoss2Active = false;
        
        //SPAWNED OBSTICLES
        public OffScreenHider offScreenHider;
        
        //PICK UP ACTIVE CHECKS
        [HideInInspector] public bool isPickUpActive = false;
        [HideInInspector] public bool isScorePUActive = false;
        [HideInInspector] public bool isJumpPUActive = false;
        [HideInInspector] public bool isInvsPUActive = false;

        //SCENE STUFF
        [HideInInspector] public bool _isRunnerScene;
        Coroutine _invincibleRoutine;
        
        //BOSS AND ICE
        public bool isBoss1Active;

        //SINGLETON FOR GAME MANAGER
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
        
        //GET GAME TO RESTART AFTER LOADING SECENE
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _isRunnerScene = (scene.buildIndex == 1);
            
            
            
            if (_isRunnerScene)// Your Runner scene
            {
                //start music
                AudioManager.Instance.gameplayBGM.Play();
                
                isBoss1Active = false;
                // HIDE BOSS
                
                Debug.Log("Runner set true");
                //enable script
                enabled = true;
                
                levelInfo = FindObjectOfType<LevelInfo>();
                offScreenHider = FindObjectOfType<OffScreenHider>();
                ResetGame();
                SpawnPlayer();
                //set camera positions
                _defaultCamPos = mainCamera.transform.localPosition;
                _defaultCamRot = mainCamera.transform.localRotation;
                SpawnBoss2();
                boss2Instance.GetComponent<Boss2Move>().playerTransform = thePlayer.transform;
                
                SegmentPooler.Instance.ResetSegmentPool();
                FindObjectOfType<SegmentController>().ResetZPosition();
                
                
            
                //start the boss looping timer
                StartCoroutine(BossCycleLoop());
                
                //pickups not cative
                isScorePUActive = false;
                isJumpPUActive = false;
                isInvsPUActive = false;
            }
        }

        void Update()
        {
            if (mainCamera != null && shouldLerpCam)
            {
                mainCamera.transform.localPosition = Vector3.Lerp(
                    mainCamera.transform.localPosition,
                    targetCamPos,
                    Time.deltaTime * camLerpSpeed
                );

                mainCamera.transform.localRotation = Quaternion.Lerp(
                    mainCamera.transform.localRotation,
                    targetCamRot,
                    Time.deltaTime * camLerpSpeed
                );
            }

        }
        
        //SETTING UP PLAYER SPAWN
        void SpawnPlayer()
        {
            if (!_isRunnerScene) return;
            
            //delete previous player
            if (thePlayer != null)
            {
                Destroy(thePlayer);
                thePlayer = null;
            }

            //set player in correct spot
            thePlayer = Instantiate(playerPrefab, new Vector3(0, 1.3f, -22.38f), Quaternion.identity);
            thePlayer.tag = "Player";
            
            //find camera childed to instance of player
            mainCamera = thePlayer.GetComponentInChildren<Camera>().gameObject;

        }
        
        //SPAWN BOSS 2 10 UNIT BEHIND PLAUER
        void SpawnBoss2()
        {
            if (thePlayer == null || boss2Prefab == null)
            {
                Debug.LogWarning("Cannot spawn Boss2 — missing references.");
                return;
            }

            Vector3 spawnPos = thePlayer.transform.position + new Vector3(0, 0, -10f);
            boss2Instance = Instantiate(boss2Prefab, spawnPos, Quaternion.identity);
        }


        /*
        //REGISTER OBSTICLESPAWNER METHODS
        public void RegisterBoss1(GameObject go) => boss1Instances.Add(go);
        public void RegisterIce(GameObject go) => iceInstances.Add(go);
        public void RegisterScorePU(GameObject go) => scorePickups.Add(go);
        public void RegisterJumpPU(GameObject go) => jumpPickups.Add(go);
        public void RegisterInvsPU(GameObject go) => invinciblePickups.Add(go);
        */
        
        //PICK UP ACTIVATIONS --------------------------------------------------
        //DEACTIVATE PICK UPS
        public void DeactivatePickUps()
        {
            if (!_isRunnerScene) return;
            
            isPickUpActive = true;
            
            offScreenHider.HideAllWithTag("ScorePickup");

            offScreenHider.HideAllWithTag("JumpPickup");
            
            offScreenHider.HideAllWithTag("InvinciblePickup");
        }

        //REACTIVATE PICK UPS
        private void ReactivatePickUps()
        {
            if (!_isRunnerScene) return;
            
            isPickUpActive = false;
            
            offScreenHider.ShowAllWithTag("ScorePickup");

            offScreenHider.ShowAllWithTag("JumpPickup");
            
            offScreenHider.ShowAllWithTag("InvinciblePickup");
        }
        
        //STUFF FOR INVINCIBILITY------------------------------------------
        public void StartInvincibility()
        {
            if (!_isRunnerScene) return;
            /*
            if (_invincibleRoutine != null)
                StopCoroutine(_invincibleRoutine);
            */

            Debug.Log("Invs PickUp routine called");
            _invincibleRoutine = StartCoroutine(InvinsibilityPickUpActivate());
        }

        public void StopInvincibilityEarly()
        {
            if (!_isRunnerScene) return;
            
            if (_invincibleRoutine != null)
                StopCoroutine(_invincibleRoutine);

            PlayerControllerRemake.IsInvincible = false;
            isInvsPUActive = false;
            isPickUpActive = false;
            ReactivatePickUps();
            Debug.Log("Invincibility ended early due to death collision.");
        }
        
        //PICK UP COROUTINES-----------------------------------------------
        //FOR DOUBLE SCORE PICK UP
        public IEnumerator DoubleScorePickUpActivate()
        {
            //LevelInfo.ScoreCount = LevelInfo.ScoreCount + 2;
            Debug.Log("Score PickUp Coroutine Activated");
            
            //set countdown number for ui
            levelInfo.StartPickUpCountDown(10);
            
            //lasts 10 seconds
            yield return new WaitForSeconds(10f);
            
            //RESET
            isScorePUActive = false;
            isPickUpActive = false;
            levelInfo.DisablePickUpUI();
            levelInfo.DisableTimerUI();
            DeactivatePickUps();
            ReactivatePickUps();
            
            Debug.Log("Double Score expired");
        }

        //FOR DOUBLE JUMP PICK UP
        public IEnumerator DoubleJumpPickUpActivate()
        {
            Debug.Log("Jump PickUp coroutine Activated");
            PlayerControllerRemake.AllowDoubleJump = true;
            
            //set countdown number for ui
            levelInfo.StartPickUpCountDown(10);
            
            //lasts 8 seconds
            yield return new WaitForSeconds(10f);
            
            //RESET
            PlayerControllerRemake.AllowDoubleJump = false;
            isJumpPUActive = false;
            isPickUpActive = false;
            levelInfo.DisablePickUpUI();
            levelInfo.DisableTimerUI();
            DeactivatePickUps();
            ReactivatePickUps();
            
            Debug.Log("Double Jump expired");
        }
        
        public IEnumerator InvinsibilityPickUpActivate()
        {
            Debug.Log("Inv PickUp coroutine Activated");
            
            PlayerControllerRemake.IsInvincible = true;
            
            //set countdown number for ui
            levelInfo.StartPickUpCountDown(20);
            
            //lasts 20 seconds
            yield return new WaitForSeconds(20f);
            
            //RESET EVERYTHING
            PlayerControllerRemake.IsInvincible = false;
            isInvsPUActive = false;
            isPickUpActive = false;
            levelInfo.DisablePickUpUI();
            levelInfo.DisableTimerUI();
            DeactivatePickUps();
            ReactivatePickUps();
            
            Debug.Log("Invincibility expired");
        }
        
        //BOSS-------------------------------------------
        
        //REACTIVATE BOSS
        private void ReactivateBossPrefs() //*****************************
        {
            //SHOW WAVE
            //i yet again realise i cant spell. 
            //correct spelling is obstacle but my world my rules i guess
            //foreach (var b in ObsticleSpawner.bosses1) b.GetComponent<OffScreenHider>().Show();
            //foreach (var i in ObsticleSpawner.ices)    i.GetComponent<OffScreenHider>().Show();
            
            offScreenHider.ShowAllWithTag("Ice");
            offScreenHider.ShowAllWithTag("Boss1");
            
        }
        
        //DEACTIVATE BOSS
        public void DeactivateBossPrefs()//**********************************
        {
            // HIDE wave
            //foreach (var b in ObsticleSpawner.bosses1) b.GetComponent<OffScreenHider>().Hide();
            //foreach (var i in ObsticleSpawner.ices)    i.GetComponent<OffScreenHider>().Hide();
            
            offScreenHider.HideAllWithTag("Ice");
            offScreenHider.HideAllWithTag("Boss1");
        }
        
        IEnumerator DelayedBossDeactivate()
        {
            // wait a few frames or seconds until segments are spawned
            yield return new WaitForSeconds(0.5f);
            DeactivateBossPrefs();
        }
        
        //BOSS COROUTINE
        IEnumerator BossCycleLoop()
        {
            while (_isRunnerScene)
            {
                //WAIT 30 SECS
                yield return new WaitForSeconds(10f);

                //play SFX
                Debug.Log("Boss 1 wave starting!");
                
                isBoss1Active = true;

                //ACTIVATE DEACTIVATED PREFABS//*************************
                ReactivateBossPrefs();
                
                //UPDATE UI TO SHOW BOSS SPAWNED
                Debug.Log("UpdateBossUI");
                levelInfo.levelBack.SetActive(true);
                levelInfo.bossBack.SetActive(true);
                levelInfo.levelDisplay.GetComponent<TMPro.TMP_Text>().text = "LEVEL 1";
                levelInfo.bossDisplay.GetComponent<TMPro.TMP_Text>().text = "ICE GLOB ATTACKING";

                //TIMING FOR THE BOSS 1
                yield return new WaitForSeconds(10f);

                Debug.Log("Boss 1 END");
                
                //DEACTIVE BOSS
                DeactivateBossPrefs();

                //DEACTIVATE BOSS UI
                levelInfo.levelBack.SetActive(false);
                levelInfo.bossBack.SetActive(false);

                //INCREASE SCORE FOR PASSING BOSS ALIVE
                if (isAlive == true)
                {
                    LevelInfo.ScoreCount += 30;
                    levelInfo.UpdateScoreUI();
                }
                
                 
                //PEACE TIME
                yield return new WaitForSeconds(10f);
                
                //ACTIVATE BOSS 2
                Debug.Log("UpdateBossUI");
                levelInfo.levelBack.SetActive(true);
                levelInfo.bossBack.SetActive(true);
                levelInfo.levelDisplay.GetComponent<TMPro.TMP_Text>().text = "LEVEL 2";
                levelInfo.bossDisplay.GetComponent<TMPro.TMP_Text>().text = "PARASITE ATTACKING";
                isBoss2Active = true;
                
                //chnage camera
                // Set boss camera view
                shouldLerpCam = true;
                mainCamera.transform.localPosition = new Vector3(0f, 16.91f, -12.38f);
                mainCamera.transform.localRotation = Quaternion.Euler(53f, 0f, 0f);
                shouldLerpCam = false;
                
                yield return new WaitForSeconds(10f);
                
                //DEACTIVATE BOSS UI
                Debug.Log("Boss2 end");
                levelInfo.levelBack.SetActive(false);
                levelInfo.bossBack.SetActive(false);
                isBoss2Active = false;
                
                //reset camera
                shouldLerpCam = true;
                mainCamera.transform.localPosition = _defaultCamPos;
                mainCamera.transform.localRotation = _defaultCamRot;
                targetCamPos = _defaultCamPos;
                targetCamRot = _defaultCamRot;
                shouldLerpCam = false;

                
                //INCREASE SCORE FOR PASSING BOSS ALIVE
                if (isAlive == true)
                {
                    LevelInfo.ScoreCount += 30;
                    levelInfo.UpdateScoreUI();
                }
                
            }
        }

        
        //RESET GAME FOR LOOP-----------------------------------------------
        public void ResetGame()
        {
            //RESET PLAYER IF THERE ISNT ONE
            PlayerControllerRemake.IsAlive = true;
            PlayerControllerRemake.IsInvincible = false;
            PlayerControllerRemake.AllowDoubleJump = false;
            if (thePlayer != null)
            {
                //POSITION PLAYER
                isAlive = true;
                thePlayer.transform.position = new Vector3(0, 1.3f, -22.38f);
                thePlayer.GetComponent<PlayerControllerRemake>().enabled = true;
            }
            
            //RESET UI
            levelInfo.ResetUI();
            
            //RESET PICK UP VALUES
            isPickUpActive = false;
            isScorePUActive = false;
            isJumpPUActive = false;
            isInvsPUActive = false;
            
            //RESET PLAYER VALUES
            PlayerControllerRemake.IsAlive = true;
            PlayerControllerRemake.IsInvincible = false;
            PlayerControllerRemake.AllowDoubleJump = false;
            
            //MAKE SURE MUSIC HAS STOPPED
            AudioManager.Instance.gameplayBGM.Stop();
            
            //RESET SPAWNING POOL
            SegmentPooler.Instance.ResetSegmentPool();
            FindObjectOfType<SegmentController>().ResetZPosition();
            
            //STOP ANYTHING ELSE
            StopAllCoroutines();
        }

    }
}
